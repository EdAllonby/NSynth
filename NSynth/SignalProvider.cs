using System;
using NAudio.Wave;

namespace NSynth
{
    public sealed class SignalProvider : WaveProvider32
    {
        private int sample;

        private readonly IWaveformCalculator waveformCalculator;
        private float amplitude;
        private float frequency;

        public SignalProvider(IWaveformCalculator waveformCalculator)
        {
            this.waveformCalculator = waveformCalculator;

            Frequency = 1000f;
            Amplitude = 0.25f;
        }

        public float Amplitude
        {
            get { return amplitude; }
            set
            {
                if (value >= 0 && value <= 1)
                {
                    amplitude = value;
                }
            }
        }

        public float Frequency
        {
            get { return frequency; }
            set
            {
                if (value > 0 && value < 20000)
                {
                    frequency = value;
                }
            }
        }

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
