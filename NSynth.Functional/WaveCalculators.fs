namespace NSynth.Functional

module WaveCalculators = 
    open System
    
    let private randomNumberGenerator = Random()

    let private randomSample maximumAmplitude = maximumAmplitude * ((2. * randomNumberGenerator.NextDouble()) - 1.0)

    let private createSineWave (frequency : float<hz>) amplitude = 
        fun sample sampleRate -> amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate)
    
    let createWave wave frequency amplitude = 
        match wave with
        | Sine -> createSineWave frequency amplitude
        | Complex cs -> 
            let finalHarmonic = 11
            
            let rec calculateForHarmonic harmonic = 
                fun sample sampleRate -> 
                    match harmonic with
                    | harmonic when harmonic < finalHarmonic -> 
                        let pointOnHarmonic = createSineWave (cs.frequencyMagnitude frequency harmonic) (cs.amplitudeScale amplitude harmonic)
                        let currentHarmonicPoint = pointOnHarmonic sample sampleRate
                        let harmonicPointAccumulator = (+) (calculateForHarmonic (harmonic + cs.harmonicIncrement) sample sampleRate)
                        harmonicPointAccumulator currentHarmonicPoint
                    | _ -> 0.0 // End calculation when we reach a certain harmonic
            calculateForHarmonic 1 // Start the calculation at the first hamonic
        | Noise ->
            fun _ _ -> randomSample amplitude

