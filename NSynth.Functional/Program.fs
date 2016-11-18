namespace NSynth.Functional

module Program = 
    open System
    open NAudio.Wave
    open ComplexWaves
    open WaveCalculators
    
    [<EntryPoint>]
    let main argv = 
        let sawtoothWave = fun sample sampleRate -> createWave sawtoothWave 1000.0<hz> 0.5 sample sampleRate
        let wave = WaveProvider(sawtoothWave)
        wave.SetWaveFormat(44100, 1)
        let waveOut = new WaveOut()
        waveOut.Init(wave)
        waveOut.Play()
        let wait = Console.ReadLine()
        waveOut.Stop()
        waveOut.Dispose()
        0
