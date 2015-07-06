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
            return 1/(float) harmonic;
        }
    }
}