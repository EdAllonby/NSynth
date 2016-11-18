namespace NSynth.Functional

module ComplexWaves = 
    let squareWave' = 
        { frequencyMagnitude = (fun frequency harmonic -> frequency * (float harmonic))
          amplitudeScale = (fun amplitude harmonic -> amplitude / (float harmonic))
          harmonicIncrement = 2 }
    
    let triangleWave' = { squareWave' with amplitudeScale = fun amplitude harmonic -> amplitude / (float (harmonic * harmonic)) }
    let sawtoothWave' = { squareWave' with harmonicIncrement = 1 }

    let squareWave = Complex squareWave'
    let triangleWave = Complex triangleWave'
    let sawtoothWave = Complex sawtoothWave'


