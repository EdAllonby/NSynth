open NAudio.Wave
open NAudio.Dsp
open System

type Wave = 
    | Sine
    | Triangle
    | Square

type sineWaveProvider(wave, frequency, amplitude) = 
    inherit WaveProvider32()
    override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
        let sampleRate = float this.WaveFormat.SampleRate
        let generateSine (sample : int) = 
            (amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate))
        
        let generateWave sample = 
            match wave with
            | Sine -> generateSine sample
            | _ -> 1.0
        for sample in 0..sampleCount do
            buffer.[sample + offset] <- float32 (generateWave sample)
        sampleCount

[<EntryPoint>]
let main argv = 
    let s = new sineWaveProvider(Wave.Sine, 500.0, 0.25)
    s.SetWaveFormat(44100, 1)
    let waveOut = new WaveOut()
    waveOut.Init(s)
    waveOut.Play()
    let wait = Console.ReadLine()
    0
