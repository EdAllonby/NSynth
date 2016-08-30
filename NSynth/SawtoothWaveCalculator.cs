using System;

namespace NSynth
{
    public sealed class SawtoothWaveCalculator : ComplexWaveCalculator
    {
        public SawtoothWaveCalculator(int finalHarmonic) : base(finalHarmonic)
        {
        }

        protected override bool IsHarmonicIncluded(int harmonic)
        {
            return true;
        }

        protected override float AmplitudeForHarmonic(int harmonic)
        {
            // multiplying by 2 / pi renormalises the amplitude
            return (float)((1.0 / harmonic) * (2.0 / Math.PI));
        }
    }
}