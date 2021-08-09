namespace Zanaptak.PcgRandom.Pcg16Variants
    /// Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.
    type Invertible =
        | RXS_M_XS
        /// Default: RXS_M_XS
        | Default
    /// Faster variant but with reduced statistical quality. Based on MCG generator type.
    type Fast =
        | XSH_RR
        | XSH_RS
        | RXS_M
        /// Default: XSH_RR
        | Default
    /// General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.
    type Normal =
        | XSH_RR
        | XSH_RS
        | RXS_M
        /// Default: XSH_RR
        | Default

namespace Zanaptak.PcgRandom
open System
open Zanaptak.PcgRandom.Utils
open Zanaptak.PcgRandom.Pcg16Variants

/// PCG 16-bit pseudorandom number generator
type Pcg16 internal ( name : string , nextFn : unit -> uint16 ) =
    static let pcg_output_xsh_rr_32_16 ( state : uint32 ) =
        rotateRight16 ( ( ( state >>> 10 ) ^^^ state ) >>> 12 |> uint16 ) ( state >>> 28 |> int )
    static let pcg_output_xsh_rs_32_16 ( state : uint32 ) =
        ( ( state >>> 11 ) ^^^ state ) >>> int ( ( state >>> 30 ) + 11u ) |> uint16
    static let pcg_output_rxs_m_32_16 ( state : uint32 ) =
        multiply32 ( ( state >>> ( ( state >>> 28 |> int ) + 4 ) ) ^^^ state ) 277803737u >>> 16 |> uint16
    static let pcg_output_rxs_m_xs_16_16 ( state : uint16 ) =
        let word = ( ( state >>> ( ( state >>> 13 |> int ) + 3 ) ) ^^^ state ) * 62169us |> truncateForJs16
        ( word >>> 11 ) ^^^ word

    let [< Literal >] Int16Max = 32767us

    let nextBytes ( bytes : byte array ) startIndex length =
        let endIndex = startIndex + length - 1
        for i in startIndex .. 2 .. endIndex do
            let randVal = nextFn ()
            for j in 0 .. min 1 ( endIndex - i ) do
                bytes.[ i + j ] <- randVal >>> ( j * 8 ) |> byte

    private new ( variant : Normal , seed : uint32 , streamOpt : uint32 option ) =
        let increment =
            match streamOpt with
            | Some stream -> stream <<< 1 ||| 1u |> truncateForJs32
            | None -> PCG_DEFAULT_INCREMENT_32
        let stepFn = pcg_setseq_32_step_r
        let outputFn , name =
            match variant with
            | Normal.Default
            | Normal.XSH_RR -> pcg_output_xsh_rr_32_16 , "PCG XSH RR 32/16 (LCG)"
            | Normal.XSH_RS -> pcg_output_xsh_rs_32_16 , "PCG XSH RS 32/16 (LCG)"
            | Normal.RXS_M -> pcg_output_rxs_m_32_16 , "PCG RXS M 32/16 (LCG)"
        let nextFn =
            let mutable state = 0u
            state <- stepFn increment state
            state <- state + seed |> truncateForJs32
            state <- stepFn increment state
            fun () ->
                let prevState = state
                state <- stepFn increment state
                outputFn prevState
        Pcg16( name , nextFn )

    private new ( variant : Invertible , seed : uint16 , streamOpt : uint16 option ) =
        let increment =
            match streamOpt with
            | Some stream -> stream <<< 1 ||| 1us |> truncateForJs32
            | None -> PCG_DEFAULT_INCREMENT_16
        let stepFn = pcg_setseq_16_step_r
        let outputFn , name =
            match variant with
            | Invertible.Default
            | Invertible.RXS_M_XS -> pcg_output_rxs_m_xs_16_16 , "PCG RXS M XS 16/16 (LCG)"
        let nextFn =
            let mutable state = 0us
            state <- stepFn increment state
            state <- state + seed |> truncateForJs32
            state <- stepFn increment state
            fun () ->
                let prevState = state
                state <- stepFn increment state
                outputFn prevState
        Pcg16( name , nextFn )

    /// Specify Fast variant with seed.
    new ( variant : Fast , seed : uint32 ) =
        let stepFn = pcg_mcg_32_step_r
        let outputFn , name =
            match variant with
            | Fast.Default
            | Fast.XSH_RR -> pcg_output_xsh_rr_32_16 , "PCG XSH RR 32/16 (MCG)"
            | Fast.XSH_RS -> pcg_output_xsh_rs_32_16 , "PCG XSH RS 32/16 (MCG)"
            | Fast.RXS_M -> pcg_output_rxs_m_32_16 , "PCG RXS M 32/16 (MCG)"
        let nextFn =
            let mutable state = seed ||| 1u |> truncateForJs32
            fun () ->
                let prevState = state
                state <- stepFn state
                outputFn prevState
        Pcg16( name , nextFn )

    /// Specify Fast variant.
    new ( variant : Fast ) = Pcg16( variant , seed32 () )

    /// Specify Normal variant with seed and stream.
    new ( variant : Normal , seed : uint32 , stream : uint32 ) = Pcg16( variant , seed , Some stream )
    /// Specify Normal variant with seed.
    new ( variant : Normal , seed : uint32 ) = Pcg16( variant , seed , None )
    /// Specify Normal variant.
    new ( variant : Normal ) = Pcg16( variant , seed32 () , None )

    /// Specify Invertible variant with seed and stream.
    new ( variant : Invertible , seed : uint16 , stream : uint16 ) = Pcg16( variant , seed , Some stream )
    /// Specify Invertible variant with seed.
    new ( variant : Invertible , seed : uint16 ) = Pcg16( variant , seed , None )
    /// Specify Invertible variant.
    new ( variant : Invertible ) = Pcg16( variant , seed16 () , None )

    /// Use default variant (Normal.XSH_RR) with seed and stream.
    new ( seed : uint32 , stream : uint32 ) = Pcg16( Normal.Default , seed , Some stream )
    /// Use default variant (Normal.XSH_RR) with seed.
    new ( seed : uint32 ) = Pcg16( Normal.Default , seed , None )
    /// Use default variant (Normal.XSH_RR).
    new () = Pcg16( Normal.Default , seed32 () , None )

    /// Returns name of PCG algorithm for this instance.
    override this.ToString() = name

    /// Returns a random 16-bit unsigned integer greater than or equal to the specified minimum and less than the specified maximum.
    member this.Next( minInclusive : uint16 , maxExclusive : uint16 ) =
        if maxExclusive > minInclusive then
            let bound = maxExclusive - minInclusive
            let threshold = ( uint16 -( int16 bound ) ) % bound
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % bound + minInclusive else loop ()
            loop ()
        elif minInclusive = maxExclusive then minInclusive
        else raise ( ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )

    /// Returns a random 16-bit unsigned integer less than the specified maximum.
    member this.Next( maxExclusive : uint16 ) =
        if maxExclusive > 0us then
            let threshold = ( uint16 -( int16 maxExclusive ) ) % maxExclusive
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % maxExclusive else loop ()
            loop ()
        else 0us

    /// Returns a random 16-bit unsigned integer.
    member this.Next() = nextFn ()

    /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary.
    member this.NextBytes( bytes : byte array ) =
        if ( not ( isNull bytes ) ) && bytes.Length > 0 then
            nextBytes bytes 0 bytes.Length
        elif isNull bytes then
            raise ( ArgumentException( "byte array cannot be null" ) )
        else ()

    /// Sets the specified bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary.
    member this.NextBytes( bytes : byte array , startIndex , length ) =
        if ( not ( isNull bytes ) ) && startIndex >= 0 && length > 0 && ( startIndex + length ) <= bytes.Length then
            nextBytes bytes startIndex length
        elif isNull bytes then
            raise ( ArgumentException( "byte array cannot be null" ) )
        elif startIndex < 0 || startIndex >= bytes.Length then
            raise ( ArgumentOutOfRangeException( "start index must be within array bounds" ) )
        elif ( startIndex + length ) > bytes.Length then
            raise ( ArgumentException( "length parameter must not exceed number of elements from start index to end of array" ) )
        else ()

    /// Returns a random boolean.
    member this.NextBoolean() = nextFn() > Int16Max

    // http://prng.di.unimi.it/
    // A standard double (64-bit) floating-point number in IEEE floating point format has 52 bits of significand, plus an implicit bit at the left of the significand.
    // Thus, the representation can actually store numbers with 53 significant binary digits.
    // Because of this fact, in C99 a 64-bit unsigned integer x should be converted to a 64-bit double using the expression
    //    #include <stdint.h>
    //    (x >> 11) * (1. / (UINT64_C(1) << 53))
    // This conversion guarantees that all dyadic rationals of the form k / 2−53 will be equally likely.
    // Note that this conversion prefers the high bits of x (usually, a good idea), but you can alternatively use the lowest bits.

    /// Returns a random double greater than or equal to 0.0 and less than 1.0 (using 53 random bits). Consumes 4 values from the generator.
    member this.NextDouble() =
        let next64bits =
            uint64( nextFn() )
            ||| ( uint64( nextFn() ) <<< 16 )
            ||| ( uint64( nextFn() ) <<< 32 )
            ||| ( uint64( nextFn() ) <<< 48 )
        float( next64bits >>> 11 ) * 0.000000000000000111022302462515654042363166809082031250000000
        // Result with 53 1-bits is: 0.999999999999999888977697537484345957636833190917968750000000
