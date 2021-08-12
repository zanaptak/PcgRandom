namespace Zanaptak.PcgRandom

open Utils
open System

/// PCG-backed pseudorandom number generator compatible with System.Random.
type Pcg
    // Private primary constructor to hide uint64 ctor.
    private ( seed : uint64 ) =

    #if ! FABLE_COMPILER
    inherit Random()
    #endif
    let pcg32 = Pcg32( seed )

    let [< Literal >] Int32Max = 2147483647u
    let [< Literal >] Int32Threshold = 2u

    let rec nextInt32 () =
        let r = pcg32.Next()
        if r >= Int32Threshold then r % Int32Max |> int else nextInt32 ()

    let nextBoundedInt32 ( maxExclusive : int ) =
        if maxExclusive >= 0 then
            pcg32.Next( uint32 maxExclusive ) |> int
        else
            raise ( ArgumentException( "maxExclusive cannot be less than 0" ) )

    let nextRangeInt32 ( minInclusive : int ) ( maxExclusive : int ) =
        if maxExclusive > minInclusive then
            // Don't use Pcg32(a,b) directly since we are allowing for negative bounds.
            let bound = uint32 ( maxExclusive - minInclusive ) // math works due to overflow
            minInclusive + ( pcg32.Next( bound ) |> int )
        elif minInclusive = maxExclusive then minInclusive
        else raise ( ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )

    let nextBytes ( bytes : byte array ) startIndex length =
        let endIndex = startIndex + length - 1
        for i in startIndex .. 4 .. endIndex do
            let randVal = pcg32.Next()
            for j in 0 .. min 3 ( endIndex - i ) do
                bytes.[ i + j ] <- randVal >>> ( j * 8 ) |> byte

    /// Create Pcg instance with specified seed.
    new ( seed : int ) = Pcg( uint64 seed )
    /// Create Pcg instance with random seed.
    new() = Pcg( seedRng.NextUInt64() )

    // http://prng.di.unimi.it/
    // A standard double (64-bit) floating-point number in IEEE floating point format has 52 bits of significand, plus an implicit bit at the left of the significand.
    // Thus, the representation can actually store numbers with 53 significant binary digits.
    // Because of this fact, in C99 a 64-bit unsigned integer x should be converted to a 64-bit double using the expression
    //    #include <stdint.h>
    //    (x >> 11) * (1. / (UINT64_C(1) << 53))
    // This conversion guarantees that all dyadic rationals of the form k / 2−53 will be equally likely.
    // Note that this conversion prefers the high bits of x (usually, a good idea), but you can alternatively use the lowest bits.

    /// Returns a random double greater than or equal to 0.0 and less than 1.0 (using 53 random bits). Consumes 2 values from the generator.
    #if FABLE_COMPILER
    member
    #else
    override
    #endif
        this.NextDouble() =
            let next64bits = uint64( pcg32.Next() ) ||| ( uint64( pcg32.Next() ) <<< 32 )
            float( next64bits >>> 11 ) * 0.000000000000000111022302462515654042363166809082031250000000
            // Result with 53 1-bits is: 0.999999999999999888977697537484345957636833190917968750000000

    /// Returns a random 32-bit signed integer greater than or equal to 0 and less than Int32.MaxValue.
    #if FABLE_COMPILER
    member
    #else
    override
    #endif
        this.Next() = nextInt32 ()

    /// Returns a random 32-bit signed integer less than the specified maximum.
    #if FABLE_COMPILER
    member
    #else
    override
    #endif
        this.Next( maxExclusive : int ) = nextBoundedInt32 maxExclusive

    /// Returns a random 32-bit signed integer greater than or equal to the specified minimum and less than the specified maximum.
    #if FABLE_COMPILER
    member
    #else
    override
    #endif
        this.Next( minInclusive : int , maxExclusive : int ) = nextRangeInt32 minInclusive maxExclusive

    /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary.
    #if FABLE_COMPILER
    member
    #else
    override
    #endif
        this.NextBytes( bytes : byte array ) =
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
    member this.NextBoolean() = pcg32.Next() > Int32Max
