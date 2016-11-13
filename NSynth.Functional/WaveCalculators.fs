namespace NSynth.Functional

module WaveCalculators = 
    open System
    
    let createSineWave frequency amplitude = fun sample sampleRate -> amplitude * Math.Sin((2.0 * Math.PI * (float sample) * frequency) / sampleRate)
    
    let createWave wave frequency amplitude = 
        match wave with
        | Sine -> createSineWave frequency amplitude
        | ComplexSine(frequencyScale, amplitudeScale, harmonicIncrement) -> 
            let finalHarmonic = 11
            
            let rec calculateForHarmonic harmonic = 
                fun sample sampleRate -> 
                    match harmonic with
                    | harmonic when harmonic < finalHarmonic -> 
                        let pointOnHarmonic = createSineWave (frequencyScale frequency (float harmonic)) (amplitudeScale amplitude (float harmonic))
                        let currentHarmonicPoint = pointOnHarmonic sample sampleRate
                        let harmonicPointAccumulator = (+) (calculateForHarmonic (harmonic + harmonicIncrement) sample sampleRate)
                        harmonicPointAccumulator currentHarmonicPoint
                    | _ -> 0.0 // End calculation when we reach a certain harmonic
            calculateForHarmonic 1 // Start the calculation at the first hamonic