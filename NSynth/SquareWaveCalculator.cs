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
            return 1 / (float)harmonic;
        }
    }
}