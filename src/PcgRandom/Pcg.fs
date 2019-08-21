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

  let nextBoundedInt32 ( maxExc : int ) =
    if maxExc < 0 then raise ( ArgumentException( "maxExclusive cannot be less than 0" ) )
    else pcg32.Next( uint32 maxExc ) |> int

  let nextRangeInt32 ( minInc : int ) ( maxExc : int ) =
    if minInc > maxExc then raise ( ArgumentException( "minInclusive cannot be greater than maxExclusive" ) )
    elif minInc = maxExc then minInc
    else
      // Don't use Pcg32(a,b) directly since we are allowing for negative bounds.
      let bound = uint32 ( maxExc - minInc ) // math works due to overflow
      minInc + ( pcg32.Next( bound ) |> int )

  let nextDouble () = ( pcg32.Next( UInt32.MaxValue ) |> float ) / float UInt32.MaxValue
  let nextBytesCount , nextBytesFn = 4 , fun () -> BitConverter.GetBytes( pcg32.Next() )

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
  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary to fill array.
  member this.NextBytes( bytes ) = nextBytes nextBytesFn nextBytesCount bytes
  #else
  /// Returns a random 32-bit signed integer greater than or equal to 0 and less than Int32.MaxValue.
  override this.Next() = nextInt32 ()
  /// Returns a random 32-bit signed integer less than the specified maximum.
  override this.Next( maxExclusive : int ) = nextBoundedInt32 maxExclusive
  /// Returns a random 32-bit signed integer greater than or equal to the specified minimum and less than the specified maximum.
  override this.Next( minInclusive : int , maxExclusive : int ) = nextRangeInt32 minInclusive maxExclusive
  override this.Sample() = nextDouble ()
  /// Returns a random double greater than or equal to 0.0 and less than 1.0.
  override this.NextDouble() = nextDouble ()
  /// Sets all bytes in the specified array to random bytes. Consumes multiple values from the generator if necessary to fill array.
  override this.NextBytes( bytes ) = nextBytes nextBytesFn nextBytesCount bytes
  #endif
