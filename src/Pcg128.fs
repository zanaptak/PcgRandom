namespace Zanaptak.PcgRandom.Pcg128Variants
    /// Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.
    type Invertible =
        | RXS_M_XS
        | XSL_RR_RR
        /// Default: XSL_RR_RR
        | Default

namespace Zanaptak.PcgRandom
open System
open System.Numerics
open Zanaptak.PcgRandom.Utils
open Zanaptak.PcgRandom.BigintUtils
open Zanaptak.PcgRandom.Pcg128Variants

/// PCG 128-bit pseudorandom number generator.
type Pcg128
    // Private primary ctor for public overloads to call, passing in appropriately configured step function.
    private ( name : string , nextFn : unit -> bigint ) =

    static let MULTIPLIER_RXS_M_XS_128_128 = PCG_128BIT_CONSTANT 17766728186571221404UL 12605985483714917081UL
    static let pcg_output_rxs_m_xs_128_128 ( state : bigint ) =
        let word = ( ( state >>> ( ( state >>> 122 |> bigintToInt32 ) + 6 ) ) ^^^ state ) * MULTIPLIER_RXS_M_XS_128_128 |> truncate128
        ( word >>> 86 ) ^^^ word
    static let pcg_output_xsl_rr_rr_128_128 ( state : bigint ) =
        let rot1 = state >>> 122 |> bigintToInt32
        let high = state >>> 64 |> bigintToUInt64
        let low = state |> bigintToUInt64
        let xored = high ^^^ low
        let newlow = rotateRight64 xored rot1
        let newhigh = rotateRight64 high ( newlow &&& 63UL |> int )
        ( bigint newhigh ) <<< 64 ||| bigint newlow

    let Int128Max = 170141183460469231731687303715884105727I

    let nextBytes ( bytes : byte array ) startIndex length =
        let endIndex = startIndex + length - 1
        for i in startIndex .. 16 .. endIndex do
            let randVal = nextFn ()
            for j in 0 .. min 15 ( endIndex - i ) do
                bytes.[ i + j ] <- ( randVal >>> ( j * 8 ) ) &&& 255I |> byte

    private new ( variant : Invertible , seed : bigint , streamOpt : bigint option ) =
        let increment =
            match streamOpt with
            | Some stream -> stream <<< 1 ||| BigInteger.One |> truncate128
            | None -> PCG_DEFAULT_INCREMENT_128
        let stepFn = pcg_setseq_128_step_r
        let outputFn , name =
            match variant with
            | Invertible.Default
            | Invertible.XSL_RR_RR -> pcg_output_xsl_rr_rr_128_128 , "PCG XSL RR RR 128/128 (LCG)"
            | Invertible.RXS_M_XS -> pcg_output_rxs_m_xs_128_128 , "PCG RXS M XS 128/128 (LCG)"
        let nextFn =
            let mutable state = BigInteger.Zero
            state <- stepFn increment state
            state <- state + seed |> truncate128
            state <- stepFn increment state
            fun () ->
                // PCG reference implementation doesn't use cached prevState for 128bit.
                state <- stepFn increment state
                outputFn state
        Pcg128( name , nextFn )

    /// Select Invertible variant with specified seed and stream.
    new ( variant : Invertible , seed : bigint , stream : bigint ) = Pcg128( variant , seed , Some stream )
    /// Select Invertible variant with specified seed.
    new ( variant : Invertible , seed : bigint ) = Pcg128( variant , seed , None )
    /// Select Invertible variant with random seed.
    new ( variant : Invertible ) = Pcg128( variant , seed128 () , None )

    /// Returns name of PCG algorithm for this instance.
    override this.ToString() = name

    /// Returns a random 128-bit unsigned integer greater than or equal to the specified minimum and less than the specified maximum.
    member this.Next( minInclusive : bigint , maxExclusive : bigint ) =
        if maxExclusive > minInclusive && minInclusive >= BigInteger.Zero then
            let bound = maxExclusive - minInclusive
            let threshold = ( bigintMaxUInt128Plus1 ) % bound
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % bound + minInclusive else loop ()
            loop ()
        elif minInclusive < BigInteger.Zero then raise ( ArgumentException( "minInclusive cannot be less than 0" ) )
        elif minInclusive = maxExclusive then minInclusive
        else raise ( ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )

    /// Returns a random 128-bit unsigned integer less than the specified maximum.
    member this.Next( maxExclusive : bigint ) =
        if maxExclusive > BigInteger.Zero then
            let threshold = ( bigintMaxUInt128Plus1 ) % maxExclusive
            let rec loop () =
                let r = nextFn ()
                if r >= threshold then r % maxExclusive else loop ()
            loop ()
        elif maxExclusive < BigInteger.Zero then raise ( ArgumentException( "maxExclusive cannot be less than 0" ) )
        else BigInteger.Zero

    /// Returns a random 128-bit unsigned integer.
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
    member this.NextBoolean() = nextFn() > Int128Max

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
        float( nextFn() >>> 75 ) * 0.000000000000000111022302462515654042363166809082031250000000
        // Result with 53 1-bits is: 0.999999999999999888977697537484345957636833190917968750000000
