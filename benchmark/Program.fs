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
  let pcg64 = Pcg64( 1234567I )
  let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
  let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )
  let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

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
  member this.Pcg64() =
    pcg64.Next()

  [< Benchmark >]
  member this.Pcg64_Fast() =
    pcg64_Fast.Next()

  [< Benchmark >]
  member this.Pcg64_Invertible() =
    pcg64_Invertible.Next()

  [< Benchmark >]
  member this.Pcg128_Invertible() =
    pcg128_Invertible.Next()

type NextBoundBenchmark() =
  let sysrand = System.Random( 1234567 )
  let pcg = Pcg( 1234567 )
  let pcg32 = Pcg32( 1234567UL )
  let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
  let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
  let pcg64 = Pcg64( 1234567I )
  let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
  let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )
  let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

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
  member this.Pcg64() =
    pcg64.Next( 100UL )

  [< Benchmark >]
  member this.Pcg64_Fast() =
    pcg64_Fast.Next( 100UL )

  [< Benchmark >]
  member this.Pcg64_Invertible() =
    pcg64_Invertible.Next( 100UL )

  [< Benchmark >]
  member this.Pcg128_Invertible() =
    pcg128_Invertible.Next( 100I )

type NextRangeBenchmark() =
  let sysrand = System.Random( 1234567 )
  let pcg = Pcg( 1234567 )
  let pcg32 = Pcg32( 1234567UL )
  let pcg32_Fast = Pcg32( Pcg32Variants.Fast.Default , 1234567UL )
  let pcg32_Invertible = Pcg32( Pcg32Variants.Invertible.Default , 1234567u )
  let pcg64 = Pcg64( 1234567I )
  let pcg64_Fast = Pcg64( Pcg64Variants.Fast.Default , 1234567I )
  let pcg64_Invertible = Pcg64( Pcg64Variants.Invertible.Default , 1234567UL )
  let pcg128_Invertible = Pcg128( Pcg128Variants.Invertible.Default , 1234567I )

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
  member this.Pcg64() =
    pcg64.Next( 0UL , 100UL )

  [< Benchmark >]
  member this.Pcg64_Fast() =
    pcg64_Fast.Next( 0UL , 100UL )

  [< Benchmark >]
  member this.Pcg64_Invertible() =
    pcg64_Invertible.Next( 0UL , 100UL )

  [< Benchmark >]
  member this.Pcg128_Invertible() =
    pcg128_Invertible.Next( 0I , 100I )

[<EntryPoint>]
let main argv =
  BenchmarkSwitcher
    .FromTypes( [| typeof< NextBenchmark > ; typeof< NextBoundBenchmark > ; typeof< NextRangeBenchmark > |] )
    .RunAll()
    |> ignore
  0 // return an integer exit code
