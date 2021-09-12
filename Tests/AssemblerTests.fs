module AssemblerTests

open Assembler
open Parsing
open WavePacker
open NUnit.Framework

[<TestFixture>]
type ``when assembling a composition`` ()=

    let extractChoice1 v = 
        match v with 
            | Choice2Of2 s -> sprintf "unexpected choice value %A" s |> failwith
            | Choice1Of2 s -> s

    [<Test>]
    member this.``come as you are`` ()=
        let stream = assembleToPackedStream "16#a1 16#a1 8#a1 16#a1 8c2 8g1 4#a1 8#a1 16#d2 4f2 32f2 16g2 16g2 4g2 16f2 8f2 16#d2 2#d2 16#a1 16g2 4g2 8#g2 16g2 8g2 4f2 16g2 16f2 8f2 2#d2 16#a1 8#a1 16#a1 8c2 8g1 4#a1 8#a1 16#d2 4f2 32f2 16g2 16g2 4g2 16f2 16f2 8#d2 16#d2 4#d2 16#a1 16g2 4g2 " |> extractChoice1
        writeSingle "comeasyouare.wav" stream
        ()

    [<Test>]
    member this.``the result should have the correct length`` ()=
        let scoreD = parse "2#d3 2- 2- 8#d3 4c2 4c2 8c1 2- 4c1" |> extractChoice1
        let samples = assemble scoreD        
        let expectedSamples = 6. * 44100.
        Assert.AreEqual(expectedSamples, Array.length samples)
        ()