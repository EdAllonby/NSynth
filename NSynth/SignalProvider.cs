using System;
using NAudio.Wave;

namespace NSynth
{
    public sealed class SignalProvider : WaveProvider32
    {
        private int sample;

        private readonly IWaveformCalculator waveformCalculator;

        public SignalProvider(IWaveformCalculator waveformCalculator)
        {
            this.waveformCalculator = waveformCalculator;

            Frequency = 1000f;
            Amplitude = 0.25f;
        }

        public float Amplitude { get; set; }

        public float Frequency { get; set; }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;

            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = Amplitude*waveformCalculator.CalculateForSample(sample, Frequency, sampleRate);

                sample++;

                if (sample >= sampleRate)
                {
                    sample = 0;
                }
            }

            return sampleCount;
        }
    }
}
