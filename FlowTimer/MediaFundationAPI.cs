using System;
using System.Text;
using System.Runtime.InteropServices;

namespace FlowTimer {

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("2CD2D921-C447-44A7-A13C-4ADABFC247E3")]
    public interface IMFAttributes {
        void GetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr pValue);
        void GetItemType([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);
        void CompareItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);
        void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);
        void GetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);
        void GetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);
        void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);
        void GetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize, out int pcchLength);
        void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);
        void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);
        void GetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize, out int pcbBlobSize);
        void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);
        void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        void SetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr Value);
        void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);
        void DeleteAllItems();
        void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);
        void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);
        void SetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);
        void SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);
        void SetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        void SetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf, int cbBufSize);
        void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        void LockStore();
        void UnlockStore();
        void GetCount(out int pcItems);
        void GetItemByIndex(int unIndex, out Guid pGuidKey, IntPtr pValue);
        void CopyAllItems([MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
    public interface IMFActivate : IMFAttributes {
        new void GetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr pValue);
        new void GetItemType([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);
        new void CompareItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);
        new void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);
        new void GetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);
        new void GetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);
        new void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);
        new void GetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize, out int pcchLength);
        new void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);
        new void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);
        new void GetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize, out int pcbBlobSize);
        new void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);
        new void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        new void SetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr Value);
        new void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);
        new void DeleteAllItems();
        new void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);
        new void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);
        new void SetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);
        new void SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);
        new void SetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        new void SetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf, int cbBufSize);
        new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        new void LockStore();
        new void UnlockStore();
        new void GetCount(out int pcItems);
        new void GetItemByIndex(int unIndex, out Guid pGuidKey, IntPtr pValue);
        new void CopyAllItems([MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
        void ActivateObject([In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out object ppv);
        void ShutdownObject();
        void DetachObject();
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("44AE0FA8-EA31-4109-8D2E-4CAE4997C555")]
    public interface IMFMediaType : IMFAttributes {
        new void GetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr pValue);
        new void GetItemType([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);
        new void CompareItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);
        new void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);
        new void GetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);
        new void GetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);
        new void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);
        new void GetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize, out int pcchLength);
        new void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);
        new void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);
        new void GetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize, out int pcbBlobSize);
        new void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);
        new void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        new void SetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr Value);
        new void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);
        new void DeleteAllItems();
        new void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);
        new void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);
        new void SetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);
        new void SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);
        new void SetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        new void SetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf, int cbBufSize);
        new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        new void LockStore();
        new void UnlockStore();
        new void GetCount(out int pcItems);
        new void GetItemByIndex(int unIndex, out Guid pGuidKey, IntPtr pValue);
        new void CopyAllItems([MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
        void GetMajorType(out Guid pguidMajorType);
        void IsCompressedFormat([MarshalAs(UnmanagedType.Bool)] out bool pfCompressed);
        int IsEqual([In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pIMediaType, ref int pdwFlags);
        void GetRepresentation( Guid guidRepresentation, ref IntPtr ppvRepresentation);
        void FreeRepresentation( Guid guidRepresentation,  IntPtr pvRepresentation);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3")]
    public interface IMFMediaEvent : IMFAttributes {
        new void GetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr pValue);
        new void GetItemType([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);
        new void CompareItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);
        new void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);
        new void GetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);
        new void GetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);
        new void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);
        new void GetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize, out int pcchLength);
        new void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);
        new void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);
        new void GetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize, out int pcbBlobSize);
        new void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);
        new void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        new void SetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr Value);
        new void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);
        new void DeleteAllItems();
        new void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);
        new void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);
        new void SetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);
        new void SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);
        new void SetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        new void SetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf, int cbBufSize);
        new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        new void LockStore();
        new void UnlockStore();
        new void GetCount(out int pcItems);
        new void GetItemByIndex(int unIndex, out Guid pGuidKey, IntPtr pValue);
        new void CopyAllItems([MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
        void GetType( out int pmet);
        void GetExtendedType(out Guid pguidExtendedType);
        void GetStatus([MarshalAs(UnmanagedType.Error)] out int phrStatus);
        void GetValue(IntPtr pvValue);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4")]
    public interface IMFSample : IMFAttributes {
        new void GetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr pValue);
        new void GetItemType([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);
        new void CompareItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);
        new void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);
        new void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);
        new void GetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);
        new void GetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);
        new void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);
        new void GetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize, out int pcchLength);
        new void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);
        new void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);
        new void GetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize, out int pcbBlobSize);
        new void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);
        new void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        new void SetItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr Value);
        new void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);
        new void DeleteAllItems();
        new void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);
        new void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);
        new void SetDouble([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);
        new void SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);
        new void SetString([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        new void SetBlob([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf, int cbBufSize);
        new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        new void LockStore();
        new void UnlockStore();
        new void GetCount(out int pcItems);
        new void GetItemByIndex(int unIndex, out Guid pGuidKey, IntPtr pValue);
        new void CopyAllItems([MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
        void GetSampleFlags(out int pdwSampleFlags);
        void SetSampleFlags(int dwSampleFlags);
        void GetSampleTime(out long phnsSampletime);
        void SetSampleTime(long hnsSampleTime);
        void GetSampleDuration(out long phnsSampleDuration);
        void SetSampleDuration(long hnsSampleDuration);
        void GetBufferCount(out int pdwBufferCount);
        void GetBufferByIndex(int dwIndex, out IMFMediaBuffer ppBuffer);
        void ConvertToContiguousBuffer(out IMFMediaBuffer ppBuffer);
        void AddBuffer(IMFMediaBuffer pBuffer);
        void RemoveBufferByIndex(int dwIndex);
        void RemoveAllBuffers();
        void GetTotalLength(out int pcbTotalLength);
        void CopyToBuffer(IMFMediaBuffer pBuffer);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
    public interface IMFMediaBuffer {
        void Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);
        void Unlock();
        void GetCurrentLength(out int pcbCurrentLength);
        void SetCurrentLength(int cbCurrentLength);
        void GetMaxLength(out int pcbMaxLength);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("bf94c121-5b05-4e6f-8000-ba598961414d")]
    public interface IMFTransform {
        void GetStreamLimits(out int pdwInputMinimum, out int pdwInputMaximum, out int pdwOutputMinimum, out int pdwOutputMaximum);
        void GetStreamCount(out int pcInputStreams, out int pcOutputStreams);
        void GetStreamIds(int dwInputIdArraySize, IntPtr pdwInputIDs, int dwOutputIdArraySize, IntPtr pdwOutputIDs);
        void GetInputStreamInfo(int dwInputStreamId, out MFT_INPUT_STREAM_INFO pStreamInfo);
        void GetOutputStreamInfo(int dwOutputStreamId, out MFT_OUTPUT_STREAM_INFO pStreamInfo);
        void GetAttributes(out IMFAttributes pAttributes);
        void GetInputStreamAttributes(int dwInputStreamId, out IMFAttributes pAttributes);
        void GetOutputStreamAttributes(int dwOutputStreamId, out IMFAttributes pAttributes);
        void DeleteInputStream(int dwOutputStreamId);
        void AddInputStreams(int cStreams, IntPtr adwStreamIDs);
        void GetInputAvailableType(int dwInputStreamId, int dwTypeIndex, out IMFMediaType ppType);
        void GetOutputAvailableType(int dwOutputStreamId, int dwTypeIndex, out IMFMediaType ppType);
        void SetInputType(int dwInputStreamId, IMFMediaType pType, int dwFlags);
        void SetOutputType(int dwOutputStreamId, IMFMediaType pType, int dwFlags);
        void GetInputCurrentType(int dwInputStreamId, out IMFMediaType ppType);
        void GetOutputCurrentType(int dwOutputStreamId, out IMFMediaType ppType);
        void GetInputStatus(int dwInputStreamId, out int pdwFlags);
        void GetOutputStatus(int dwInputStreamId, out int pdwFlags);
        void SetOutputBounds(long hnsLowerBound, long hnsUpperBound);
        void ProcessEvent(int dwInputStreamId, IMFMediaEvent pEvent);
        void ProcessMessage(int eMessage, IntPtr ulParam);
        void ProcessInput(int dwInputStreamId, IMFSample pSample, int dwFlags);
        int ProcessOutput(int dwFlags, int cOutputBufferCount, ref MFT_OUTPUT_DATA_BUFFER pOutputSamples, out int pdwStatus);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
    public interface IMFCollection {
        void GetElementCount(out int pcElements);
        void GetElement(int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);
        void AddElement([MarshalAs(UnmanagedType.IUnknown)] object pUnkElement);
        void RemoveElement(int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);
        void InsertElementAt(int dwIndex, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        void RemoveAllElements();
    }

    [Guid("E7E9984F-F09F-4da4-903F-6E2E0EFE56B5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMResamplerProps {
        int SetHalfFilterLength(int outputQuality);
        int SetUserChannelMtx(float[] channelConversionMatrix);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MFT_REGISTER_TYPE_INFO {

        public Guid guidMajorType;
        public Guid guidSubtype;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_INPUT_STREAM_INFO {

        public long hnsMaxLatency;
        public int dwFlags;
        public int cbSize;
        public int cbMaxLookahead;
        public int cbAlignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_OUTPUT_STREAM_INFO {

        public int dwFlags;
        public int cbSize;
        public int cbAlignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_OUTPUT_DATA_BUFFER {

        public int dwStreamID;
        public IMFSample pSample;
        public int dwStatus;
        public IMFCollection pEvents;
    }

    public static class MediaFundationAPI {

        public const int MF_SDK_VERSION = 0x2;
        public const int MF_API_VERSION = 0x70;

        public const int MFT_ENUM_FLAG_SYNCMFT = 0x00000001;
        public const int MFT_ENUM_FLAG_ASYNCMFT = 0x00000002;
        public const int MFT_ENUM_FLAG_HARDWARE = 0x00000004;
        public const int MFT_ENUM_FLAG_FIELDOFUSE = 0x00000008;
        public const int MFT_ENUM_FLAG_LOCALMFT = 0x00000010;
        public const int MFT_ENUM_FLAG_TRANSCODE_ONLY = 0x00000020;
        public const int MFT_ENUM_FLAG_SORTANDFILTER = 0x00000040;
        public const int MFT_ENUM_FLAG_ALL = 0x0000003;

        public const int MFT_OUTPUT_STREAM_WHOLE_SAMPLES = 0x00000001;
        public const int MFT_OUTPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 0x00000002;
        public const int MFT_OUTPUT_STREAM_FIXED_SAMPLE_SIZE = 0x00000004;
        public const int MFT_OUTPUT_STREAM_DISCARDABLE = 0x00000008;
        public const int MFT_OUTPUT_STREAM_OPTIONAL = 0x00000010;
        public const int MFT_OUTPUT_STREAM_PROVIDES_SAMPLES = 0x00000100;
        public const int MFT_OUTPUT_STREAM_CAN_PROVIDE_SAMPLES = 0x00000200;
        public const int MFT_OUTPUT_STREAM_LAZY_READ = 0x00000400;
        public const int MFT_OUTPUT_STREAM_REMOVABLE = 0x00000800;

        public const int MFT_INPUT_STREAM_WHOLE_SAMPLES = 0x00000001;
        public const int MFT_INPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 0x00000002;
        public const int MFT_INPUT_STREAM_FIXED_SAMPLE_SIZE = 0x00000004;
        public const int MFT_INPUT_STREAM_HOLDS_BUFFERS = 0x00000008;
        public const int MFT_INPUT_STREAM_DOES_NOT_ADDREF = 0x00000100;
        public const int MFT_INPUT_STREAM_REMOVABLE = 0x00000200;
        public const int MFT_INPUT_STREAM_OPTIONAL = 0x00000400;
        public const int MFT_INPUT_STREAM_PROCESSES_IN_PLACE = 0x00000800;

        public const int MFT_SET_TYPE_TEST_ONLY = 0x00000001;
        public const int MFT_INPUT_STATUS_ACCEPT_DATA = 0x00000001;
        public const int MFT_OUTPUT_STATUS_SAMPLE_READY = 0x00000001;

        public const int MFT_MESSAGE_COMMAND_FLUSH = 0x00000000;
        public const int MFT_MESSAGE_COMMAND_DRAIN = 0x00000001;
        public const int MFT_MESSAGE_SET_D3D_MANAGER = 0x00000002;
        public const int MFT_MESSAGE_DROP_SAMPLES = 0x00000003;
        public const int MFT_MESSAGE_COMMAND_TICK = 0x00000004;
        public const int MFT_MESSAGE_NOTIFY_BEGIN_STREAMING = 0x10000000;
        public const int MFT_MESSAGE_NOTIFY_END_STREAMING = 0x10000001;
        public const int MFT_MESSAGE_NOTIFY_END_OF_STREAM = 0x10000002;
        public const int MFT_MESSAGE_NOTIFY_START_OF_STREAM = 0x10000003;
        public const int MFT_MESSAGE_COMMAND_MARKER = 0x20000000;

        public const int MEUnknown = 0;
        public const int MEError = 1;
        public const int MEExtendedType = 2;
        public const int MENonFatalError = 3;
        public const int MESessionUnknown = 100;
        public const int MESessionTopologySet = 101;
        public const int MESessionTopologiesCleared = 102;
        public const int MESessionStarted = 103;
        public const int MESessionPaused = 104;
        public const int MESessionStopped = 105;
        public const int MESessionClosed = 106;
        public const int MESessionEnded = 107;
        public const int MESessionRateChanged = 108;
        public const int MESessionScrubSampleComplete = 109;
        public const int MESessionNotifyPresentationTime = 112;
        public const int MENewPresentation = 113;
        public const int MELicenseAcquisitionStart = 114;
        public const int MELicenseAcquisitionCompleted = 115;
        public const int MEIndividualizationStart = 116;
        public const int MEIndividualizationCompleted = 117;
        public const int MEEnablerProgress = 118;
        public const int MEEnablerCompleted = 119;
        public const int MEPolicyError = 120;
        public const int MEPolicyReport = 121;
        public const int MEBufferingStarted = 122;
        public const int MEBufferingStopped = 123;
        public const int MEConnectStart = 124;
        public const int MEConnectEnd = 125;
        public const int MEReconnectStart = 126;
        public const int MEReconnectEnd = 127;
        public const int MERendererEvent = 128;
        public const int MESessionStreamSinkFormatChanged = 129;
        public const int MESourceUnknown = 200;
        public const int MESourceStarted = 201;
        public const int MEStreamStarted = 202;
        public const int MESourceSeeked = 203;
        public const int MEStreamSeeked = 204;
        public const int MENewStream = 205;
        public const int MEUpdatedStream = 206;
        public const int MESourceStopped = 207;
        public const int MEStreamStopped = 208;
        public const int MESourcePaused = 209;
        public const int MEStreamPaused = 210;
        public const int MEEndOfPresentation = 211;
        public const int MEEndOfStream = 212;
        public const int MEMediaSample = 213;
        public const int MEStreamTick = 214;
        public const int MEStreamThinMode = 215;
        public const int MEStreamFormatChanged = 216;
        public const int MESourceRateChanged = 217;
        public const int MEEndOfPresentationSegment = 218;
        public const int MESourceCharacteristicsChanged = 219;
        public const int MESourceRateChangeRequested = 220;
        public const int MESourceMetadataChanged = 221;
        public const int MESequencerSourceTopologyUpdated = 222;
        public const int MESinkUnknown = 300;
        public const int MEStreamSinkStarted = 301;
        public const int MEStreamSinkStopped = 302;
        public const int MEStreamSinkPaused = 303;
        public const int MEStreamSinkRateChanged = 304;
        public const int MEStreamSinkRequestSample = 305;
        public const int MEStreamSinkMarker = 306;
        public const int MEStreamSinkPrerolled = 307;
        public const int MEStreamSinkScrubSampleComplete = 308;
        public const int MEStreamSinkFormatChanged = 309;
        public const int MEStreamSinkDeviceChanged = 310;
        public const int MEQualityNotify = 311;
        public const int MESinkInvalidated = 312;
        public const int MEAudioSessionNameChanged = 313;
        public const int MEAudioSessionVolumeChanged = 314;
        public const int MEAudioSessionDeviceRemoved = 315;
        public const int MEAudioSessionServerShutdown = 316;
        public const int MEAudioSessionGroupingParamChanged = 317;
        public const int MEAudioSessionIconChanged = 318;
        public const int MEAudioSessionFormatChanged = 319;
        public const int MEAudioSessionDisconnected = 320;
        public const int MEAudioSessionExclusiveModeOverride = 321;
        public const int METrustUnknown = 400;
        public const int MEPolicyChanged = 401;
        public const int MEContentProtectionMessage = 402;
        public const int MEPolicySet = 403;
        public const int MEWMDRMLicenseBackupCompleted = 500;
        public const int MEWMDRMLicenseBackupProgress = 501;
        public const int MEWMDRMLicenseRestoreCompleted = 502;
        public const int MEWMDRMLicenseRestoreProgress = 503;
        public const int MEWMDRMLicenseAcquisitionCompleted = 506;
        public const int MEWMDRMIndividualizationCompleted = 508;
        public const int MEWMDRMIndividualizationProgress = 513;
        public const int MEWMDRMProximityCompleted = 514;
        public const int MEWMDRMLicenseStoreCleaned = 515;
        public const int MEWMDRMRevocationDownloadCompleted = 516;
        public const int METransformUnknown = 600;
        public const int METransformNeedInput = (METransformUnknown + 1);
        public const int METransformHaveOutput = (METransformNeedInput + 1);
        public const int METransformDrainComplete = (METransformHaveOutput + 1);
        public const int METransformMarker = (METransformDrainComplete + 1);

        public const int MFT_PROCESS_OUTPUT_DISCARD_WHEN_NO_BUFFER = 0x00000001;
        public const int MFT_PROCESS_OUTPUT_REGENERATE_LAST_OUTPUT = 0x00000002;
        public const int MFT_PROCESS_OUTPUT_STATUS_NEW_STREAMS = 0x00000001;

        public const int MFT_OUTPUT_DATA_BUFFER_INCOMPLETE = 0x01000000;
        public const int MFT_OUTPUT_DATA_BUFFER_FORMAT_CHANGE = 0x00000100;
        public const int MFT_OUTPUT_DATA_BUFFER_STREAM_END = 0x00000200;
        public const int MFT_OUTPUT_DATA_BUFFER_NO_SAMPLE = 0x00000300;

        public static Guid MFT_CATEGORY_VIDEO_DECODER = new Guid("{d6c02d4b-6833-45b4-971a-05a4b04bab91}");
        public static Guid MFT_CATEGORY_VIDEO_ENCODER = new Guid("{f79eac7d-e545-4387-bdee-d647d7bde42a}");
        public static Guid MFT_CATEGORY_VIDEO_EFFECT = new Guid("{12e17c21-532c-4a6e-8a1c-40825a736397}");
        public static Guid MFT_CATEGORY_MULTIPLEXER = new Guid("{059c561e-05ae-4b61-b69d-55b61ee54a7b}");
        public static Guid MFT_CATEGORY_DEMULTIPLEXER = new Guid("{a8700a7a-939b-44c5-99d7-76226b23b3f1}");
        public static Guid MFT_CATEGORY_AUDIO_DECODER = new Guid("{9ea73fb4-ef7a-4559-8d5d-719d8f0426c7}");
        public static Guid MFT_CATEGORY_AUDIO_ENCODER = new Guid("{91c64bd0-f91e-4d8c-9276-db248279d975}");
        public static Guid MFT_CATEGORY_AUDIO_EFFECT = new Guid("{11064c48-3648-4ed0-932e-05ce8ac811b7}");
        public static Guid MFT_CATEGORY_VIDEO_PROCESSOR = new Guid("{302EA3FC-AA5F-47f9-9F7A-C2188BB16302}");
        public static Guid MFT_CATEGORY_OTHER = new Guid("{90175d57-b7ea-4901-aeb3-933a8747756f}");
        public static Guid CLSID_CResamplerMediaObject = new Guid("{f447b69e-1884-4a7e-8055-346f74d6edb3}");
        public static Guid CLSID_IMFTransform = new Guid("{bf94c121-5b05-4e6f-8000-ba598961414d}");
        public static Guid MF_TRANSFORM_ASYNC = new Guid("f81a699a-649a-497d-8c73-29f8fed6ad7a");
        public static Guid MF_TRANSFORM_ASYNC_UNLOCK = new Guid("e5666d6b-3422-4eb6-a421-da7db1f8e207");
        public static Guid MF_TRANSFORM_FLAGS_Attribute = new Guid("9359bb7e-6275-46c4-a025-1c01e45f1a86");
        public static Guid MF_TRANSFORM_CATEGORY_Attribute = new Guid("ceabba49-506d-4757-a6ff-66c184987e4e");
        public static Guid MFT_TRANSFORM_CLSID_Attribute = new Guid("6821c42b-65a4-4e82-99bc-9a88205ecd0c");
        public static Guid MFT_INPUT_TYPES_Attributes = new Guid("4276c9b1-759d-4bf3-9cd0-0d723d138f96");
        public static Guid MFT_OUTPUT_TYPES_Attributes = new Guid("8eae8cf3-a44f-4306-ba5c-bf5dda242818");
        public static Guid MFT_ENUM_HARDWARE_URL_Attribute = new Guid("2fb866ac-b078-4942-ab6c-003d05cda674");
        public static Guid MFT_FRIENDLY_NAME_Attribute = new Guid("314ffbae-5b41-4c95-9c19-4e7d586face3");
        public static Guid MFT_CONNECTED_STREAM_ATTRIBUTE = new Guid("71eeb820-a59f-4de2-bcec-38db1dd611a4");
        public static Guid MFT_CONNECTED_TO_HW_STREAM = new Guid("34e6e728-06d6-4491-a553-4795650db912");
        public static Guid MFT_PREFERRED_OUTPUTTYPE_Attribute = new Guid("7e700499-396a-49ee-b1b4-f628021e8c9d");
        public static Guid MFT_PROCESS_LOCAL_Attribute = new Guid("543186e4-4649-4e65-b588-4aa352aff379");
        public static Guid MFT_PREFERRED_ENCODER_PROFILE = new Guid("53004909-1ef5-46d7-a18e-5a75f8b5905f");
        public static Guid MFT_HW_TIMESTAMP_WITH_QPC_Attribute = new Guid("8d030fb8-cc43-4258-a22e-9210bef89be4");
        public static Guid MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid("8ec2e9fd-9148-410d-831e-702439461a8e");
        public static Guid MFT_CODEC_MERIT_Attribute = new Guid("88a7cb15-7b07-4a34-9128-e64c6703c4d3");
        public static Guid MFT_ENUM_TRANSCODE_ONLY_ATTRIBUTE = new Guid("111ea8cd-b62a-4bdb-89f6-67ffcdc2458b");
        public static Guid MF_PD_PMPHOST_CONTEXT = new Guid("6c990d31-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_APP_CONTEXT = new Guid("6c990d32-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_DURATION = new Guid("6c990d33-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_TOTAL_FILE_SIZE = new Guid("6c990d34-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_AUDIO_ENCODING_BITRATE = new Guid("6c990d35-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_VIDEO_ENCODING_BITRATE = new Guid("6c990d36-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_MIME_TYPE = new Guid("6c990d37-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_LAST_MODIFIED_TIME = new Guid("6c990d38-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_PLAYBACK_ELEMENT_ID = new Guid("6c990d39-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_PREFERRED_LANGUAGE = new Guid("6c990d3a-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_PLAYBACK_BOUNDARY_TIME = new Guid("6c990d3b-bb8e-477a-8598-0d5d96fcd88a");
        public static Guid MF_PD_AUDIO_ISVARIABLEBITRATE = new Guid("33026ee0-e387-4582-ae0a-34a2ad3baa18");
        public static Guid MF_MT_MAJOR_TYPE = new Guid("48eba18e-f8c9-4687-bf11-0a74c9f96a8f");
        public static Guid MF_MT_SUBTYPE = new Guid("f7e34c9a-42e8-4714-b74b-cb29d72c35e5");
        public static Guid MF_MT_AUDIO_BLOCK_ALIGNMENT = new Guid("322de230-9eeb-43bd-ab7a-ff412251541d");
        public static Guid MF_MT_AUDIO_AVG_BYTES_PER_SECOND = new Guid("1aab75c8-cfef-451c-ab95-ac034b8e1731");
        public static Guid MF_MT_AUDIO_NUM_CHANNELS = new Guid("37e48bf5-645e-4c5b-89de-ada9e29b696a");
        public static Guid MF_MT_AUDIO_SAMPLES_PER_SECOND = new Guid("5faeeae7-0290-4c31-9e8a-c534f68d9dba");
        public static Guid MF_MT_AUDIO_BITS_PER_SAMPLE = new Guid("f2deb57f-40fa-4764-aa33-ed4f2d1ff669");
        public static Guid MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS = new Guid("a634a91c-822b-41b9-a494-4de4643612b0");
        public static Guid MF_MT_USER_DATA = new Guid("b6bc765f-4c3b-40a4-bd51-2535b66fe09d");
        public static Guid MF_MT_ALL_SAMPLES_INDEPENDENT = new Guid("c9173739-5e56-461c-b713-46fb995cb95f");
        public static Guid MF_MT_FIXED_SIZE_SAMPLES = new Guid("b8ebefaf-b718-4e04-b0a9-116775e3321b");
        public static Guid MF_MT_AM_FORMAT_TYPE = new Guid("73d1072d-1870-4174-a063-29ff4ff6c11e");
        public static Guid MF_MT_AUDIO_PREFER_WAVEFORMATEX = new Guid("a901aaba-e037-458a-bdf6-545be2074042");
        public static Guid MF_MT_COMPRESSED = new Guid("3afd0cee-18f2-4ba5-a110-8bea502e1f92");
        public static Guid MF_MT_AVG_BITRATE = new Guid("20332624-fb0d-4d9e-bd0d-cbf6786c102e");
        public static Guid MF_MT_AAC_PAYLOAD_TYPE = new Guid("bfbabe79-7434-4d1c-94f0-72a3b9e17188");
        public static Guid MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION = new Guid("7632f0e6-9538-4d61-acda-ea29c8c14456");
        public static Guid MFMediaType_Audio = new Guid(0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        public static Guid MFAudioFormat_PCM = new Guid(0x0001, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        [DllImport("mfplat.dll")]
        public static extern int MFStartup(int version, int dwFlags);

        [DllImport("mfplat.dll")]
        public static extern void MFShutdown();

        [DllImport("mfplat.dll")]
        public static extern void MFTEnumEx(Guid guidCategory, int flags, MFT_REGISTER_TYPE_INFO pInputType, MFT_REGISTER_TYPE_INFO pOutputType, out IntPtr pppMFTActivate, out int pcMFTActivate);

        [DllImport("mfplat.dll")]
        public static extern void MFCreateMediaType(out IMFMediaType ppMFType);

        [DllImport("mfplat.dll")]
        public static extern void MFCreateMemoryBuffer(int cbMaxLength, out IMFMediaBuffer ppBuffer);

        [DllImport("mfplat.dll")]
        public static extern void MFCreateSample(out IMFSample ppIMFSample);
    }
}
