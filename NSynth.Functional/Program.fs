open NAudio.Wave
open NAudio.Dsp
open System

type WaveProvider(getPointOnWave) = 
    inherit WaveProvider32()
    override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
        let sampleRate = float this.WaveFormat.SampleRate
        for sample in 0..sampleCount do
            buffer.[sample + offset] <- float32 (getPointOnWave sample sampleRate)
        sampleCount

let createSineWave frequency amplitude = 
    fun sample sampleRate -> 
        amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate)

let createComplexWave frequency amplitude = 
    let rec calculateForHarmonic harmonic frequency amplitude =
        fun (sample : int) (sampleRate : float) -> 
            match harmonic with
            | 10 -> 0.0 // Only calculate up to the 9th harmonic for performance
            | _ ->
                let harmonicComponent = createSineWave (frequency * float harmonic) ((amplitude / float harmonic))
                let currentHarmonic = harmonicComponent sample sampleRate
                currentHarmonic + calculateForHarmonic (harmonic + 1) frequency amplitude sample sampleRate
    calculateForHarmonic 1 frequency amplitude

let square = fun sample sampleRate -> createComplexWave 1000.0 0.5 sample sampleRate

[<EntryPoint>]
let main argv = 
    let s = WaveProvider(square)
    s.SetWaveFormat(44100, 1)
    let waveOut = new WaveOut()
    waveOut.Init(s)
    waveOut.Play()
    let wait = Console.ReadLine()
    0