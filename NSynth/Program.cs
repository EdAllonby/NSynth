using System;
using NAudio.Wave;

namespace NSynth
{
    public class Program
    {
        private static WaveOut waveOut;

        private static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.S)
                {
                    TogglePlayback();
                }
            }
        }

        private static void TogglePlayback()
        {
            if (waveOut == null)
            {
                var signalProvider = new SignalProvider(new SawtoothWaveCalculator());

                int sampleRate = 16000;
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
