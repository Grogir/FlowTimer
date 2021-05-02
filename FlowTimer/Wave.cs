﻿using System.IO;
using System.Runtime.InteropServices;

namespace FlowTimer {

    public static class Wave {

        private static readonly uint WaveId_RIFF = MakeRiff("RIFF");
        private static readonly uint WaveId_WAVE = MakeRiff("WAVE");
        private static readonly uint WaveId_data = MakeRiff("data");
        private static readonly uint WaveId_fmt = MakeRiff("fmt ");

        private static uint MakeRiff(string str) {
            return (uint) ((str[3] << 24) | (str[2] << 16) | (str[1] << 8) | str[0]);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct WaveHeader {

            public uint RiffId;
            public uint Size;
            public uint WaveId;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct WaveChunkHeader {

            public uint Id;
            public uint Size;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct WaveFmt {

            public ushort FormatTag;
            public ushort Channels;
            public uint SampleRate;
            public uint AvgBytesPerSec;
            public ushort BlockAlign;
            public ushort BitsPerSample;
        }

        public static bool LoadWAV(string fileName, out byte[] pcm) {
            pcm = new byte[0];

            byte[] bytes = File.ReadAllBytes(fileName);
            int pointer = 0;

            WaveHeader header = bytes.Consume<WaveHeader>(ref pointer);
            if(header.RiffId != WaveId_RIFF) return false;
            if(header.WaveId != WaveId_WAVE) return false;

            while(pointer < bytes.Length) {
                WaveChunkHeader chunkHeader = bytes.Consume<WaveChunkHeader>(ref pointer);

                if(chunkHeader.Id == WaveId_fmt) {
                    WaveFmt fmt = bytes.ReadStruct<WaveFmt>(pointer);
                } else if(chunkHeader.Id == WaveId_data) {
                    pcm = bytes.Subarray(pointer, (int) chunkHeader.Size);
                }

                pointer += (int) chunkHeader.Size;
            }

            return true;
        }
    }
}