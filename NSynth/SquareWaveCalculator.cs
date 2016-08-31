using System;

namespace NSynth
{
    public sealed class SquareWaveCalculator : ComplexWaveCalculator
    {
        public SquareWaveCalculator(int finalHarmonic) : base(finalHarmonic)
        {
        }

        protected override bool IsHarmonicIncluded(int harmonic)
        {
            return harmonic % 2 != 0;
        }

        protected override float AmplitudeForHarmonic(int harmonic)
        {
            // multiplying by 4 / pi renormalises the amplitude
            return (float)((1.0 / harmonic) * (4.0 / Math.PI));
        }
    }
}