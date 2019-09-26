module Tests

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

open System
open System.Numerics
open TestUtils
open Zanaptak.PcgRandom

//let parseBigintStr ( s : string ) =
//  s
//  |> Seq.chunkBySize 2
//  |> Seq.map ( fun chars -> "0X" + String( chars ) )
//  |> Seq.map byte
//  |> Seq.fold ( fun state b -> ( state <<< 8 ) + BigInteger( int b ) ) 0I

let referenceSeedTests =
  testList "reference seed" [

    testCase "reference seed: pcg8" <| fun () ->
      [
        "oneseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , referenceSeed16 )
        "setseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , referenceSeed16 , referenceStream16 )
        "mcg-16-xsh-rr-8" , Pcg8( Pcg8Variants.Fast.XSH_RR , referenceSeed16 )

        "oneseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , referenceSeed16 )
        "setseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , referenceSeed16 , referenceStream16 )
        "mcg-16-xsh-rs-8" , Pcg8( Pcg8Variants.Fast.XSH_RS , referenceSeed16 )

        "oneseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , referenceSeed16 )
        "setseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , referenceSeed16 , referenceStream16 )
        "mcg-16-rxs-m-8" , Pcg8( Pcg8Variants.Fast.RXS_M , referenceSeed16 )

        "setseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , referenceSeed8 , referenceStream8 )
        "oneseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , referenceSeed8 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataReference.expected variantName
        let bits , len = getExpectedValueCount expected
        let actual =
          generateOutput
            bits
            len
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 8 / 4 , '0' ) )
            ( fun ( i : int ) -> pcg.Next( byte i ) |> int )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "reference seed: pcg16" <| fun () ->
      [
        "oneseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , referenceSeed32 )
        "setseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , referenceSeed32 , referenceStream32 )
        "mcg-32-xsh-rr-16" , Pcg16( Pcg16Variants.Fast.XSH_RR , referenceSeed32 )

        "oneseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , referenceSeed32 )
        "setseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , referenceSeed32 , referenceStream32 )
        "mcg-32-xsh-rs-16" , Pcg16( Pcg16Variants.Fast.XSH_RS , referenceSeed32 )

        "oneseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , referenceSeed32 )
        "setseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , referenceSeed32 , referenceStream32 )
        "mcg-32-rxs-m-16" , Pcg16( Pcg16Variants.Fast.RXS_M , referenceSeed32 )

        "setseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , referenceSeed16 , referenceStream16 )
        "oneseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , referenceSeed16 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataReference.expected variantName
        let bits , len = getExpectedValueCount expected
        let actual =
          generateOutput
            bits
            len
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 16 / 4 , '0' ) )
            ( fun ( i : int ) -> pcg.Next( uint16 i ) |> int )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "reference seed: pcg32" <| fun () ->
      [
        "oneseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , referenceSeed64 )
        "setseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , referenceSeed64 , referenceStream64 )
        "mcg-64-xsh-rr-32" , Pcg32( Pcg32Variants.Fast.XSH_RR , referenceSeed64 )

        "oneseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , referenceSeed64 )
        "setseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , referenceSeed64 , referenceStream64 )
        "mcg-64-xsh-rs-32" , Pcg32( Pcg32Variants.Fast.XSH_RS , referenceSeed64 )

        "oneseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , referenceSeed64 )
        "setseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , referenceSeed64 , referenceStream64 )
        "mcg-64-xsl-rr-32" , Pcg32( Pcg32Variants.Fast.XSL_RR , referenceSeed64 )

        "oneseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , referenceSeed64 )
        "setseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , referenceSeed64 , referenceStream64 )
        "mcg-64-rxs-m-32" , Pcg32( Pcg32Variants.Fast.RXS_M , referenceSeed64 )

        "setseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , referenceSeed32 , referenceStream32 )
        "oneseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , referenceSeed32 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataReference.expected variantName
        let bits , len = getExpectedValueCount expected
        let actual =
          generateOutput
            bits
            len
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 32 / 4 , '0' ) )
            ( fun ( i : int ) -> pcg.Next( uint32 i ) |> int )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "reference seed: pcg64" <| fun () ->
      [
        "oneseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , referenceSeed128 )
        "setseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , referenceSeed128 , referenceStream128 )
        "mcg-128-xsh-rr-64" , Pcg64( Pcg64Variants.Fast.XSH_RR , referenceSeed128 )

        "oneseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , referenceSeed128 )
        "setseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , referenceSeed128 , referenceStream128 )
        "mcg-128-xsh-rs-64" , Pcg64( Pcg64Variants.Fast.XSH_RS , referenceSeed128 )

        "oneseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , referenceSeed128 )
        "setseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , referenceSeed128 , referenceStream128 )
        "mcg-128-xsl-rr-64" , Pcg64( Pcg64Variants.Fast.XSL_RR , referenceSeed128 )

        "oneseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , referenceSeed128 )
        "setseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , referenceSeed128 , referenceStream128 )
        "mcg-128-rxs-m-64" , Pcg64( Pcg64Variants.Fast.RXS_M , referenceSeed128 )

        "setseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , referenceSeed64 , referenceStream64 )
        "oneseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , referenceSeed64 )

        "setseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , referenceSeed64 , referenceStream64 )
        "oneseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , referenceSeed64 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataReference.expected variantName
        let bits , len = getExpectedValueCount expected
        let actual =
          generateOutput
            bits
            len
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 64 / 4 , '0' ) )
            ( fun ( i : int ) -> pcg.Next( uint64 i ) |> int )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "reference seed: pcg128" <| fun () ->
      [
        "setseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , referenceSeed128 , referenceStream128 )
        "oneseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , referenceSeed128 )

        "setseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , referenceSeed128 , referenceStream128 )
        "oneseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , referenceSeed128 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataReference.expected variantName
        let bits , len = getExpectedValueCount expected
        let actual =
          generateOutput
            bits
            len
            ( fun () -> pcg.Next() |> bigintToHex )
            ( fun ( i : int ) -> pcg.Next( bigint i ) |> int )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )
  ]

