using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Threading;
using static FlowTimer.MMDeviceAPI;

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
        }

        public void Destroy() {
            if(Running) {
                Running = false;
                AudioClient.Stop();
            }
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

        public byte[] ToNativeFormat(byte[] pcm) {
            byte[] ret = pcm;
            // TODO: Unsure if this covers all platforms.
            if(Format.wFormatTag == WAVE_FORMAT_PCM || ExtensibleFormatTag == KSDATAFORMAT_SUBTYPE_PCM) {
                if(Format.wBitsPerSample == 16) {
                    // .wav files are already in this format, do nothing.
                } else if(Format.wBitsPerSample == 8) {
                    ret = new byte[pcm.Length / 2];
                    for(int i = 0; i < pcm.Length; i += 2) {
                        short shortSample = (short) (pcm[i] | (pcm[i + 1] << 8));
                        ushort normalizedSample = (ushort) (shortSample + 32768);
                        ret[i / 2] = (byte) (normalizedSample >> 8);
                    }
                }
            } else if(Format.wFormatTag == WAVE_FORMAT_IEEE_FLOAT || ExtensibleFormatTag == KSDATAFORMAT_SUBTYPE_IEEE_FLOAT) {
                // TODO: Maybe not assume 32bit?
                ret = new byte[pcm.Length * 2];
                for(int i = 0; i < pcm.Length; i += 2) {
                    short shortSample = (short) (pcm[i] | (pcm[i + 1] << 8));
                    float floatSample = (float) shortSample / (float) short.MaxValue;
                    if(floatSample < -1) floatSample = -1;
                    if(floatSample > 1) floatSample = 1;
                    byte[] bytes = BitConverter.GetBytes(floatSample);
                    ret[i * 2 + 0] = bytes[0];
                    ret[i * 2 + 1] = bytes[1];
                    ret[i * 2 + 2] = bytes[2];
                    ret[i * 2 + 3] = bytes[3];
                }
            }

            return ret;
        }

        public unsafe void GenerateSamples(IntPtr dest, int numBytes) {
            int length = Math.Min(numBytes, AudioBuffer.Length - AudioBufferPosition);
            for(int i = 0; i < length; i++) ((byte*) dest)[i] = AudioBuffer[AudioBufferPosition + i];
            for(int i = length; i < numBytes; i++) ((byte*) dest)[i] = 0;
            AudioBufferPosition += length;
        }
    }
}