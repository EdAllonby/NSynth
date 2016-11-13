namespace NSynth.Functional

[<AutoOpen>]
module DomainTypes = 
    open NAudio.Wave
    
    type Wave = 
        | Sine
        | ComplexSine of frequencyMagnitude : (float -> float -> float) * amplitudeScale : (float -> float -> float) * harmonicIncrement : int
    
    type WaveProvider(getPointOnWave : int -> float -> float) = 
        inherit WaveProvider32()
        override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
            let sampleRate = float this.WaveFormat.SampleRate
            for sample in 0..sampleCount do
                buffer.[sample + offset] <- float32 (getPointOnWave sample sampleRate)
            sampleCount
