﻿using System;

namespace SnesBox
{
    public class AudioUpdatedEventArgs : EventArgs
    {
        ushort[] _audioBuffer;
        int _sampleCount;

        public AudioUpdatedEventArgs(ushort[] audioBuffer, int sampleCount)
        {
            _audioBuffer = audioBuffer;
            _sampleCount = sampleCount;
        }

        public ushort[] AudioBuffer { get { return _audioBuffer; } }
        public int SampleCount { get { return _sampleCount; } }
    }

    public class VideoUpdatedEventArgs : EventArgs
    {
        ushort[] _videoBuffer;
        int _width;
        int _height;

        public VideoUpdatedEventArgs(ushort[] videoBuffer, int width, int height)
        {
            _videoBuffer = videoBuffer;
            _width = width;
            _height = height;
        }

        public ushort[] VideoBuffer { get { return _videoBuffer; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
    }

    public delegate void AudioUpdatedEventHandler(object sender, AudioUpdatedEventArgs e);
    public delegate void VideoUpdatedEventHandler(object sender, VideoUpdatedEventArgs e);
}