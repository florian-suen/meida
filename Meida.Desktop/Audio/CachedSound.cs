/*
using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace MetronomeApp.Audio;

public class CachedSound
{
    public CachedSound(string audioFilePath)
    {
        using var audioFileReader = new AudioFileReader(audioFilePath);

        WaveFormat = audioFileReader.WaveFormat;
        var wholeFile = new List<float>();


        var bufferSize = WaveFormat.SampleRate * WaveFormat.Channels;
        var bufferArray = new float[bufferSize];
        var bufferSpan = bufferArray.AsSpan();

        int samplesRead;


        while ((samplesRead = audioFileReader.Read(bufferSpan)) > 0)
            for (var i = 0; i < samplesRead; i++)
                wholeFile.Add(bufferArray[i]);

        AudioData = wholeFile.ToArray();
    }

    public float[] AudioData { get; }
    public WaveFormat WaveFormat { get; }
}
*/
