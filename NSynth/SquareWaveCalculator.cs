namespace NSynth
{
    public sealed class SquareWaveCalculator : IWaveformCalculator
    {
        private readonly HarmonicSignalCalculator harmonicSignalCalculator = new HarmonicSignalCalculator();

        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            return harmonicSignalCalculator.CalculateHarmonicsForSample(9, OddHarmonics, InverseAmplitude, sample, frequency, sampleRate);
        }

        private static bool OddHarmonics(int harmonic)
        {
            return harmonic%2 != 0;
        }

        private static float InverseAmplitude(int harmonic)
        {
            return 1/(float) harmonic;
        }
    }
}