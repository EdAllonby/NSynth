namespace NSynth.Functional

module ComplexWaves = 
    let squareWave = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / harmonic), 2)
    let triangleWave = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / (harmonic * harmonic)), 2)
    let sawtoothWave = ComplexSine((fun frequency harmonic -> frequency * harmonic), (fun amplitude harmonic -> amplitude / (harmonic)), 1)
