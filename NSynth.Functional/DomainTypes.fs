namespace NSynth.Functional

[<AutoOpen>]
module DomainTypes = 
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
    
    type WaveProvider(getPointOnWave : int -> float<hz> -> float) = 
        inherit WaveProvider32()
        override this.Read(buffer : float32 [], offset : int, sampleCount : int) = 
            let sampleRate = float this.WaveFormat.SampleRate * 1.0<hz> // Sample rate is measure in hz
            for sample in 0..sampleCount do
                buffer.[sample + offset] <- float32 (getPointOnWave sample sampleRate)
            sampleCount
