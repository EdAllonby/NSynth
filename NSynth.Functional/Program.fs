open NAudio.Wave
open NAudio.Dsp
open System

type Wave = 
    | Sine
    | ComplexSine of frequencyMagnitude : (float -> float -> float) * amplitudeScale : (float -> float -> float) * harmonicIncrement:int

let squareWave = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / harmonic), 2)
let triangleWave = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / (harmonic * harmonic)), 2)
let sawtoothCalculator = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / (harmonic)), 1)

type WaveProvider(getPointOnWave) = 
    inherit WaveProvider32()
    override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
        let sampleRate = float this.WaveFormat.SampleRate
        for sample in 0..sampleCount do
            buffer.[sample + offset] <- float32 (getPointOnWave sample sampleRate)
        sampleCount

let createSineWave frequency amplitude = fun sample sampleRate -> amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate)

let createWave wave frequency amplitude = 
    match wave with
    | Wave.Sine -> createSineWave frequency amplitude
    | Wave.ComplexSine(frequencyScale, amplitudeScale, harmonicIncrement) -> 
        let finalHarmonic = 11
        let rec calculateForHarmonic harmonic = 
            fun sample sampleRate -> 
                match harmonic with
                | harmonic when harmonic < finalHarmonic -> 
                    let harmonicComponent = createSineWave (frequencyScale frequency (float harmonic)) (amplitudeScale amplitude (float harmonic))
                    let currentHarmonic = harmonicComponent sample sampleRate
                    currentHarmonic + calculateForHarmonic (harmonic + harmonicIncrement) sample sampleRate
                | _ -> 0.0 // End calculation when we reach a certain harmonic

        calculateForHarmonic 1 // Start the calculation at the first hamonic

[<EntryPoint>]
let main argv = 
    let square = fun sample sampleRate -> createWave sawtoothCalculator 1000.0 0.5 sample sampleRate
    let wave = WaveProvider(square)
    wave.SetWaveFormat(44100, 1)
    let waveOut = new WaveOut()
    waveOut.Init(wave)
    waveOut.Play()
    let wait = Console.ReadLine()
    waveOut.Stop()
    waveOut.Dispose()
    0