let thresholdTests =
  // Straightforward threshold function requires higher size data type (slow for bigint).
  // So we are using threshold function from PCG source.
  // Test that it produces same results.
  let thresholdNaive8 ( bound : uint8 ) = ( uint16 Byte.MaxValue + 1us ) % ( uint16 bound ) |> uint8
  let thresholdNaive16 ( bound : uint16 ) = ( uint32 UInt16.MaxValue + 1u ) % ( uint32 bound ) |> uint16
  let thresholdNaive32 ( bound : uint32 ) = ( uint64 UInt32.MaxValue + 1UL ) % ( uint64 bound ) |> uint32
  let bigintMaxUInt64 = bigint UInt64.MaxValue
  let thresholdNaive64 ( bound : uint64 ) = ( bigintMaxUInt64 + 1I ) % ( bigint bound ) &&& bigintMaxUInt64 |> uint64

  // PCG version
  let threshold8 bound = ( uint8 -( int8 bound ) ) % bound
  let threshold16 bound = ( uint16 -( int16 bound ) ) % bound
  let threshold32 bound = ( uint32 -( int32 bound ) ) % bound
  let threshold64 bound = ( uint64 -( int64 bound ) ) % bound

  testList "threshold" [

    testCase "threshold8 vs naive" <| fun () ->
      let theyMatch = Seq.init (Byte.MaxValue - 1uy |> int) (fun i -> 1uy + byte i) |> Seq.forall (fun n -> threshold8 n = thresholdNaive8 n)
      Expect.isTrue theyMatch ""


    testCase "threshold16 vs naive" <| fun () ->
      let theyMatch = Seq.init (UInt16.MaxValue - 1us |> int) (fun i -> 1us + uint16 i) |> Seq.forall (fun n -> threshold16 n = thresholdNaive16 n)
      Expect.isTrue theyMatch ""

    testCase "threshold32 vs naive" <| fun () ->
        // low values
      let theyMatch = Seq.init 999999 (fun i -> 1u + uint32 i) |> Seq.forall (fun n -> threshold32 n = thresholdNaive32 n)
      Expect.isTrue theyMatch ""

      // high values
      let theyMatch = Seq.init 999999 (fun i -> UInt32.MaxValue - uint32 i) |> Seq.forall (fun n -> threshold32 n = thresholdNaive32 n)
      Expect.isTrue theyMatch ""

      // random values
      let r32 = Pcg32();
      let theyMatch = Seq.init 999999 (fun i -> r32.Next()) |> Seq.forall (fun n -> threshold32 n = thresholdNaive32 n)
      Expect.isTrue theyMatch ""

    // fewer iters since bigints are slow on JS
    testCase "threshold64 vs naive" <| fun () ->
        // low values
      let theyMatch = Seq.init 9999 (fun i -> 1UL + uint64 i) |> Seq.forall (fun n -> threshold64 n = thresholdNaive64 n)
      Expect.isTrue theyMatch ""

      // high values
      let theyMatch = Seq.init 9999 (fun i -> UInt64.MaxValue - uint64 i) |> Seq.forall (fun n -> threshold64 n = thresholdNaive64 n)
      Expect.isTrue theyMatch ""

      // random values
      let r64 = Pcg64();
      let theyMatch = Seq.init 9999 (fun i -> r64.Next()) |> Seq.forall (fun n -> threshold64 n = thresholdNaive64 n)
      Expect.isTrue theyMatch ""

    // fewer iters since bigints are slow on JS
    testCase "threshold64 only (to see perf diff)" <| fun () ->
      // low values
      let theyMatch = Seq.init 9999 (fun i -> 1UL + uint64 i) |> Seq.forall (fun n -> threshold64 n = threshold64 n)
      Expect.isTrue theyMatch ""

      // high values
      let theyMatch = Seq.init 9999 (fun i -> UInt64.MaxValue - uint64 i) |> Seq.forall (fun n -> threshold64 n = threshold64 n)
      Expect.isTrue theyMatch ""

      // random values
      let r64 = Pcg64();
      let theyMatch = Seq.init 9999 (fun i -> r64.Next()) |> Seq.forall (fun n -> threshold64 n = threshold64 n)
      Expect.isTrue theyMatch ""

  ]

