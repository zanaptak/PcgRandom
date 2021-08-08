open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

open System
open Zanaptak.PcgRandom


type NextBenchmark() =
    let sysrand = System.Random( 1234567 )
    let pcg = Pcg( 1234567 )
    let pcg32 = Pcg32( 1234567UL )
    let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
    let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
    let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )

    [< Benchmark >]
    member this.SystemRandom() =
        sysrand.Next()

    [< Benchmark >]
    member this.Pcg() =
        pcg.Next()

    [< Benchmark >]
    member this.Pcg32() =
        pcg32.Next()

    [< Benchmark >]
    member this.Pcg32_Fast() =
        pcg32_Fast.Next()

    [< Benchmark >]
    member this.Pcg32_Invertible() =
        pcg32_Invertible.Next()

    [< Benchmark >]
    member this.Pcg64_Invertible() =
        pcg64_Invertible.Next()


type Next128Benchmark() =
    let pcg64 = Pcg64( 1234567I )
    let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
    let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

    [< Benchmark >]
    member this.Pcg64() =
        pcg64.Next()

    [< Benchmark >]
    member this.Pcg64_Fast() =
        pcg64_Fast.Next()

    [< Benchmark >]
    member this.Pcg128_Invertible() =
        pcg128_Invertible.Next()


type NextBoundBenchmark() =
    let sysrand = System.Random( 1234567 )
    let pcg = Pcg( 1234567 )
    let pcg32 = Pcg32( 1234567UL )
    let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
    let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
    let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )

    [< Benchmark >]
    member this.SystemRandom() =
        sysrand.Next( 100 )

    [< Benchmark >]
    member this.Pcg() =
        pcg.Next( 100 )

    [< Benchmark >]
    member this.Pcg32() =
        pcg32.Next( 100u )

    [< Benchmark >]
    member this.Pcg32_Fast() =
        pcg32_Fast.Next( 100u )

    [< Benchmark >]
    member this.Pcg32_Invertible() =
        pcg32_Invertible.Next( 100u )

    [< Benchmark >]
    member this.Pcg64_Invertible() =
        pcg64_Invertible.Next( 100UL )


type NextBound128Benchmark() =
    let pcg64 = Pcg64( 1234567I )
    let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
    let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

    [< Benchmark >]
    member this.Pcg64() =
        pcg64.Next( 100UL )

    [< Benchmark >]
    member this.Pcg64_Fast() =
        pcg64_Fast.Next( 100UL )

    [< Benchmark >]
    member this.Pcg128_Invertible() =
        pcg128_Invertible.Next( 100I )


type NextRangeBenchmark() =
    let sysrand = System.Random( 1234567 )
    let pcg = Pcg( 1234567 )
    let pcg32 = Pcg32( 1234567UL )
    let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
    let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
    let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )

    [< Benchmark >]
    member this.SystemRandom() =
        sysrand.Next( 0 , 100 )

    [< Benchmark >]
    member this.Pcg() =
        pcg.Next( 0 , 100 )

    [< Benchmark >]
    member this.Pcg32() =
        pcg32.Next( 0u , 100u )

    [< Benchmark >]
    member this.Pcg32_Fast() =
        pcg32_Fast.Next( 0u , 100u )

    [< Benchmark >]
    member this.Pcg32_Invertible() =
        pcg32_Invertible.Next( 0u , 100u )

    [< Benchmark >]
    member this.Pcg64_Invertible() =
        pcg64_Invertible.Next( 0UL , 100UL )


type NextRange128Benchmark() =
    let pcg64 = Pcg64( 1234567I )
    let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
    let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

    [< Benchmark >]
    member this.Pcg64() =
        pcg64.Next( 0UL , 100UL )

    [< Benchmark >]
    member this.Pcg64_Fast() =
        pcg64_Fast.Next( 0UL , 100UL )

    [< Benchmark >]
    member this.Pcg128_Invertible() =
        pcg128_Invertible.Next( 0I , 100I )


type NextBytesBenchmark() =
    let sysrand = System.Random( 1234567 )
    let pcg = Pcg( 1234567 )
    let pcg32 = Pcg32( 1234567UL )
    let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
    let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
    let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )

    let sysrand_Bytes = Array.create 99 0uy
    let pcg_Bytes = Array.create 99 0uy
    let pcg32_Bytes = Array.create 99 0uy
    let pcg32_Fast_Bytes = Array.create 99 0uy
    let pcg32_Invertible_Bytes = Array.create 99 0uy
    let pcg64_Invertible_Bytes = Array.create 99 0uy

    [< Benchmark >]
    member this.SystemRandom() =
        sysrand.NextBytes( sysrand_Bytes )

    [< Benchmark >]
    member this.Pcg() =
        pcg.NextBytes( pcg_Bytes )

    [< Benchmark >]
    member this.Pcg32() =
        pcg32.NextBytes( pcg32_Bytes )

    [< Benchmark >]
    member this.Pcg32_Fast() =
        pcg32_Fast.NextBytes( pcg32_Fast_Bytes )

    [< Benchmark >]
    member this.Pcg32_Invertible() =
        pcg32_Invertible.NextBytes( pcg32_Invertible_Bytes )

    [< Benchmark >]
    member this.Pcg64_Invertible() =
        pcg64_Invertible.NextBytes( pcg64_Invertible_Bytes )


[<EntryPoint>]
let main argv =
    BenchmarkSwitcher
        .FromTypes(
            [|
                typeof< NextBenchmark >
                typeof< NextBoundBenchmark >
                typeof< NextRangeBenchmark >
                typeof< NextBytesBenchmark >
                typeof< Next128Benchmark >
            |]
        )
        .RunAll()
        |> ignore
    0 // return an integer exit code
