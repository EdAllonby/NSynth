namespace NSynth
{
    public sealed class TriangleWaveCalculator : IWaveformCalculator
    {
        private readonly HarmonicSignalCalculator harmonicSignalCalculator = new HarmonicSignalCalculator();

        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            return harmonicSignalCalculator.CalculateHarmonicsForSample(9, OddHarmonics, InverseSquaredAmplitude, sample, frequency, sampleRate);
        }

        private static bool OddHarmonics(int harmonic)
        {
            return harmonic%2 != 0;
        }

        private static float InverseSquaredAmplitude(int harmonic)
        {
            return 1/(float) (harmonic*harmonic);
        }
    }
}