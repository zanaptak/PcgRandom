module internal Zanaptak.PcgRandom.BigintUtils

open System
open System.Numerics

let PCG_128BIT_CONSTANT ( hi : uint64 ) ( lo : uint64 ) = ( bigint hi <<< 64 ) + bigint lo

let PCG_DEFAULT_MULTIPLIER_128 =
  PCG_128BIT_CONSTANT 2549297995355413924UL 4865540595714422341UL
let PCG_DEFAULT_INCREMENT_128 =
  PCG_128BIT_CONSTANT 6364136223846793005UL 1442695040888963407UL

let bigintMaxInt32 = bigint Int32.MaxValue
let bigintMaxUInt32 = bigint UInt32.MaxValue
let bigintMaxUInt64 = bigint UInt64.MaxValue
let bigintMaxUInt128 = PCG_128BIT_CONSTANT UInt64.MaxValue UInt64.MaxValue
let bigintMaxUInt128Plus1 = bigintMaxUInt128 + BigInteger.One

let inline bigintToUInt64 ( value : bigint ) = value &&& bigintMaxUInt64 |> uint64
let inline bigintToInt32 ( value : bigint ) = value &&& bigintMaxUInt32 |> int // mask to uint to include sign bit

let inline bigintToHiLoUInt64 value = bigintToUInt64 ( value >>> 64 ) , bigintToUInt64 value

let inline truncate128 value = value &&& bigintMaxUInt128

let inline pcg_setseq_128_step_r ( increment : bigint ) ( state : bigint ) =
  ( state * PCG_DEFAULT_MULTIPLIER_128 + increment ) |> truncate128
let inline pcg_mcg_128_step_r ( state : bigint ) =
  state * PCG_DEFAULT_MULTIPLIER_128 |> truncate128

let seed128 () =
  let seedGen = System.Random()
  // use only 31 bits of each part since high bit is always 0
  let seedParts = Array.init 5 ( fun _ -> seedGen.Next() |> bigint )
  seedParts |> Array.reduce ( fun a b -> a <<< 31 ||| ( b &&& bigintMaxInt32 ) ) |> truncate128
