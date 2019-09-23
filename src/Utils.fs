module internal Zanaptak.PcgRandom.Utils
open System

// http://www.pcg-random.org/
// https://github.com/imneme/pcg-c/blob/master/include/pcg_variants.h

#if FABLE_COMPILER
/// JS-friendly uint32 multiply
/// https://stackoverflow.com/a/28151933
let [< Literal >] multiply32Limit = 0xfffffu
let inline multiply32 ( a : uint32 ) ( b : uint32 ) =
  let a = a >>> 0
  let b = b >>> 0
  if a <= multiply32Limit || b <= multiply32Limit then a * b >>> 0
  else
    let alo = a &&& 0xffffu
    let ahi = a - alo
    ( ( ahi * b >>> 0 ) + ( alo * b ) ) >>> 0
#else
let inline multiply32 ( a : uint32 ) ( b : uint32 ) = a * b
#endif

#if FABLE_COMPILER
let inline truncateForJs8 value = value &&& 0xFFuy
let inline truncateForJs16 value = value &&& 0xFFFFus
let inline truncateForJs32 value = value >>> 0
#else
let inline truncateForJs8 value = value
let inline truncateForJs16 value = value
let inline truncateForJs32 value = value
#endif

let inline rotateRight8 ( value : uint8 ) ( count : int ) =
  ( ( value >>> ( count &&& 0b111 ) ) ||| ( value <<< ( ( 8 - count ) &&& 0b111 ) ) ) |> truncateForJs8
let inline rotateRight16 ( value : uint16 ) ( count : int ) =
  ( ( value >>> ( count &&& 0b1111 ) ) ||| ( value <<< ( ( 16 - count ) &&& 0b1111 ) ) ) |> truncateForJs16
let inline rotateRight32 ( value : uint32 ) ( count : int ) =
  ( value >>> ( count &&& 0b11111 ) ) ||| ( value <<< ( ( 32 - count ) &&& 0b11111 ) ) |> truncateForJs32
let inline rotateRight64 ( value : uint64 ) ( count : int ) =
  ( value >>> count ) ||| ( value <<< ( 64 - count ) )

let seed8 () = System.Random().Next() |> uint8
let seed16 () = System.Random().Next() |> uint16
let seed32 () =
  let seedGen = System.Random()
  let hi , lo = seedGen.Next() |> uint32 , seedGen.Next() |> uint32
  hi <<< 16 ||| ( lo &&& 0xffffu )
let seed64 () =
  let seedGen = System.Random()
  // use only 31 bits of each part since high bit is always 0
  let seedParts = Array.init 3 ( fun _ -> seedGen.Next() |> uint64 )
  seedParts |> Array.reduce ( fun a b -> a <<< 31 ||| ( b &&& 0x7fffffffUL ) )

let [< Literal >] PCG_DEFAULT_MULTIPLIER_8 = 141uy
let [< Literal >] PCG_DEFAULT_INCREMENT_8 = 77uy
let [< Literal >] PCG_DEFAULT_MULTIPLIER_16 = 12829us
let [< Literal >] PCG_DEFAULT_INCREMENT_16 = 47989us
let [< Literal >] PCG_DEFAULT_MULTIPLIER_32 = 747796405u
let [< Literal >] PCG_DEFAULT_INCREMENT_32 = 2891336453u
let [< Literal >] PCG_DEFAULT_MULTIPLIER_64 = 6364136223846793005UL
let [< Literal >] PCG_DEFAULT_INCREMENT_64 = 1442695040888963407UL

let inline pcg_setseq_8_step_r ( increment : uint8 ) ( state : uint8 ) =
  state * PCG_DEFAULT_MULTIPLIER_8 + increment |> truncateForJs8
let inline pcg_setseq_16_step_r ( increment : uint16 ) ( state : uint16 ) =
  state * PCG_DEFAULT_MULTIPLIER_16 + increment |> truncateForJs16
let inline pcg_mcg_16_step_r ( state : uint16 ) =
  state * PCG_DEFAULT_MULTIPLIER_16 |> truncateForJs16
let inline pcg_setseq_32_step_r ( increment : uint32 ) ( state : uint32 ) =
  multiply32 state PCG_DEFAULT_MULTIPLIER_32 + increment |> truncateForJs32
let inline pcg_mcg_32_step_r ( state : uint32 ) =
  multiply32 state PCG_DEFAULT_MULTIPLIER_32
let inline pcg_setseq_64_step_r ( increment : uint64 ) ( state : uint64 ) =
  state * PCG_DEFAULT_MULTIPLIER_64 + increment
let inline pcg_mcg_64_step_r ( state : uint64 ) =
  state * PCG_DEFAULT_MULTIPLIER_64

let nextBytes ( nextBytesFn : unit -> byte array ) ( nextBytesCount : int ) ( bytes : byte array ) =
  if bytes = null then raise ( ArgumentException( "byte array cannot be null" ) )
  if bytes.Length = 0 then ()
  else
    let mutable writeIndex = 0
    while writeIndex < bytes.Length do
      let bytesToWrite = bytes.Length - writeIndex |> min 65536
      let randValueCount = float bytesToWrite / float nextBytesCount |> ceil |> int
      let randArray =
        Array.init randValueCount ( fun _ -> nextBytesFn () )
        |> Array.concat
      Array.Copy( randArray , 0 , bytes , writeIndex , bytesToWrite )
      writeIndex <- writeIndex + bytesToWrite
