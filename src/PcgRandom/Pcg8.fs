namespace Zanaptak.PcgRandom.Pcg8Variants
  /// Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.
  type Invertible =
    | RXS_M_XS
    | Default
  /// Faster variant but with reduced statistical quality. Based on MCG generator type.
  type Fast =
    | XSH_RR
    | XSH_RS
    | RXS_M
    | Default
  /// General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.
  type Normal =
    | XSH_RR
    | XSH_RS
    | RXS_M
    | Default

namespace Zanaptak.PcgRandom
open Zanaptak.PcgRandom.Utils
open Zanaptak.PcgRandom.Pcg8Variants

/// PCG 8-bit pseudorandom number generator
type Pcg8 private ( name : string , nextFn : unit -> uint8 ) =
  static let pcg_output_xsh_rr_16_8 ( state : uint16 ) =
    rotateRight8 ( ( ( state >>> 5 ) ^^^ state ) >>> 5 |> uint8 ) ( state >>> 13 |> int )
  static let pcg_output_xsh_rs_16_8 ( state : uint16 ) =
    ( ( state >>> 7 ) ^^^ state ) >>> int ( ( state >>> 14 ) + 3us ) |> uint8
  static let pcg_output_rxs_m_16_8 ( state : uint16 ) =
    ( ( state >>> ( ( state >>> 13 |> int ) + 3 ) ) ^^^ state ) * 62169us >>> 8 |> uint8
  static let pcg_output_rxs_m_xs_8_8 ( state : uint8 ) =
    let word = ( ( state >>> ( ( state >>> 6 |> int ) + 2 ) ) ^^^ state ) * 217uy |> truncateForJs8
    ( word >>> 6 ) ^^^ word

  let nextBytesCount , nextBytesFn = 1 , fun () -> [| nextFn() |]

  /// PCG 8-bit pseudorandom number generator
  private new ( variant : Normal , seed : uint16 , streamOpt : uint16 option ) =
    let increment =
      match streamOpt with
      | Some stream -> stream <<< 1 ||| 1us |> truncateForJs16
      | None -> PCG_DEFAULT_INCREMENT_16
    let stepFn = pcg_setseq_16_step_r
    let outputFn , name =
      match variant with
      | Normal.Default
      | Normal.XSH_RR -> pcg_output_xsh_rr_16_8 , "PCG XSH RR 16/8 (LCG)"
      | Normal.XSH_RS -> pcg_output_xsh_rs_16_8 , "PCG XSH RS 16/8 (LCG)"
      | Normal.RXS_M -> pcg_output_rxs_m_16_8 , "PCG RXS M 16/8 (LCG)"
    let nextFn =
      let mutable state = 0us
      state <- stepFn increment state
      state <- state + seed |> truncateForJs16
      state <- stepFn increment state
      fun () ->
        let prevState = state
        state <- stepFn increment state
        outputFn prevState
    Pcg8( name , nextFn )

  /// PCG 8-bit pseudorandom number generator
  private new ( variant : Invertible , seed : uint8 , streamOpt : uint8 option ) =
    let increment =
      match streamOpt with
      | Some stream -> stream <<< 1 ||| 1uy |> truncateForJs8
      | None -> PCG_DEFAULT_INCREMENT_8
    let stepFn = pcg_setseq_8_step_r
    let outputFn , name =
      match variant with
      | Invertible.Default
      | Invertible.RXS_M_XS -> pcg_output_rxs_m_xs_8_8 , "PCG RXS M XS 8/8 (LCG)"
    let nextFn =
      let mutable state = 0uy
      state <- stepFn increment state
      state <- state + seed |> truncateForJs8
      state <- stepFn increment state
      fun () ->
        let prevState = state
        state <- stepFn increment state
        outputFn prevState
    Pcg8( name , nextFn )

  /// PCG 8-bit pseudorandom number generator
  new ( variant : Fast , seed : uint16 ) =
    let stepFn = pcg_mcg_16_step_r
    let outputFn , name =
      match variant with
      | Fast.Default
      | Fast.XSH_RR -> pcg_output_xsh_rr_16_8 , "PCG XSH RR 16/8 (MCG)"
      | Fast.XSH_RS -> pcg_output_xsh_rs_16_8 , "PCG XSH RS 16/8 (MCG)"
      | Fast.RXS_M -> pcg_output_rxs_m_16_8 , "PCG RXS M 16/8 (MCG)"
    let nextFn =
      let mutable state = seed ||| 1us |> truncateForJs16
      fun () ->
        let prevState = state
        state <- stepFn state
        outputFn prevState
    Pcg8( name , nextFn )

  /// PCG 8-bit pseudorandom number generator
  new ( variant : Normal , seed : uint16 , stream : uint16 ) = Pcg8( variant , seed , Some stream )
  /// PCG 8-bit pseudorandom number generator
  new ( variant : Normal , seed : uint16 ) = Pcg8( variant , seed , None )
  /// PCG 8-bit pseudorandom number generator
  new ( variant : Normal ) = Pcg8( variant , seed16 () , None )

  /// PCG 8-bit pseudorandom number generator
  new ( variant : Invertible , seed : uint8 , stream : uint8 ) = Pcg8( variant , seed , Some stream )
  /// PCG 8-bit pseudorandom number generator
  new ( variant : Invertible , seed : uint8 ) = Pcg8( variant , seed , None )
  /// PCG 8-bit pseudorandom number generator
  new ( variant : Invertible ) = Pcg8( variant , seed8 () , None )

  /// PCG 8-bit pseudorandom number generator
  new ( variant : Fast ) = Pcg8( variant , seed16 () )

  /// PCG 8-bit pseudorandom number generator
  new ( seed : uint16 , stream : uint16 ) = Pcg8( Normal.Default , seed , Some stream )
  /// PCG 8-bit pseudorandom number generator
  new ( seed : uint16 ) = Pcg8( Normal.Default , seed , None )
  /// PCG 8-bit pseudorandom number generator
  new () = Pcg8( Normal.Default , seed16 () , None )

  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary to fill array.
  member this.NextBytes( bytes ) = Zanaptak.PcgRandom.Utils.nextBytes nextBytesFn nextBytesCount bytes
  /// Returns a random 8-bit unsigned integer greater than or equal to the specified minimum and less than the specified maximum.
  member this.Next( minInclusive : uint8 , maxExclusive : uint8 ) =
    if minInclusive > maxExclusive then
      raise ( System.ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )
    elif minInclusive = maxExclusive then minInclusive
    else
      let bound = maxExclusive - minInclusive
      let threshold = ( uint8 -( int8 bound ) ) % bound
      let rec loop () =
        let r = nextFn ()
        if r >= threshold then r % bound + minInclusive else loop ()
      loop ()
  /// Returns a random 8-bit unsigned integer less than the specified maximum.
  member this.Next( maxExclusive : uint8 ) =
    if maxExclusive = 0uy then 0uy
    else
      let threshold = ( uint8 -( int8 maxExclusive ) ) % maxExclusive
      let rec loop () =
        let r = nextFn ()
        if r >= threshold then r % maxExclusive else loop ()
      loop ()
  /// Returns a random 8-bit unsigned integer.
  member this.Next() = nextFn ()
  override this.ToString() = name
