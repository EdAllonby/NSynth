using System;
using System.Collections.Generic;
using System.Linq;

namespace NSynth
{
    public abstract class ComplexWaveCalculator : IWaveformCalculator
    {
        private readonly int finalHarmonic;
        private readonly SineWaveCalculator sineWaveCalculator = new SineWaveCalculator();

        protected ComplexWaveCalculator(int finalHarmonic)
        {
            this.finalHarmonic = finalHarmonic;
        }

        public float CalculateForSample(int sample, float frequency, int sampleRate)
        {
            IEnumerable<int> harmonicsToUse = Enumerable.Range(1, finalHarmonic).Where(IsHarmonicIncluded);

            IEnumerable<float> harmonicSignals = harmonicsToUse.Select(harmonic => CalculateSignalForHarmonic(sample, frequency, sampleRate, harmonic));

            return harmonicSignals.Sum();
        }

        protected abstract bool IsHarmonicIncluded(int harmonic);

        protected abstract float AmplitudeForHarmonic(int harmonic);

        private float CalculateSignalForHarmonic(int sample, float frequency, int sampleRate, int currentHarmonic)
        {
            float harmonicFrequency = frequency*currentHarmonic;
            float harmonicSignal = sineWaveCalculator.CalculateForSample(sample, harmonicFrequency, sampleRate);
            float harmonicAmplitude = AmplitudeForHarmonic(currentHarmonic);

            return harmonicAmplitude*harmonicSignal;
        }
    }
}