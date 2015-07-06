using System;

namespace NSynth
{
    public sealed class SineWaveCalculator : IWaveformCalculator
    {
        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            return (float) Math.Sin((2*Math.PI*sample*frequency)/sampleRate);
        }
    }
}