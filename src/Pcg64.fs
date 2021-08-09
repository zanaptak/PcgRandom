namespace Zanaptak.PcgRandom.Pcg64Variants
    /// Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.
    type Invertible =
        | RXS_M_XS
        | XSL_RR_RR
        /// Default: RXS_M_XS
        | Default
    /// Faster variant but with reduced statistical quality. Based on MCG generator type.
    type Fast =
        | XSH_RR
        | XSH_RS
        | XSL_RR
        | RXS_M
        /// Default: XSL_RR
        | Default
    /// General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.
    type Normal =
        | XSH_RR
        | XSH_RS
        | XSL_RR
        | RXS_M
        /// Default: XSL_RR
        | Default

namespace Zanaptak.PcgRandom
open System
open System.Numerics
open Zanaptak.PcgRandom.Utils
open Zanaptak.PcgRandom.BigintUtils
open Zanaptak.PcgRandom.Pcg64Variants

/// PCG 64-bit pseudorandom number generator
type Pcg64 internal ( name : string , nextFn : unit -> uint64 ) =
    static let pcg_output_xsh_rr_128_64 ( state : bigint ) =
        rotateRight64 ( ( ( state >>> 35 ) ^^^ state ) >>> 58 |> bigintToUInt64 ) ( state >>> 122 |> int )
    static let pcg_output_xsh_rs_128_64 ( state : bigint ) =
        ( ( state >>> 43 ) ^^^ state ) >>> ( ( state >>> 124 |> int ) + 45 ) |> bigintToUInt64
    static let pcg_output_xsl_rr_128_64 ( state : bigint ) =
        rotateRight64 ( ( state >>> 64 |> bigintToUInt64 ) ^^^ bigintToUInt64 state ) ( state >>> 122 |> int )
    static let MULTIPLIER_RXS_M_128_64 = PCG_128BIT_CONSTANT 17766728186571221404UL 12605985483714917081UL
    static let pcg_output_rxs_m_128_64 ( state : bigint ) =
        ( ( state >>> ( ( state >>> 122 |> int ) + 6 ) ) ^^^ state ) * MULTIPLIER_RXS_M_128_64 >>> 64 |> bigintToUInt64
    static let pcg_output_rxs_m_xs_64_64 ( state : uint64 ) =
        let word = ( ( state >>> ( ( state >>> 59 |> int ) + 5 ) ) ^^^ state ) * 12605985483714917081UL
        ( word >>> 43 ) ^^^ word
    static let pcg_output_xsl_rr_rr_64_64 ( state : uint64 ) =
        let rot1 = state >>> 59 |> int
        let high = state >>> 32 |> uint32
        let low = state |> uint32
        let xored = high ^^^ low
        let newlow = rotateRight32 xored rot1
        let newhigh = rotateRight32 high ( newlow &&& 31u |> int )
        ( uint64 newhigh ) <<< 32 ||| uint64 newlow

    let [< Literal >] Int64Max = 9223372036854775807UL

    let nextBytes ( bytes : byte array ) startIndex length =
        let endIndex = startIndex + length - 1
        for i in startIndex .. 8 .. endIndex do
            let randVal = nextFn ()
            for j in 0 .. min 7 ( endIndex - i ) do
                bytes.[ i + j ] <- randVal >>> ( j * 8 ) |> byte

    private new ( variant : Normal , seed : bigint , streamOpt : bigint option ) =
        let increment =
            match streamOpt with
            | Some stream -> stream <<< 1 ||| BigInteger.One |> truncate128
            | None -> PCG_DEFAULT_INCREMENT_128
        let stepFn = pcg_setseq_128_step_r
        let outputFn , name =
            match variant with
            | Normal.Default
            | Normal.XSL_RR -> pcg_output_xsl_rr_128_64 , "PCG XSL RR 128/64 (LCG)"
            | Normal.XSH_RR -> pcg_output_xsh_rr_128_64 , "PCG XSH RR 128/64 (LCG)"
            | Normal.XSH_RS -> pcg_output_xsh_rs_128_64 , "PCG XSH RS 128/64 (LCG)"
            | Normal.RXS_M -> pcg_output_rxs_m_128_64 , "PCG RXS M 128/64 (LCG)"
        let nextFn =
            let mutable state = BigInteger.Zero
            state <- stepFn increment state
            state <- state + seed |> truncate128
            state <- stepFn increment state
            fun () ->
                state <- stepFn increment state
                outputFn state
        Pcg64( name , nextFn )

    private new ( variant : Invertible , seed : uint64 , streamOpt : uint64 option ) =
        let increment =
            match streamOpt with
            | Some stream -> stream <<< 1 ||| 1UL
            | None -> PCG_DEFAULT_INCREMENT_64
        let stepFn = pcg_setseq_64_step_r
        let outputFn , name =
            match variant with
            | Invertible.Default
            | Invertible.RXS_M_XS -> pcg_output_rxs_m_xs_64_64 , "PCG RXS M XS 64/64 (LCG)"
            | Invertible.XSL_RR_RR -> pcg_output_xsl_rr_rr_64_64 , "PCG XSL RR RR 64/64 (LCG)"
        let nextFn =
            let mutable state = 0UL
            state <- stepFn increment state
            state <- state + seed
            state <- stepFn increment state
            fun () ->
                let prevState = state
                state <- stepFn increment state
                outputFn prevState
        Pcg64( name , nextFn )

    /// Specify Fast variant with seed.
    new ( variant : Fast , seed : bigint ) =
        let stepFn = pcg_mcg_128_step_r
        let outputFn , name =
            match variant with
            | Fast.Default
            | Fast.XSL_RR -> pcg_output_xsl_rr_128_64 , "PCG XSL RR 128/64 (MCG)"
            | Fast.XSH_RR -> pcg_output_xsh_rr_128_64 , "PCG XSH RR 128/64 (MCG)"
            | Fast.XSH_RS -> pcg_output_xsh_rs_128_64 , "PCG XSH RS 128/64 (MCG)"
            | Fast.RXS_M -> pcg_output_rxs_m_128_64 , "PCG RXS M 128/64 (MCG)"
        let nextFn =
            let mutable state = seed ||| BigInteger.One |> truncate128
            fun () ->
                // PCG reference implementation doesn't use cached prevState for 128bit.
                state <- stepFn state
                outputFn state
        Pcg64( name , nextFn )

    /// Specify Fast variant.
    new ( variant : Fast ) = Pcg64( variant , seed128 () )

    /// Specify Normal variant with seed and stream.
    new ( variant : Normal , seed : bigint , stream : bigint ) = Pcg64( variant , seed , Some stream )
    /// Specify Normal variant with seed.
    new ( variant : Normal , seed : bigint ) = Pcg64( variant , seed , None )
    /// Specify Normal variant.
    new ( variant : Normal ) = Pcg64( variant , seed128 () , None )

    /// Specify Invertible variant with seed and stream.
    new ( variant : Invertible , seed : uint64 , stream : uint64 ) = Pcg64( variant , seed , Some stream )
    /// Specify Invertible variant with seed.
    new ( variant : Invertible , seed : uint64 ) = Pcg64( variant , seed , None )
    /// Specify Invertible variant.
    new ( variant : Invertible ) = Pcg64( variant , seed64 () , None )

    /// Use default variant (Normal.XSL_RR) with seed and stream.
    new ( seed : bigint , stream : bigint ) = Pcg64( Normal.Default , seed , Some stream )
    /// Use default variant (Normal.XSL_RR) with seed.
    new ( seed : bigint ) = Pcg64( Normal.Default , seed , None )
    /// Use default variant (Normal.XSL_RR).
    new () = Pcg64( Normal.Default , seed128 () , None )

    /// Returns name of PCG algorithm for this instance.
    override this.ToString() = name

    /// Returns a random 64-bit unsigned integer greater than or equal to the specified minimum and less than the specified maximum.
    member this.Next( minInclusive : uint64 , maxExclusive : uint64 ) =
        if maxExclusive > minInclusive then
            let bound = maxExclusive - minInclusive
            let threshold = ( uint64 -( int64 bound ) ) % bound
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % bound + minInclusive else loop ()
            loop ()
        elif minInclusive = maxExclusive then minInclusive
        else raise ( ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )

    /// Returns a random 64-bit unsigned integer less than the specified maximum.
    member this.Next( maxExclusive : uint64 ) =
        if maxExclusive > 0UL then
            let threshold = ( uint64 -( int64 maxExclusive ) ) % maxExclusive
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % maxExclusive else loop ()
            loop ()
        else 0UL

    /// Returns a random 64-bit unsigned integer.
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
    member this.NextBoolean() = nextFn() > Int64Max

    // http://prng.di.unimi.it/
    // A standard double (64-bit) floating-point number in IEEE floating point format has 52 bits of significand, plus an implicit bit at the left of the significand.
    // Thus, the representation can actually store numbers with 53 significant binary digits.
    // Because of this fact, in C99 a 64-bit unsigned integer x should be converted to a 64-bit double using the expression
    //    #include <stdint.h>
    //    (x >> 11) * (1. / (UINT64_C(1) << 53))
    // This conversion guarantees that all dyadic rationals of the form k / 2−53 will be equally likely.
    // Note that this conversion prefers the high bits of x (usually, a good idea), but you can alternatively use the lowest bits.

    /// Returns a random double greater than or equal to 0.0 and less than 1.0 (using 53 random bits).
    member this.NextDouble() =
        float( nextFn() >>> 11 ) * 0.000000000000000111022302462515654042363166809082031250000000
        // Result with 53 1-bits is: 0.999999999999999888977697537484345957636833190917968750000000
