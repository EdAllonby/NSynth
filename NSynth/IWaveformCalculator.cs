namespace NSynth
{
    public interface IWaveformCalculator
    {
        float CalculateForSample(int sample, float frequency, int sampleRate);
    }
}
