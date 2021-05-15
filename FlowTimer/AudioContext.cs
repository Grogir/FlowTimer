using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using static FlowTimer.MMDeviceAPI;
using static FlowTimer.MediaFundationAPI;

namespace FlowTimer {

    public class AudioContext {

        public MMDevice AudioEndPoint;
        public AudioClient AudioClient;
        public AudioRenderClient RenderClient;

        public WAVEFORMATEX Format;
        public Guid ExtensibleFormatTag;
        public int BytesPerSample;

        public int FundementalPeriod;
        public int MinPeriod;
        public int MaxPeriod;
        public int BufferSampleCount;

        public bool Running;
        public EventWaitHandle SampleWaitHandle;

        public Thread AudioThread;
        public byte[] AudioBuffer = new byte[0];
        public int AudioBufferPosition = 0;

        public object Resampler;

        public AudioContext() {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator((IMMDeviceEnumerator) Activator.CreateInstance(Type.GetTypeFromCLSID(MMDeviceEnumeratorID)));
            AudioEndPoint = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

            AudioClient = AudioEndPoint.CreateAudioCilent();
            AudioClient.GetCurrentSharedModeEnginePeriod(out IntPtr formatPtr, out _);
            Format = Marshal.PtrToStructure<WAVEFORMATEX>(formatPtr);
            BytesPerSample = Format.nChannels * Format.wBitsPerSample / 8;

            if(Format.wFormatTag == WAVE_FORMAT_EXTENSIBLE) {
                ExtensibleFormatTag = Marshal.PtrToStructure<WAVEFORMATEXTENSIBLE>(formatPtr).SubFormat;
            } else {
                ExtensibleFormatTag = Guid.Empty;
            }

            AudioClient.GetSharedModeEnginePeriod(formatPtr, out int _, out FundementalPeriod, out MinPeriod, out MaxPeriod);
            AudioClient.InitializeSharedAudioStream(AUDCLNT_STREAMFLAGS_EVENTCALLBACK, MinPeriod, formatPtr, IntPtr.Zero);

            BufferSampleCount = AudioClient.BufferSize;

            SampleWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            AudioClient.SetEventHandle(SampleWaitHandle.SafeWaitHandle.DangerousGetHandle());

            RenderClient = AudioClient.CreateRenderClient();

            MFStartup(MF_SDK_VERSION << 16 | MF_API_VERSION, 0);

            MFTEnumEx(MFT_CATEGORY_AUDIO_EFFECT, MFT_ENUM_FLAG_ALL, null, null, out var interfacesPointer, out var interfaceCount);
            for(int n = 0; n < interfaceCount; n++) {
                IntPtr ptr = Marshal.ReadIntPtr(interfacesPointer + n * Marshal.SizeOf(interfacesPointer));
                IMFActivate activator = (IMFActivate) Marshal.GetObjectForIUnknown(ptr);
                activator.GetGUID(MFT_TRANSFORM_CLSID_Attribute, out Guid clsid);
                if(clsid.Equals(CLSID_CResamplerMediaObject)) {
                    activator.ActivateObject(CLSID_IMFTransform, out Resampler);
                    break;
                }
            }
        }

        public void Destroy() {
            if(Running) {
                Running = false;
                AudioClient.Stop();
            }

            Marshal.ReleaseComObject(Resampler);
            MFShutdown();
        }

        public void QueueAudio(byte[] samples) {
            AudioBuffer = samples;
            AudioBufferPosition = 0;
        }

        public void ClearQueuedAudio() {
            AudioBufferPosition = AudioBuffer.Length;
        }

        public void StartAudioThread() {
            if(Running) return;

            Running = true;
            AudioThread = new Thread(() => {
                IntPtr data = RenderClient.GetBuffer(BufferSampleCount);
                GenerateSamples(data, BufferSampleCount * BytesPerSample);
                RenderClient.ReleaseBuffer(BufferSampleCount, 0);

                AudioClient.Start();

                while(Running) {
                    WaitHandle.WaitAny(new WaitHandle[] { SampleWaitHandle }, -1, false);

                    int samplesAvailable = BufferSampleCount - AudioClient.CurrentPadding;
                    IntPtr buf = RenderClient.GetBuffer(samplesAvailable);
                    GenerateSamples(buf, samplesAvailable * BytesPerSample);
                    RenderClient.ReleaseBuffer(samplesAvailable, 0);
                }
            });
            AudioThread.Start();
        }

        public unsafe void GenerateSamples(IntPtr dest, int numBytes) {
            int length = Math.Min(numBytes, AudioBuffer.Length - AudioBufferPosition);
            for(int i = 0; i < length; i++) ((byte*) dest)[i] = AudioBuffer[AudioBufferPosition + i];
            for(int i = length; i < numBytes; i++) ((byte*) dest)[i] = 0;
            AudioBufferPosition += length;
        }

