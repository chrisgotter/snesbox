using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SnesBox
{
    class Audio : GameComponent
    {
        DynamicSoundEffectInstance _audioFrame;

        public Audio(Game game, Snes snes)
            : base(game)
        {
            _audioFrame = new DynamicSoundEffectInstance(32040, AudioChannels.Stereo);
            _audioFrame.Play();

            snes.AudioUpdated += new AudioUpdatedEventHandler(OnAudioUpdated);
        }

        void OnAudioUpdated(object sender, AudioUpdatedEventArgs e)
        {
            var audioBuffer = new byte[e.SampleCount * 4];
            int bufferIndex = 0;

            for (int i = 0; i < e.AudioBuffer.Length; i++)
            {
                var samples = BitConverter.GetBytes(e.AudioBuffer[i]);
                audioBuffer[bufferIndex++] = samples[0];
                audioBuffer[bufferIndex++] = samples[1];
                audioBuffer[bufferIndex++] = samples[2];
                audioBuffer[bufferIndex++] = samples[3];
            }

            if (audioBuffer.Length > 0)
            {
                _audioFrame.SubmitBuffer(audioBuffer, 0, audioBuffer.Length);
            }
        }
    }
}
