namespace NSynth.Functional

module Program = 
    open System
    open NAudio.Wave
    open ComplexWaves
    open WaveCalculators
    
    [<EntryPoint>]
    let main _ = 
        let waveOut = new WaveOut()
        let waveFunction = fun sample sampleRate -> createWave Noise 1000.0<hz> 0.5 sample sampleRate
        let wave = WaveProvider(waveFunction)
        wave.SetWaveFormat(44100, 1)
        waveOut.Init(wave)
        waveOut.Play()
        while true do
            let key = Console.ReadKey(true).Key
            match key with
            | ConsoleKey.S -> wave.NoteState <- Operators.not wave.NoteState
            | _ -> ()
        0
