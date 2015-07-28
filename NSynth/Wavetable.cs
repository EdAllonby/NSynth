using System.Collections.Generic;

namespace NSynth
{
    public sealed class Wavetable
    {
        private readonly List<float> samples;

        public Wavetable(List<float> wavetableSamples)
        {
            samples = wavetableSamples;
        }

        public int Size => samples.Count;

        public float SampleAtPosition(int position)
        {
            int wrappedPosition = position % (samples.Count - 1);

            return samples[wrappedPosition];
        }
    }
}