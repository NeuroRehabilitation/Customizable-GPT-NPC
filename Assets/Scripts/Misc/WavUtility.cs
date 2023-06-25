using System;
using System.IO;
using UnityEngine;

public static class WavUtility
{
    // convert two bytes to one float in the range -1 to 1
    static float bytesToFloat(byte firstByte, byte secondByte)
    {
        // convert two bytes to one short (little endian)
        short s = (short)((secondByte << 8) | firstByte);
        // convert to range from -1 to (just below) 1
        return s / 32768.0F;
    }

    static int bytesToInt(byte[] bytes, int offset = 0)
    {
        int value = 0;
        for (int i = 0; i < 4; i++)
        {
            value |= ((int)bytes[offset + i]) << (i * 8);
        }
        return value;
    }

    // Returns left and right float arrays. 'right' will be null if sound is mono.
    public static void FromAudioClip(AudioClip clip, out float[] left, out float[] right)
    {
        int hz = clip.frequency;
        float[] data = new float[clip.samples * clip.channels];
        clip.GetData(data, 0);
        Convert(data, out left, out right);
    }

    static void Convert(float[] clipData, out float[] left, out float[] right)
    {
        left = new float[clipData.Length / 2];
        right = new float[clipData.Length / 2];
        for (int i = 0; i < clipData.Length / 2; i++)
        {
            left[i] = clipData[2 * i];
            right[i] = clipData[2 * i + 1];
        }
    }

    public static byte[] FromAudioClip(AudioClip clip)
    {
        float[] left, right;
        FromAudioClip(clip, out left, out right);
        return ToByteArray(left, right, clip.frequency);
    }

    static byte[] ToByteArray(float[] left, float[] right, int hz)
    {
        MemoryStream output = new MemoryStream();

        BinaryWriter writer = new BinaryWriter(output);

        short two = 2;
        int subchunk1Size = 16; // 24 byte header
        short audioFormat = 1;
        short numChannels = 2;
        int sampleRate = hz;
        short bitsPerSample = 16;
        short byteRate = (short)(sampleRate * numChannels * bitsPerSample / 8);
        short blockAlign = (short)(numChannels * bitsPerSample / 8);
        int dataSize = (left.Length + right.Length) * (bitsPerSample / 8);

        writer.Write(new char[4] { 'R', 'I', 'F', 'F' });

        writer.Write(36 + dataSize);

        writer.Write(new char[4] { 'W', 'A', 'V', 'E' });
        writer.Write(new char[4] { 'f', 'm', 't', ' ' });
        writer.Write(subchunk1Size);
        writer.Write(audioFormat);
        writer.Write(numChannels);
        writer.Write(sampleRate);
        writer.Write(byteRate);
        writer.Write(blockAlign);
        writer.Write(bitsPerSample);

        // data subchunk
        writer.Write(new char[4] { 'd', 'a', 't', 'a' });
        writer.Write(dataSize);

        float rescaleFactor = 32767; // to convert float to Int16

        for (int i = 0; i < left.Length; i++)
        {
            writer.Write((short)(left[i] * rescaleFactor));
            writer.Write((short)(right[i] * rescaleFactor));
        }

        writer.Seek(4, SeekOrigin.Begin);
        uint filesize = (uint)writer.BaseStream.Length;
        writer.Write(filesize - 8);

        byte[] bytes = output.ToArray();

        // Swap byte order if system architecture is not little-endian (required for WAV)
        if (!System.BitConverter.IsLittleEndian)
        {
            byte t;
            for (int i = 0; i < bytes.Length; i += 2)
            {
                t = bytes[i];
                bytes[i] = bytes[i + 1];
                bytes[i + 1] = t;
            }
        }

        return bytes;
    }
}
