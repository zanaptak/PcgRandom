namespace Zanaptak.PcgRandom.Pcg32Variants
  /// Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.
  type Invertible =
    | RXS_M_XS
    | Default
  /// Faster variant but with reduced statistical quality. Based on MCG generator type.
  type Fast =
    | XSH_RR
    | XSH_RS
    | XSL_RR
    | RXS_M
    | Default
  /// General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.
  type Normal =
    | XSH_RR
    | XSH_RS
    | XSL_RR
    | RXS_M
    | Default

namespace Zanaptak.PcgRandom
open System
open Zanaptak.PcgRandom.Utils
open Zanaptak.PcgRandom.Pcg32Variants

/// PCG 32-bit pseudorandom number generator
type Pcg32 internal ( name : string , nextFn : unit -> uint32 ) =
  static let pcg_output_xsh_rr_64_32 ( state : uint64 ) =
    rotateRight32 ( ( ( state >>> 18 ) ^^^ state ) >>> 27 |> uint32 ) ( state >>> 59 |> int )
  static let pcg_output_xsh_rs_64_32 ( state : uint64 ) =
    ( ( state >>> 22 ) ^^^ state ) >>> int ( ( state >>> 61 ) + 22UL ) |> uint32
  static let pcg_output_xsl_rr_64_32 ( state : uint64 ) =
    rotateRight32 ( ( ( state >>> 32 ) ^^^ state ) |> uint32 ) ( state >>> 59 |> int )
  static let pcg_output_rxs_m_64_32 ( state : uint64 ) =
    ( ( state >>> ( ( state >>> 59 |> int ) + 5 ) ) ^^^ state ) * 12605985483714917081UL >>> 32 |> uint32
  static let pcg_output_rxs_m_xs_32_32 ( state : uint32 ) =
    let word = multiply32 ( ( state >>> ( ( state >>> 28 |> int ) + 4 ) ) ^^^ state ) 277803737u
    ( word >>> 22 ) ^^^ word

  let nextBytesCount , nextBytesFn = 4 , nextFn >> BitConverter.GetBytes

  /// PCG 32-bit pseudorandom number generator
  private new ( variant : Normal , seed : uint64 , streamOpt : uint64 option ) =
    let increment =
      match streamOpt with
      | Some stream -> stream <<< 1 ||| 1UL
      | None -> PCG_DEFAULT_INCREMENT_64
    let stepFn = pcg_setseq_64_step_r
    let outputFn , name =
      match variant with
      | Normal.Default
      | Normal.XSH_RR -> pcg_output_xsh_rr_64_32 , "PCG XSH RR 64/32 (LCG)"
      | Normal.XSH_RS -> pcg_output_xsh_rs_64_32 , "PCG XSH RS 64/32 (LCG)"
      | Normal.XSL_RR -> pcg_output_xsl_rr_64_32 , "PCG XSL RR 64/32 (LCG)"
      | Normal.RXS_M -> pcg_output_rxs_m_64_32 , "PCG RXS M 64/32 (LCG)"
    let nextFn =
      let mutable state = 0UL
      state <- stepFn increment state
      state <- state + seed
      state <- stepFn increment state
      fun () ->
        let prevState = state
        state <- stepFn increment state
        outputFn prevState
    Pcg32( name , nextFn )

  /// PCG 32-bit pseudorandom number generator
  private new ( variant : Invertible , seed : uint32 , streamOpt : uint32 option ) =
    let increment =
      match streamOpt with
      | Some stream -> stream <<< 1 ||| 1u |> truncateForJs32
      | None -> PCG_DEFAULT_INCREMENT_32
    let stepFn = pcg_setseq_32_step_r
    let outputFn , name =
      match variant with
      | Invertible.Default
      | Invertible.RXS_M_XS -> pcg_output_rxs_m_xs_32_32 , "PCG RXS M XS 32/32 (LCG)"
    let nextFn =
      let mutable state = 0u
      state <- stepFn increment state
      state <- state + seed |> truncateForJs32
      state <- stepFn increment state
      fun () ->
        let prevState = state
        state <- stepFn increment state
        outputFn prevState
    Pcg32( name , nextFn )

  /// PCG 32-bit pseudorandom number generator
  new ( variant : Fast , seed : uint64 ) =
    let stepFn = pcg_mcg_64_step_r
    let outputFn , name =
      match variant with
      | Fast.Default
      | Fast.XSH_RR -> pcg_output_xsh_rr_64_32 , "PCG XSH RR 64/32 (MCG)"
      | Fast.XSH_RS -> pcg_output_xsh_rs_64_32 , "PCG XSH RS 64/32 (MCG)"
      | Fast.XSL_RR -> pcg_output_xsl_rr_64_32 , "PCG XSL RR 64/32 (MCG)"
      | Fast.RXS_M -> pcg_output_rxs_m_64_32 , "PCG RXS M 64/32 (MCG)"
    let nextFn =
      let mutable state = seed ||| 1UL
      fun () ->
        let prevState = state
        state <- stepFn state
        outputFn prevState
    Pcg32( name , nextFn )

  /// PCG 32-bit pseudorandom number generator
  new ( variant : Normal , seed : uint64 , stream : uint64 ) = Pcg32( variant , seed , Some stream )
  /// PCG 32-bit pseudorandom number generator
  new ( variant : Normal , seed : uint64 ) = Pcg32( variant , seed , None )
  /// PCG 32-bit pseudorandom number generator
  new ( variant : Normal ) = Pcg32( variant , seed64 () , None )

  /// PCG 32-bit pseudorandom number generator
  new ( variant : Invertible , seed : uint32 , stream : uint32 ) = Pcg32( variant , seed , Some stream )
  /// PCG 32-bit pseudorandom number generator
  new ( variant : Invertible , seed : uint32 ) = Pcg32( variant , seed , None )
  /// PCG 32-bit pseudorandom number generator
  new ( variant : Invertible ) = Pcg32( variant , seed32 () , None )

  /// PCG 32-bit pseudorandom number generator
  new ( variant : Fast ) = Pcg32( variant , seed64 () )

  /// PCG 32-bit pseudorandom number generator
  new ( seed : uint64 , stream : uint64 ) = Pcg32( Normal.Default , seed , Some stream )
  /// PCG 32-bit pseudorandom number generator
  new ( seed : uint64 ) = Pcg32( Normal.Default , seed , None )
  /// PCG 32-bit pseudorandom number generator
  new () = Pcg32( Normal.Default , seed64 () , None )

  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary to fill array.
  member this.NextBytes( bytes ) = nextBytes nextBytesFn nextBytesCount bytes
  /// Returns a random 32-bit unsigned integer greater than or equal to the specified minimum and less than the specified maximum.
  member this.Next( minInclusive : uint32 , maxExclusive : uint32 ) =
    if minInclusive > maxExclusive then
      raise ( System.ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )
    elif minInclusive = maxExclusive then minInclusive
    else
      let bound = maxExclusive - minInclusive
      let threshold = ( uint32 -( int32 bound ) ) % bound
      let rec loop () =
        let r = nextFn ()
        if r >= threshold then r % bound + minInclusive else loop ()
      loop ()
  /// Returns a random 32-bit unsigned integer less than the specified maximum.
  member this.Next( maxExclusive : uint32 ) =
    if maxExclusive = 0u then 0u
    else
      let threshold = ( uint32 -( int32 maxExclusive ) ) % maxExclusive
      let rec loop () =
        let r = nextFn ()
        if r >= threshold then r % maxExclusive else loop ()
      loop ()
  /// Returns a random 32-bit unsigned integer.
  member this.Next() = nextFn ()
  override this.ToString() = name
