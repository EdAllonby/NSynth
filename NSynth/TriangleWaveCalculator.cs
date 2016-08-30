using System;

namespace NSynth
{
    public sealed class TriangleWaveCalculator : ComplexWaveCalculator
    {
        public TriangleWaveCalculator(int finalHarmonic) : base(finalHarmonic)
        {
        }

        protected override bool IsHarmonicIncluded(int harmonic)
        {
            return harmonic % 2 != 0;
        }

        protected override float AmplitudeForHarmonic(int harmonic)
        {
            // multiplying by sin(harmonic * pi / 2) inverts every other odd harmonic
            return (float)((1.0 / (harmonic * harmonic)) * Math.Sin(harmonic * Math.PI / 2));
        }
    }
}