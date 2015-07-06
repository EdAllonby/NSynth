namespace NSynth
{
    public sealed class SawtoothWaveCalculator : IWaveformCalculator
    {
        private readonly HarmonicSignalCalculator harmonicSignalCalculator = new HarmonicSignalCalculator();

        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            return harmonicSignalCalculator.CalculateHarmonicsForSample(9, x => true, InverseAmplitude, sample, frequency, sampleRate);
        }

        private static float InverseAmplitude(int harmonic)
        {
            return 1/(float) harmonic;
        }
    }
}