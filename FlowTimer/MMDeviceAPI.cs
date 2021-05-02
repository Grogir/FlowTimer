using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using static FlowTimer.MMDeviceAPI;

namespace FlowTimer {

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WAVEFORMATEX {

        public ushort wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct WAVEFORMATEXTENSIBLE {

        [FieldOffset(0x00)] public WAVEFORMATEX Format;
        [FieldOffset(0x12)] public short wValidBitsPerSample;
        [FieldOffset(0x12)] public short wSamplesPerBlock;
        [FieldOffset(0x12)] public short wValidBitsPewReservedrSample;
        [FieldOffset(0x14)] public int dwChannelMask;
        [FieldOffset(0x18)] public Guid SubFormat;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AudioClientProperties {

        public int cbSize;
        public bool bIsOffload;
        public AudioStreamCategory eCategory;
        public int eOptions;
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice {

        int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
    }

    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection {

        int GetCount(out int pcDevices);
        int Item(int nDevice, out IMMDevice ppDevice);
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator {

        int EnumAudioEndpoints(DataFlow dataFlow, int dwStateMask, out IMMDeviceCollection ppDevices);
        int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice ppEndPoint);
    }

    [Guid("7ED4EE07-8E67-4CD4-8C1A-2B7A5987AD42"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioClient3 {

        int Initialize(int shareMode, int streamFlags, long hnsBufferDuration, long hnsPeriodicity, IntPtr pFormat, IntPtr audioSessionGuid);
        int GetBufferSize(out int bufferSize);
        [return: MarshalAs(UnmanagedType.I8)] long GetStreamLatency();
        int GetCurrentPadding(out int currentPadding);
        int IsFormatSupported(int shareMode, IntPtr pFormat, out IntPtr closestMatchFormat);
        int GetMixFormat(out IntPtr deviceFormatPointer);
        int GetDevicePeriod(out long defaultDevicePeriod, out long minimumDevicePeriod);
        int Start();
        int Stop();
        int Reset();
        int SetEventHandle(IntPtr eventHandle);
        int GetService([MarshalAs(UnmanagedType.LPStruct)] Guid interfaceId, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);
        void IsOffloadCapable(int category, out bool pbOffloadCapable);
        int SetClientProperties(AudioClientProperties pProperties);
        void GetBufferSizeLimits(IntPtr pFormat, bool bEventDriven, out long phnsMinBufferDuration, out long phnsMaxBufferDuration);
        int GetSharedModeEnginePeriod(IntPtr pFormat, out int pDefaultPeriodInFrames, out int pFundementalPeriodInFrames, out int pMinPeriodInFrames, out int pMaxPeriodInFrames);
        int GetCurrentSharedModeEnginePeriod(out IntPtr ppFormat, out int pCurrentPeriodInFrames);
        int InitializeSharedAudioStream(int streamFlags, int periodInFrames, IntPtr pFormat, IntPtr audioSessionGuid);
    }

    [Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioRenderClient {

        int GetBuffer(int numFramesRequested, out IntPtr dataBufferPointer);
        int ReleaseBuffer(int numFramesWritten, int bufferFlags);
    }

    public class MMDevice {

        private IMMDevice Interface;

        public MMDevice(IMMDevice interfaceIn) => Interface = interfaceIn;

        public AudioClient CreateAudioCilent() {
            Interface.Activate(ref IAudioClient3ID, CLSCTX_ALL, IntPtr.Zero, out object audioClientInterface);
            return new AudioClient(audioClientInterface as IAudioClient3);
        }
    }

    public class MMDeviceCollection : IEnumerable<MMDevice> {

        private IMMDeviceCollection Interface;

        public MMDeviceCollection(IMMDeviceCollection interfaceIn) => Interface = interfaceIn;

        public int Count {
            get {
                Interface.GetCount(out int result);
                return result;
            }
        }

        public MMDevice this[int index] {
            get {
                Interface.Item(index, out IMMDevice result);
                return new MMDevice(result);
            }
        }

        public IEnumerator<MMDevice> GetEnumerator() {
            for(int i = 0; i < Count; i++) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    public class MMDeviceEnumerator {

        private IMMDeviceEnumerator Interface;

        public MMDeviceEnumerator(IMMDeviceEnumerator interfaceIn) => Interface = interfaceIn;

        public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, int dwStateMask) {
            Interface.EnumAudioEndpoints(dataFlow, dwStateMask, out IMMDeviceCollection result);
            return new MMDeviceCollection(result);
        }

        public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role) {
            Interface.GetDefaultAudioEndpoint(dataFlow, role, out IMMDevice result);
            return new MMDevice(result);
        }
    }

    public class AudioClient {

        private IAudioClient3 Interface;

        public AudioClient(IAudioClient3 interfaceIn) => Interface = interfaceIn;

        public IntPtr MixFormat {
            get {
                Interface.GetMixFormat(out IntPtr result);
                return result;
            }
        }

        public int BufferSize {
            get {
                Interface.GetBufferSize(out int result);
                return result;
            }
        }

        public int CurrentPadding {
            get {
                Interface.GetCurrentPadding(out int result);
                return result;
            }
        }

        public AudioRenderClient CreateRenderClient() {
            Interface.GetService(IAudioRenderClientID, out object audioRenderClientInterface);
            return new AudioRenderClient(audioRenderClientInterface as IAudioRenderClient);
        }

        public void Initialize(int mode, int flags, int bufferDuration, int bufferPeriodicity, IntPtr format, IntPtr sessionGuid) {
            Interface.Initialize(mode, flags, bufferDuration, bufferPeriodicity, format, sessionGuid);
        }

        public void InitializeSharedAudioStream(int flags, int bufferPeriodicity, IntPtr format, IntPtr sessionGuid) {
            Interface.InitializeSharedAudioStream(flags, bufferPeriodicity, format, sessionGuid);
        }

        public void Start() {
            Interface.Start();
        }

        public void Stop() {
            Interface.Stop();
        }

        public void Reset() {
            Interface.Reset();
        }

        public void SetEventHandle(IntPtr handle) {
            Interface.SetEventHandle(handle);
        }

        public void SetProperties(AudioClientProperties properties) {
            Interface.SetClientProperties(properties);
        }

        public void IsFormatSupported(int shareMode, IntPtr format, out IntPtr closestMatch) {
            Interface.IsFormatSupported(shareMode, format, out closestMatch);
        }

        public void GetDevicePeriod(out long defaultDevicePeriod, out long minimumDevicePeriod) {
            Interface.GetDevicePeriod(out defaultDevicePeriod, out minimumDevicePeriod);
        }

        public void GetSharedModeEnginePeriod(IntPtr format, out int defaultPeriod, out int fundementalPeriod, out int minPeriod, out int maxPeriod) {
            Interface.GetSharedModeEnginePeriod(format, out defaultPeriod, out fundementalPeriod, out minPeriod, out maxPeriod);
        }

        public void GetCurrentSharedModeEnginePeriod(out IntPtr format, out int currentPeriodInFrames) {
            Interface.GetCurrentSharedModeEnginePeriod(out format, out currentPeriodInFrames);
        }
    }

    public class AudioRenderClient {

        private IAudioRenderClient Interface;

        public AudioRenderClient(IAudioRenderClient interfaceIn) => Interface = interfaceIn;

        public IntPtr GetBuffer(int sampleCount) {
            Interface.GetBuffer(sampleCount, out IntPtr ptr);
            return ptr;
        }

        public void ReleaseBuffer(int samplesWritten, int flags) {
            Interface.ReleaseBuffer(samplesWritten, flags);
        }
    }


    public enum DataFlow : int {

        Render,
        Capture,
        All,
    }

    public enum Role : int {

        Console,
        Multimedia,
        Communications,
    }

    public enum AudioStreamCategory : int {

        Other,
        ForegroundOnlyMedia,
        BackgroundCapableMedia,
        Communications,
        Alerts,
        SoundEffects,
        GameEffects,
        GameMedia,
        GameChat,
        Speech,
        Movie,
        Media,
    }

    public static class MMDeviceAPI {

        public static Guid MMDeviceEnumeratorID = new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E");
        public static Guid IAudioClient3ID = new Guid("7ED4EE07-8E67-4CD4-8C1A-2B7A5987AD42");
        public static Guid IAudioRenderClientID = new Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2");

        public static Guid KSDATAFORMAT_SUBTYPE_PCM = new Guid(0x00000001, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);
        public static Guid KSDATAFORMAT_SUBTYPE_IEEE_FLOAT = new Guid(0x00000003, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

        public const int DEVICE_STATE_ACTIVATE = 0x1;
        public const int DEVICE_STATE_DISABLED = 0x2;
        public const int DEVICE_STATE_NOTPRESENT = 0x4;
        public const int DEVICE_STATE_UNPLUGGED = 0x8;

        public const int AUDCLNT_SHAREMODE_SHARED = 0x0;
        public const int AUDCLNT_SHAREMODE_EXCLUSIVE = 0x1;
        public const int AUDCLNT_STREAMFLAGS_CROSSPROCESS = 0x00010000;
        public const int AUDCLNT_STREAMFLAGS_LOOPBACK = 0x00020000;
        public const int AUDCLNT_STREAMFLAGS_EVENTCALLBACK = 0x00040000;
        public const int AUDCLNT_STREAMFLAGS_NOPERSIST = 0x00080000;
        public const int AUDCLNT_STREAMFLAGS_RATEADJUST = 0x00100000;
        public const int AUDCLNT_STREAMFLAGS_AUTOCONVERTPCM = unchecked((int) 0x80000000);
        public const int AUDCLNT_STREAMFLAGS_SRC_DEFAULT_QUALITY = 0x08000000;
        public const int AUDCLNT_STREAMOPTIONS_NONE = 0x0;
        public const int AUDCLNT_STREAMOPTIONS_RAW = 0x1;
        public const int AUDCLNT_STREAMOPTIONS_MATCH_FORMAT = 0x2;
        public const int AUDCLNT_STREAMOPTIONS_AMBISONICS = 0x4;

        public const int CLSCTX_INPROC_SERVER = 0x1;
        public const int CLSCTX_INPROC_HANDLER = 0x2;
        public const int CLSCTX_LOCAL_SERVER = 0x4;
        public const int CLSCTX_INPROC_SERVER16 = 0x8;
        public const int CLSCTX_REMOTE_SERVER = 0x10;
        public const int CLSCTX_INPROC_HANDLER16 = 0x20;
        public const int CLSCTX_RESERVED1 = 0x40;
        public const int CLSCTX_RESERVED2 = 0x80;
        public const int CLSCTX_RESERVED3 = 0x100;
        public const int CLSCTX_RESERVED4 = 0x200;
        public const int CLSCTX_NO_CODE_DOWNLOAD = 0x400;
        public const int CLSCTX_RESERVED5 = 0x800;
        public const int CLSCTX_NO_CUSTOM_MARSHAL = 0x1000;
        public const int CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000;
        public const int CLSCTX_NO_FAILURE_LOG = 0x4000;
        public const int CLSCTX_DISABLE_AAA = 0x8000;
        public const int CLSCTX_ENABLE_AAA = 0x10000;
        public const int CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000;
        public const int CLSCTX_ACTIVATE_32_BIT_SERVER = 0x40000;
        public const int CLSCTX_ACTIVATE_64_BIT_SERVER = 0x80000;
        public const int CLSCTX_ENABLE_CLOAKING = 0x100000;
        public const int CLSCTX_PS_DLL = unchecked((int) 0x80000000);
        public const int CLSCTX_INPROC = CLSCTX_INPROC_SERVER | CLSCTX_INPROC_HANDLER;
        public const int CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER | CLSCTX_REMOTE_SERVER;
        public const int CLSCTX_ALL = CLSCTX_SERVER | CLSCTX_INPROC_HANDLER;

        public const int WAVE_FORMAT_UNKNOWN = 0x0;
        public const int WAVE_FORMAT_PCM = 0x1;
        public const int WAVE_FORMAT_IEEE_FLOAT = 0x3;
        public const int WAVE_FORMAT_EXTENSIBLE = 0xFFFE;
    }
}