let customSeedTests =
  testList "custom seed tests" [

    testCase "custom seed tests: pcg8" <| fun () ->
      [
        "oneseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , TestDataCustomSeed.seed16 )
        "setseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , TestDataCustomSeed.seed16 , TestDataCustomSeed.stream16 )
        "mcg-16-xsh-rr-8" , Pcg8( Pcg8Variants.Fast.XSH_RR , TestDataCustomSeed.seed16 )

        "oneseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , TestDataCustomSeed.seed16 )
        "setseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , TestDataCustomSeed.seed16 , TestDataCustomSeed.stream16 )
        "mcg-16-xsh-rs-8" , Pcg8( Pcg8Variants.Fast.XSH_RS , TestDataCustomSeed.seed16 )

        "oneseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , TestDataCustomSeed.seed16 )
        "setseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , TestDataCustomSeed.seed16 , TestDataCustomSeed.stream16 )
        "mcg-16-rxs-m-8" , Pcg8( Pcg8Variants.Fast.RXS_M , TestDataCustomSeed.seed16 )

        "setseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed8 , TestDataCustomSeed.stream8 )
        "oneseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed8 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataCustomSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 8 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "custom seed tests: pcg16" <| fun () ->
      [
        "oneseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , TestDataCustomSeed.seed32 )
        "setseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , TestDataCustomSeed.seed32 , TestDataCustomSeed.stream32 )
        "mcg-32-xsh-rr-16" , Pcg16( Pcg16Variants.Fast.XSH_RR , TestDataCustomSeed.seed32 )

        "oneseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , TestDataCustomSeed.seed32 )
        "setseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , TestDataCustomSeed.seed32 , TestDataCustomSeed.stream32 )
        "mcg-32-xsh-rs-16" , Pcg16( Pcg16Variants.Fast.XSH_RS , TestDataCustomSeed.seed32 )

        "oneseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , TestDataCustomSeed.seed32 )
        "setseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , TestDataCustomSeed.seed32 , TestDataCustomSeed.stream32 )
        "mcg-32-rxs-m-16" , Pcg16( Pcg16Variants.Fast.RXS_M , TestDataCustomSeed.seed32 )

        "setseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed16 , TestDataCustomSeed.stream16 )
        "oneseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed16 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataCustomSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 16 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "custom seed tests: pcg32" <| fun () ->
      [
        "oneseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , TestDataCustomSeed.seed64 )
        "setseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "mcg-64-xsh-rr-32" , Pcg32( Pcg32Variants.Fast.XSH_RR , TestDataCustomSeed.seed64 )

        "oneseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , TestDataCustomSeed.seed64 )
        "setseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "mcg-64-xsh-rs-32" , Pcg32( Pcg32Variants.Fast.XSH_RS , TestDataCustomSeed.seed64 )

        "oneseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , TestDataCustomSeed.seed64 )
        "setseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "mcg-64-xsl-rr-32" , Pcg32( Pcg32Variants.Fast.XSL_RR , TestDataCustomSeed.seed64 )

        "oneseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , TestDataCustomSeed.seed64 )
        "setseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "mcg-64-rxs-m-32" , Pcg32( Pcg32Variants.Fast.RXS_M , TestDataCustomSeed.seed64 )

        "setseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed32 , TestDataCustomSeed.stream32 )
        "oneseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed32 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataCustomSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 32 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "custom seed tests: pcg64" <| fun () ->
      [
        "oneseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , TestDataCustomSeed.seed128 )
        "setseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "mcg-128-xsh-rr-64" , Pcg64( Pcg64Variants.Fast.XSH_RR , TestDataCustomSeed.seed128 )

        "oneseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , TestDataCustomSeed.seed128 )
        "setseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "mcg-128-xsh-rs-64" , Pcg64( Pcg64Variants.Fast.XSH_RS , TestDataCustomSeed.seed128 )

        "oneseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , TestDataCustomSeed.seed128 )
        "setseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "mcg-128-xsl-rr-64" , Pcg64( Pcg64Variants.Fast.XSL_RR , TestDataCustomSeed.seed128 )

        "oneseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , TestDataCustomSeed.seed128 )
        "setseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "mcg-128-rxs-m-64" , Pcg64( Pcg64Variants.Fast.RXS_M , TestDataCustomSeed.seed128 )

        "setseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "oneseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed64 )

        "setseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , TestDataCustomSeed.seed64 , TestDataCustomSeed.stream64 )
        "oneseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , TestDataCustomSeed.seed64 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataCustomSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 64 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "custom seed tests: pcg128" <| fun () ->
      [
        "setseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "oneseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , TestDataCustomSeed.seed128 )

        "setseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , TestDataCustomSeed.seed128 , TestDataCustomSeed.stream128 )
        "oneseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , TestDataCustomSeed.seed128 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataCustomSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> pcg.Next() |> bigintToHex )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

  ]

let maxSeedValuesTests =
  testList "max seed tests" [

    testCase "max seed tests: pcg8" <| fun () ->
      [
        "oneseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , TestDataMaxSeed.seed16 )
        "setseq-16-xsh-rr-8" , Pcg8( Pcg8Variants.Normal.XSH_RR , TestDataMaxSeed.seed16 , TestDataMaxSeed.stream16 )
        "mcg-16-xsh-rr-8" , Pcg8( Pcg8Variants.Fast.XSH_RR , TestDataMaxSeed.seed16 )

        "oneseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , TestDataMaxSeed.seed16 )
        "setseq-16-xsh-rs-8" , Pcg8( Pcg8Variants.Normal.XSH_RS , TestDataMaxSeed.seed16 , TestDataMaxSeed.stream16 )
        "mcg-16-xsh-rs-8" , Pcg8( Pcg8Variants.Fast.XSH_RS , TestDataMaxSeed.seed16 )

        "oneseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , TestDataMaxSeed.seed16 )
        "setseq-16-rxs-m-8" , Pcg8( Pcg8Variants.Normal.RXS_M , TestDataMaxSeed.seed16 , TestDataMaxSeed.stream16 )
        "mcg-16-rxs-m-8" , Pcg8( Pcg8Variants.Fast.RXS_M , TestDataMaxSeed.seed16 )

        "setseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed8 , TestDataMaxSeed.stream8 )
        "oneseq-8-rxs-m-xs-8" , Pcg8( Pcg8Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed8 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataMaxSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 8 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "max seed tests: pcg16" <| fun () ->
      [
        "oneseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , TestDataMaxSeed.seed32 )
        "setseq-32-xsh-rr-16" , Pcg16( Pcg16Variants.Normal.XSH_RR , TestDataMaxSeed.seed32 , TestDataMaxSeed.stream32 )
        "mcg-32-xsh-rr-16" , Pcg16( Pcg16Variants.Fast.XSH_RR , TestDataMaxSeed.seed32 )

        "oneseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , TestDataMaxSeed.seed32 )
        "setseq-32-xsh-rs-16" , Pcg16( Pcg16Variants.Normal.XSH_RS , TestDataMaxSeed.seed32 , TestDataMaxSeed.stream32 )
        "mcg-32-xsh-rs-16" , Pcg16( Pcg16Variants.Fast.XSH_RS , TestDataMaxSeed.seed32 )

        "oneseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , TestDataMaxSeed.seed32 )
        "setseq-32-rxs-m-16" , Pcg16( Pcg16Variants.Normal.RXS_M , TestDataMaxSeed.seed32 , TestDataMaxSeed.stream32 )
        "mcg-32-rxs-m-16" , Pcg16( Pcg16Variants.Fast.RXS_M , TestDataMaxSeed.seed32 )

        "setseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed16 , TestDataMaxSeed.stream16 )
        "oneseq-16-rxs-m-xs-16" , Pcg16( Pcg16Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed16 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataMaxSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 16 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "max seed tests: pcg32" <| fun () ->
      [
        "oneseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , TestDataMaxSeed.seed64 )
        "setseq-64-xsh-rr-32" , Pcg32( Pcg32Variants.Normal.XSH_RR , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "mcg-64-xsh-rr-32" , Pcg32( Pcg32Variants.Fast.XSH_RR , TestDataMaxSeed.seed64 )

        "oneseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , TestDataMaxSeed.seed64 )
        "setseq-64-xsh-rs-32" , Pcg32( Pcg32Variants.Normal.XSH_RS , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "mcg-64-xsh-rs-32" , Pcg32( Pcg32Variants.Fast.XSH_RS , TestDataMaxSeed.seed64 )

        "oneseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , TestDataMaxSeed.seed64 )
        "setseq-64-xsl-rr-32" , Pcg32( Pcg32Variants.Normal.XSL_RR , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "mcg-64-xsl-rr-32" , Pcg32( Pcg32Variants.Fast.XSL_RR , TestDataMaxSeed.seed64 )

        "oneseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , TestDataMaxSeed.seed64 )
        "setseq-64-rxs-m-32" , Pcg32( Pcg32Variants.Normal.RXS_M , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "mcg-64-rxs-m-32" , Pcg32( Pcg32Variants.Fast.RXS_M , TestDataMaxSeed.seed64 )

        "setseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed32 , TestDataMaxSeed.stream32 )
        "oneseq-32-rxs-m-xs-32" , Pcg32( Pcg32Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed32 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataMaxSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 32 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "max seed tests: pcg64" <| fun () ->
      [
        "oneseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , TestDataMaxSeed.seed128 )
        "setseq-128-xsh-rr-64" , Pcg64( Pcg64Variants.Normal.XSH_RR , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "mcg-128-xsh-rr-64" , Pcg64( Pcg64Variants.Fast.XSH_RR , TestDataMaxSeed.seed128 )

        "oneseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , TestDataMaxSeed.seed128 )
        "setseq-128-xsh-rs-64" , Pcg64( Pcg64Variants.Normal.XSH_RS , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "mcg-128-xsh-rs-64" , Pcg64( Pcg64Variants.Fast.XSH_RS , TestDataMaxSeed.seed128 )

        "oneseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , TestDataMaxSeed.seed128 )
        "setseq-128-xsl-rr-64" , Pcg64( Pcg64Variants.Normal.XSL_RR , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "mcg-128-xsl-rr-64" , Pcg64( Pcg64Variants.Fast.XSL_RR , TestDataMaxSeed.seed128 )

        "oneseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , TestDataMaxSeed.seed128 )
        "setseq-128-rxs-m-64" , Pcg64( Pcg64Variants.Normal.RXS_M , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "mcg-128-rxs-m-64" , Pcg64( Pcg64Variants.Fast.RXS_M , TestDataMaxSeed.seed128 )

        "setseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "oneseq-64-rxs-m-xs-64" , Pcg64( Pcg64Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed64 )

        "setseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , TestDataMaxSeed.seed64 , TestDataMaxSeed.stream64 )
        "oneseq-64-xsl-rr-rr-64" , Pcg64( Pcg64Variants.Invertible.XSL_RR_RR , TestDataMaxSeed.seed64 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataMaxSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> "0x" + ( sprintf "%x" ( pcg.Next() ) ).PadLeft( 64 / 4 , '0' ) )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )

    testCase "max seed tests: pcg128" <| fun () ->
      [
        "setseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "oneseq-128-rxs-m-xs-128" , Pcg128( Pcg128Variants.Invertible.RXS_M_XS , TestDataMaxSeed.seed128 )

        "setseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , TestDataMaxSeed.seed128 , TestDataMaxSeed.stream128 )
        "oneseq-128-xsl-rr-rr-128" , Pcg128( Pcg128Variants.Invertible.XSL_RR_RR , TestDataMaxSeed.seed128 )
      ]
      |> List.iter ( fun ( variantName , pcg ) ->
        let expected = getExpectedResultBlock TestDataMaxSeed.expected variantName
        let count , wrap = getValueCountAndWrap expected
        let actual =
          generateValuesBlock
            count
            wrap
            ( fun () -> pcg.Next() |> bigintToHex )
        Expect.equal actual expected ( "FAILED on variant " + variantName )
      )
  ]

let pcgTests =
  testList "Pcg methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg( 12345 ).Next() ) 1411482639 ""
    testCase "Next(Int32.MaxValue) same as Next()" <| fun () ->
      Expect.equal ( Pcg( 12345 ).Next( Int32.MaxValue ) ) 1411482639 ""
    testCase "NextDouble()" <| fun () ->
      Expect.equal ( Pcg( 12345 ).NextDouble() ) 0.32863641142114913 ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 5
      Pcg( 12345 ).NextBytes( bytes )
      Expect.equal bytes [| 15uy ; 132uy ; 33uy ; 84uy ; 155uy |] ""
    testCase "NextBytes() offset " <| fun () ->
      let bytes : byte array = Array.zeroCreate 7
      Pcg( 12345 ).NextBytes( bytes , 1 , 5 )
      Expect.equal bytes [| 0uy ; 15uy ; 132uy ; 33uy ; 84uy ; 155uy ; 0uy |] ""
    testCase "Next() 22 items" <| fun () ->
      let p = Pcg( 12345 )
      let expected =
        [|1411482639; 1017708956; 1213308536; 285554700; 628889468; 1631147903;
          283047574; 357275135; 1116223725; 866116730; 1421996083; 1466704544;
          1726705447; 1043933343; 1500908903; 640134120; 1028161106;
          1997943676; 1123590394; 1757611379; 553427003; 988897843|]
      Expect.equal ( Array.init 22 ( fun _ -> p.Next() ) ) expected ""
    testCase "Next(Int32.MaxValue) same as Next() 22 items" <| fun () ->
      let p = Pcg( 12345 )
      let expected =
        [|1411482639; 1017708956; 1213308536; 285554700; 628889468; 1631147903;
          283047574; 357275135; 1116223725; 866116730; 1421996083; 1466704544;
          1726705447; 1043933343; 1500908903; 640134120; 1028161106;
          1997943676; 1123590394; 1757611379; 553427003; 988897843|]
      Expect.equal ( Array.init 22 ( fun _ -> p.Next( Int32.MaxValue ) ) ) expected ""
    testCase "Next(100) 10 items" <| fun () ->
      let p = Pcg( 12345 )
      let expected = [|39; 3; 83; 47; 68; 50; 21; 82; 25; 77|]
      Expect.equal ( Array.init 10 ( fun _ -> p.Next(100) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p = Pcg( 12345 )
      let expected = [|39; 3; 83; 47; 68; 50; 21; 82; 25; 77|]
      Expect.equal ( Array.init 10 ( fun _ -> p.Next(0,100) ) ) expected ""
    testCase "Next(-3,3) in range" <| fun () ->
      let p = Pcg( 12345 )
      let actual = Seq.init 999 ( fun _ -> p.Next( -3 , 3 ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| -3 ; -2 ; -1 ; 0 ; 1 ; 2 |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg()
      let p2 = Pcg()
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let pcg8Tests =
  testList "Pcg8 methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg8( 1234us ).Next() ) 130uy ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 5
      Pcg8( 1234us ).NextBytes( bytes )
      Expect.equal bytes [| 130uy ; 24uy ; 254uy ; 194uy ; 148uy |] ""
    testCase "Next(100) 10 items" <| fun () ->
      let p8 = Pcg8( 1234us )
      let expected = [|30uy; 54uy; 94uy; 48uy; 69uy; 92uy; 89uy; 57uy; 23uy; 87uy|]
      Expect.equal ( Array.init 10 ( fun _ -> p8.Next( 100uy ) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p8 = Pcg8( 1234us )
      let expected = [|30uy; 54uy; 94uy; 48uy; 69uy; 92uy; 89uy; 57uy; 23uy; 87uy|]
      Expect.equal ( Array.init 10 ( fun _ -> p8.Next( 0uy , 100uy ) ) ) expected ""
    testCase "Next(3,9) in range" <| fun () ->
      let p = Pcg8( 1234us )
      let actual = Seq.init 999 ( fun _ -> p.Next( 3uy , 9uy ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| 3uy ; 4uy ; 5uy ; 6uy ; 7uy ; 8uy |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg8()
      let p2 = Pcg8()
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let pcg16Tests =
  testList "Pcg16 methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg16( 1234567u ).Next() ) 53068us ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 5
      Pcg16( 1234567u ).NextBytes( bytes )
      Expect.equal bytes [| 76uy; 207uy; 76uy; 62uy; 170uy |] ""
    testCase "Next(100) 10 items" <| fun () ->
      let p16 = Pcg16( 1234567u )
      let expected = [|68us; 48us; 50us; 84us; 45us; 44us; 33us; 23us; 20us; 93us|]
      Expect.equal ( Array.init 10 ( fun _ -> p16.Next( 100us ) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p16 = Pcg16( 1234567u )
      let expected = [|68us; 48us; 50us; 84us; 45us; 44us; 33us; 23us; 20us; 93us|]
      Expect.equal ( Array.init 10 ( fun _ -> p16.Next( 0us , 100us ) ) ) expected ""
    testCase "Next(3,9) in range" <| fun () ->
      let p = Pcg16( 1234567u )
      let actual = Seq.init 999 ( fun _ -> p.Next( 3us , 9us ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| 3us ; 4us ; 5us ; 6us ; 7us ; 8us |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg16()
      let p2 = Pcg16()
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let pcg32Tests =
  testList "Pcg32 methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg32( 1234567890987UL ).Next() ) 108360520u ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 5
      Pcg32( 1234567890987UL ).NextBytes( bytes )
      Expect.equal bytes [| 72uy; 115uy; 117uy; 6uy; 108uy |] ""
    testCase "Next( int32 max ) 22 items" <| fun () ->
      let p = Pcg32( 12345UL )
      let expected =
        [|1411482639u; 1017708956u; 1213308536u; 285554700u; 628889468u; 1631147903u;
          283047574u; 357275135u; 1116223725u; 866116730u; 1421996083u; 1466704544u;
          1726705447u; 1043933343u; 1500908903u; 640134120u; 1028161106u;
          1997943676u; 1123590394u; 1757611379u; 553427003u; 988897843u|]
      Expect.equal ( Array.init 22 ( fun _ -> p.Next( uint32 Int32.MaxValue ) ) ) expected ""
    testCase "Next(100) 10 items" <| fun () ->
      let p32 = Pcg32( 1234567890987UL )
      let expected = [|20u; 16u; 57u; 98u; 54u; 27u; 64u; 42u; 13u; 58u|]
      Expect.equal ( Array.init 10 ( fun _ -> p32.Next( 100u ) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p32 = Pcg32( 1234567890987UL )
      let expected = [|20u; 16u; 57u; 98u; 54u; 27u; 64u; 42u; 13u; 58u|]
      Expect.equal ( Array.init 10 ( fun _ -> p32.Next( 0u , 100u ) ) ) expected ""
    testCase "Next(3,9) in range" <| fun () ->
      let p = Pcg32( 1234567890987UL )
      let actual = Seq.init 999 ( fun _ -> p.Next( 3u , 9u ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| 3u ; 4u ; 5u ; 6u ; 7u ; 8u |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg32()
      let p2 = Pcg32()
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let pcg64Tests =
  testList "Pcg64 methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg64( 1234567890987654321012345I ).Next() ) 4138087873088397747UL ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 10
      Pcg64( 1234567890987654321012345I ).NextBytes( bytes )
      Expect.equal bytes [| 179uy; 33uy; 201uy; 252uy; 254uy; 112uy; 109uy; 57uy; 231uy; 198uy |] ""
    testCase "Next(100) 10 items" <| fun () ->
      let p64 = Pcg64( 1234567890987654321012345I )
      let expected = [|47UL; 51UL; 96UL; 52UL; 49UL; 84UL; 59UL; 24UL; 90UL; 52UL|]
      Expect.equal ( Array.init 10 ( fun _ -> p64.Next( 100UL ) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p64 = Pcg64( 1234567890987654321012345I )
      let expected = [|47UL; 51UL; 96UL; 52UL; 49UL; 84UL; 59UL; 24UL; 90UL; 52UL|]
      Expect.equal ( Array.init 10 ( fun _ -> p64.Next( 0UL , 100UL ) ) ) expected ""
    testCase "Next(3,9) in range" <| fun () ->
      let p = Pcg64( 1234567890987654321012345I )
      let actual = Seq.init 999 ( fun _ -> p.Next( 3UL , 9UL ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| 3UL ; 4UL ; 5UL ; 6UL ; 7UL ; 8UL |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg64()
      let p2 = Pcg64()
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let pcg128InvertibleTests =
  testList "Pcg128 Invertible methods" [
    testCase "Next()" <| fun () ->
      Expect.equal ( Pcg128( Pcg128Variants.Invertible.Default , 1234567890987654321012345I ).Next() ) 201198658666555169016505132273858322867I ""
    testCase "NextBytes()" <| fun () ->
      let bytes : byte array = Array.zeroCreate 18
      Pcg128( Pcg128Variants.Invertible.Default , 1234567890987654321012345I ).NextBytes( bytes )
      Expect.equal bytes [| 179uy; 33uy; 201uy; 252uy; 254uy; 112uy; 109uy; 57uy; 145uy; 162uy; 72uy; 20uy; 192uy; 115uy; 93uy; 151uy; 231uy; 198uy |] ""
    testCase "Next(100) 10 items" <| fun () ->
      let p128 = Pcg128( Pcg128Variants.Invertible.Default , 1234567890987654321012345I )
      let expected = [|67I; 95I; 32I; 8I; 9I; 52I; 19I; 52I; 58I; 40I|]
      Expect.equal ( Array.init 10 ( fun _ -> p128.Next( 100I ) ) ) expected ""
    testCase "Next(0,100) 10 items" <| fun () ->
      let p128 = Pcg128( Pcg128Variants.Invertible.Default , 1234567890987654321012345I )
      let expected = [|67I; 95I; 32I; 8I; 9I; 52I; 19I; 52I; 58I; 40I|]
      Expect.equal ( Array.init 10 ( fun _ -> p128.Next( 0I , 100I ) ) ) expected ""
    testCase "Next(3,9) in range" <| fun () ->
      let p = Pcg128( Pcg128Variants.Invertible.Default , 1234567890987654321012345I )
      let actual = Seq.init 999 ( fun _ -> p.Next( 3I , 9I ) ) |> Seq.distinct |> Seq.toArray |> Array.sort
      let expected = [| 3I ; 4I ; 5I ; 6I ; 7I ; 8I |]
      Expect.equal actual expected ""
    testCase "diff output from random seeds" <| fun () ->
      // should differ due to crypto seeding, infinitesimal chance of seed collision
      let p1 = Pcg128( Pcg128Variants.Invertible.Default )
      let p2 = Pcg128( Pcg128Variants.Invertible.Default )
      let r1 = Array.init 22 ( fun _ -> p1.Next() )
      let r2 = Array.init 22 ( fun _ -> p2.Next() )
      Expect.notEqual r1 r2 ""
  ]

let allTests =
  testList "All" [
    //thresholdTests
    maxSeedValuesTests
    customSeedTests
    referenceSeedTests
    pcgTests
    pcg8Tests
    pcg16Tests
    pcg32Tests
    pcg64Tests
    pcg128InvertibleTests
  ]
