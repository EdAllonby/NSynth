open NAudio.Wave
open NAudio.Dsp
open System

type sineWaveProvider(frequency, amplitude) = 
    inherit WaveProvider32()
    override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
        let sampleRate = float this.WaveFormat.SampleRate
        let generateSine (sample : int) = 
            (amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate))

        for i in 0..sampleCount do
            buffer.[i + offset] <- float32 (generateSine i)

        sampleCount

[<EntryPoint>]
let main argv = 
    let s = new sineWaveProvider(2000.0, 0.25)
    s.SetWaveFormat(44100, 1)
    let waveOut = new WaveOut()
    waveOut.Init(s)
    waveOut.Play()
    let wait = Console.ReadLine()
    0 // return an integer exit code
