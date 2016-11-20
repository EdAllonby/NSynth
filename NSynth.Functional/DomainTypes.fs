namespace NSynth.Functional

[<AutoOpen>]
module DomainTypes = 
    open NAudio.Dsp
    open NAudio.Wave
    
    [<Measure>]
    type hz
    
    /// A complex wave is a combination of many sine waves, resulting in a 'complex' wave.
    /// We can define the subsequent sine waves from the fundamental frequency as 'harmonics', 
    /// where each harmonic affects the frequency and amplitude in some manner.
    type Complex = 
        { frequencyMagnitude : float<hz> -> int -> float<hz>
          amplitudeScale : float -> int -> float
          harmonicIncrement : int }
    
    type Wave = 
        | Sine
        | Complex of Complex
        | Noise
    
    type WaveProvider(getPointOnWave : int -> float<hz> -> float) = 
        inherit WaveProvider32()
        let envelope = EnvelopeGenerator()
        
        do 
            envelope.AttackRate <- float32 44100
            envelope.DecayRate <- float32 44100
            envelope.SustainLevel <- float32 0.5
            envelope.ReleaseRate <- float32 44100
        
        member __.NoteState 
            with get () = envelope.State <> EnvelopeGenerator.EnvelopeState.Idle
            and set (value) = envelope.Gate value
        
        override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
            envelope.AttackRate <- float32 44100
            let sampleRate = float this.WaveFormat.SampleRate * 1.0<hz> // Sample rate is measure in hz
            for sample in 0..sampleCount do
                let envelopeAmplitude = envelope.Process()
                let nextSample =  float32 (getPointOnWave sample sampleRate)
                buffer.[sample + offset] <- envelopeAmplitude * nextSample
            sampleCount
