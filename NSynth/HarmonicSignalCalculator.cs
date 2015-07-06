using System;
using System.Collections.Generic;
using System.Linq;

namespace NSynth
{
    public class HarmonicSignalCalculator
    {
        private readonly SineWaveCalculator sineWaveCalculator = new SineWaveCalculator();

        public float CalculateHarmonicsForSample(int finalHarmonic, Func<int, bool> harmonicSelector, Func<int, float> amplitudeAttenuator, int sample, float frequency, int sampleRate)
        {
            IEnumerable<int> harmonicsToUse = Enumerable.Range(1, finalHarmonic).Where(harmonicSelector);

            IEnumerable<float> harmonicSignals = harmonicsToUse.Select(harmonic => CalculateSignalForHarmonic(amplitudeAttenuator, sample, frequency, sampleRate, harmonic));

            return harmonicSignals.Sum();
        }

        private float CalculateSignalForHarmonic(Func<int, float> amplitudeAttenuator, int sample, float frequency, int sampleRate, int currentHarmonic)
        {
            float harmonicFrequency = frequency*currentHarmonic;
            float harmonicSignal = sineWaveCalculator.CalculateForSample(sample, harmonicFrequency, sampleRate);
            float harmonicAmplitude = amplitudeAttenuator(currentHarmonic);

            return harmonicAmplitude*harmonicSignal;
        }
    }
}