        public byte[] ResamplePCM(byte[] pcm, WAVEFORMATEX pcmFormat) {
            if(Resampler == null) return pcm; // Error?

            IWMResamplerProps resamplerProps = (IWMResamplerProps) Resampler;
            resamplerProps.SetHalfFilterLength(60);

            IMFTransform transform = (IMFTransform) Resampler;

            MFCreateMediaType(out IMFMediaType inputType);
            inputType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
            inputType.SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM);
            inputType.SetUINT32(MF_MT_AUDIO_NUM_CHANNELS, pcmFormat.nChannels);
            inputType.SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, pcmFormat.nSamplesPerSec);
            inputType.SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, pcmFormat.nBlockAlign);
            inputType.SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, pcmFormat.nAvgBytesPerSec);
            inputType.SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, pcmFormat.wBitsPerSample);
            inputType.SetUINT32(MF_MT_ALL_SAMPLES_INDEPENDENT, 1);
            transform.SetInputType(0, inputType, 0);

            WAVEFORMATEX outputFormat = new WAVEFORMATEX();
            outputFormat.wFormatTag = 1;
            outputFormat.cbSize = (short) Marshal.SizeOf<WAVEFORMATEX>();
            outputFormat.nSamplesPerSec = Format.nSamplesPerSec;
            outputFormat.nChannels = 2;
            outputFormat.wBitsPerSample = 16;
            outputFormat.nAvgBytesPerSec = outputFormat.nSamplesPerSec * outputFormat.nChannels * outputFormat.wBitsPerSample / 8;
            outputFormat.nBlockAlign = (short) (outputFormat.nChannels * outputFormat.wBitsPerSample / 8);

            MFCreateMediaType(out IMFMediaType outputType);
            outputType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
            outputType.SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM);
            outputType.SetUINT32(MF_MT_AUDIO_NUM_CHANNELS, outputFormat.nChannels);
            outputType.SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, outputFormat.nSamplesPerSec);
            outputType.SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, outputFormat.nBlockAlign);
            outputType.SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, outputFormat.nAvgBytesPerSec);
            outputType.SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, outputFormat.wBitsPerSample);
            outputType.SetUINT32(MF_MT_ALL_SAMPLES_INDEPENDENT, 1);
            transform.SetOutputType(0, outputType, 0);

            transform.ProcessMessage(MFT_MESSAGE_COMMAND_FLUSH, IntPtr.Zero);
            transform.ProcessMessage(MFT_MESSAGE_NOTIFY_BEGIN_STREAMING, IntPtr.Zero);
            transform.ProcessMessage(MFT_MESSAGE_NOTIFY_START_OF_STREAM, IntPtr.Zero);

            MemoryStream resampledPCM = new MemoryStream();

            int inputCursor = 0;
            int maxBufferSize = pcmFormat.nAvgBytesPerSec;

            while(inputCursor < pcm.Length) {
                int bufferSize = Math.Min(pcm.Length - inputCursor, maxBufferSize);

                MFCreateMemoryBuffer(bufferSize, out IMFMediaBuffer inputData);
                inputData.Lock(out IntPtr byteBufferTo, out _, out _);
                Marshal.Copy(pcm, inputCursor, byteBufferTo, bufferSize);
                inputData.Unlock();
                inputData.SetCurrentLength(bufferSize);
                inputCursor += bufferSize;

                MFCreateSample(out IMFSample inputSample);
                inputSample.AddBuffer(inputData);
                transform.ProcessInput(0, inputSample, 0);

                MFCreateSample(out IMFSample outSample);
                MFCreateMemoryBuffer(Format.nAvgBytesPerSec + Format.nBlockAlign, out IMFMediaBuffer outputData);
                outSample.AddBuffer(outputData);

                MFT_OUTPUT_DATA_BUFFER outputDataBuffer = new MFT_OUTPUT_DATA_BUFFER() {
                    pSample = outSample,
                };
                transform.ProcessOutput(0, 1, ref outputDataBuffer, out int status);

                outputDataBuffer.pSample.ConvertToContiguousBuffer(out IMFMediaBuffer outputBuffer);
                outputData.GetCurrentLength(out int outputBufferLength);

                outputData.Lock(out IntPtr pOutputData, out _, out _);
                byte[] resampledBuffer = new byte[outputBufferLength];
                Marshal.Copy(pOutputData, resampledBuffer, 0, outputBufferLength);
                outputData.Unlock();

                resampledPCM.Write(resampledBuffer, 0, resampledBuffer.Length);
            }

            transform.ProcessMessage(MFT_MESSAGE_NOTIFY_END_OF_STREAM, IntPtr.Zero);
            transform.ProcessMessage(MFT_MESSAGE_COMMAND_DRAIN, IntPtr.Zero);

            return resampledPCM.ToArray();
        }
    }
}