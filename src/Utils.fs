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

#if FABLE_COMPILER

open Fable.Core
open Fable.Core.JsInterop

#if ZANAPTAK_NODEJS_CRYPTO

let crypto : obj = importDefault "crypto"
let getSeedBytes =
  fun ( bytes : byte array ) -> crypto?randomFillSync( bytes )

#else

let [<Global>] window: obj = jsNative
[<Emit("typeof $0 !== 'undefined'")>]
let isNotTypeofUndefined (x: 'a) : bool = jsNative

let getSeedBytes =
  if isNotTypeofUndefined window && window?crypto && window?crypto?getRandomValues then
    fun ( bytes : byte array ) -> window?crypto?getRandomValues( bytes )
  elif isNotTypeofUndefined window && window?msCrypto && window?msCrypto?getRandomValues then
    fun ( bytes : byte array ) -> window?msCrypto?getRandomValues( bytes )
  else
    let systemRng = System.Random()
    fun ( bytes : byte array ) -> systemRng.NextBytes( bytes )

#endif

#else

let getSeedBytes =
  let cryptoRng = System.Security.Cryptography.RandomNumberGenerator.Create()
  fun ( bytes : byte array ) -> cryptoRng.GetBytes( bytes )

#endif

let seed8 () : uint8 =
  let bytes = Array.create 1 0uy
  getSeedBytes bytes
  bytes.[ 0 ]
let seed16 () =
  let bytes = Array.create 2 0uy
  getSeedBytes bytes
  BitConverter.ToUInt16( bytes , 0 )
let seed32 () =
  let bytes = Array.create 4 0uy
  getSeedBytes bytes
  BitConverter.ToUInt32( bytes , 0 )
let seed64 () =
  let bytes = Array.create 8 0uy
  getSeedBytes bytes
  BitConverter.ToUInt64( bytes , 0 )

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
