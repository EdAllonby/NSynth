using System;
using System.Collections.Generic;

namespace NSynth
{
    public sealed class WavetableCalculator : IWaveformCalculator
    {
        private readonly List<float> wavetable = new List<float>();
        private float currentWavetablePosition;

        public WavetableCalculator(IWaveformCalculator calculator)
        {
            int wavetableSize = 1024;

            for (int position = 0; position < wavetableSize; position++)
            {
                float sample = calculator.CalculateForSample(position, wavetableSize, wavetableSize*wavetableSize);
                wavetable.Add(sample);
            }
        }

        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            float wavetableSample = wavetable[(int) Math.Round(currentWavetablePosition)];

            float step = wavetable.Count*frequency/sampleRate;

            currentWavetablePosition = (currentWavetablePosition + step)%(wavetable.Count - 1);

            return wavetableSample;
        }
    }
}
