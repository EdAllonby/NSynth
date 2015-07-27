using System;
using NAudio.Wave;

namespace NSynth
{
    public class Program
    {
        private static WaveOut waveOut;
        private static SignalProvider signalProvider;
        private static float frequencyIncrement = 10;
        private static float amplitudeIncrement = 0.1f;

        private static void Main(string[] args)
        {
            TogglePlayback();

            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;

                switch (keyPressed)
                {
                    case ConsoleKey.S:
                        signalProvider.NoteState = !signalProvider.NoteState;
                        break;
                    case ConsoleKey.UpArrow:
                        signalProvider.Frequency += frequencyIncrement;
                        break;
                    case ConsoleKey.DownArrow:
                        signalProvider.Frequency -= frequencyIncrement;
                        break;
                    case ConsoleKey.RightArrow:
                        signalProvider.Amplitude += amplitudeIncrement;
                        break;
                    case ConsoleKey.LeftArrow:
                        signalProvider.Amplitude -= amplitudeIncrement;
                        break;
                }
            }
        }

        private static void TogglePlayback()
        {
            if (waveOut == null)
            {
                signalProvider = new SignalProvider(new WavetableCalculator(new SawtoothWaveCalculator(9)));

                int sampleRate = 44100;
                int channels = 1;

                signalProvider.SetWaveFormat(sampleRate, channels);

                waveOut = new WaveOut();
                waveOut.Init(signalProvider);
                waveOut.Play();
            }
            else
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }
    }
}
