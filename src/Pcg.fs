namespace Zanaptak.PcgRandom

open Utils
open System

/// PCG-backed pseudorandom number generator compatible with System.Random.
type Pcg( seed : int ) =
  #if ! FABLE_COMPILER
  inherit Random()
  #endif
  let pcg32 = Pcg32( uint64 seed )

  let [< Literal >] int32Bound = 2147483647u
  let [< Literal >] int32Threshold = 2u
  let rec nextInt32 () =
    let r = pcg32.Next()
    if r >= int32Threshold then r % int32Bound |> int else nextInt32 ()

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

  let nextDouble () =
    let rec loop () =
      let r = pcg32.Next()
      if r < UInt32.MaxValue then float r / float UInt32.MaxValue
      else loop ()
    loop ()

  let nextBytes ( bytes : byte array ) startIndex length =
    let endIndex = startIndex + length - 1
    for i in startIndex .. 4 .. endIndex do
      let randVal = pcg32.Next()
      for j in 0 .. min 3 ( endIndex - i ) do
        bytes.[ i + j ] <- randVal >>> ( j * 8 ) |> byte

  /// PCG-backed pseudorandom number generator compatible with System.Random.
  new() = Pcg( seed32 () |> int )

  #if FABLE_COMPILER
  /// Returns a random 32-bit signed integer greater than or equal to 0 and less than Int32.MaxValue.
  member this.Next() = nextInt32 ()
  /// Returns a random 32-bit signed integer less than the specified maximum.
  member this.Next( maxExclusive : int ) = nextBoundedInt32 maxExclusive
  /// Returns a random 32-bit signed integer greater than or equal to the specified minimum and less than the specified maximum.
  member this.Next( minInclusive : int , maxExclusive : int ) = nextRangeInt32 minInclusive maxExclusive
  /// Returns a random double greater than or equal to 0.0 and less than 1.0.
  member this.NextDouble() = nextDouble ()
  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary.
  member this.NextBytes( bytes : byte array ) =
    if ( not ( isNull bytes ) ) && bytes.Length > 0 then
      nextBytes bytes 0 bytes.Length
    elif isNull bytes then
      raise ( ArgumentException( "byte array cannot be null" ) )
    else ()
  #else
  /// Returns a random 32-bit signed integer greater than or equal to 0 and less than Int32.MaxValue.
  override this.Next() = nextInt32 ()
  /// Returns a random 32-bit signed integer less than the specified maximum.
  override this.Next( maxExclusive : int ) = nextBoundedInt32 maxExclusive
  /// Returns a random 32-bit signed integer greater than or equal to the specified minimum and less than the specified maximum.
  override this.Next( minInclusive : int , maxExclusive : int ) = nextRangeInt32 minInclusive maxExclusive
  /// Returns a random double greater than or equal to 0.0 and less than 1.0.
  override this.NextDouble() = nextDouble ()
  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary.
  override this.NextBytes( bytes : byte array ) =
    if ( not ( isNull bytes ) ) && bytes.Length > 0 then
      nextBytes bytes 0 bytes.Length
    elif isNull bytes then
      raise ( ArgumentException( "byte array cannot be null" ) )
    else ()
  #endif

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

