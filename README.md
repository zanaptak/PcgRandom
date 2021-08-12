# Zanaptak.PcgRandom

[![GitHub](https://img.shields.io/badge/-github-gray?logo=github)](https://github.com/zanaptak/PcgRandom) [![NuGet](https://img.shields.io/nuget/v/Zanaptak.PcgRandom?logo=nuget)](https://www.nuget.org/packages/Zanaptak.PcgRandom)

A PCG pseudorandom number generator implementation for .NET and Fable. PCG is a family of simple fast space-efficient statistically good algorithms for random number generation.

See the [PCG website](https://www.pcg-random.org/) and [paper](https://www.pcg-random.org/paper.html) for more information.

## Basic usage

Add the [NuGet package](https://www.nuget.org/packages/Zanaptak.PcgRandom) to your project:
```
dotnet add package Zanaptak.PcgRandom
```

The main general-purpose generator is `Pcg`, which is compatible with System.Random. It uses an internal PCG 32-bit unsigned integer generator to produce signed integer output.

### C#
```cs
using Zanaptak.PcgRandom;
var pcg = new Pcg();  // or with seed: Pcg(12345)
var randomValue = pcg.Next();
var diceRoll = pcg.Next(1, 7);
var coinFlip = pcg.NextBoolean();
Console.WriteLine($"{randomValue}, {diceRoll}, {coinFlip}");
```

### F#
```fs
open Zanaptak.PcgRandom
let pcg = Pcg()  // or with seed: Pcg(12345)
let randomValue = pcg.Next()
let diceRoll = pcg.Next(1, 7)
let coinFlip = pcg.NextBoolean()
printfn $"{randomValue}, {diceRoll}, {coinFlip}"
```

## Unsigned integer generators

Additional `Pcg*` generators with multiple algorithm variants are available for 8-, 16-, 32-, 64-, and 128-bit unsigned integer output sizes. By default, these use the recommended variants from the [PCG paper](https://www.pcg-random.org/paper.html), with options to specify other variants.

### C#
```cs
using Zanaptak.PcgRandom;
using Pcg128Variants = Zanaptak.PcgRandom.Pcg128Variants;

// generates Byte
var pcg8 = new Pcg8();
var randomByte = pcg8.Next();

// generates unsigned 128-bit integer as BigInteger,
// using variant, seed, and stream parameters
var pcg128 = new Pcg128(Pcg128Variants.Invertible.RXS_M_XS, 12345, 67890);
var randomBigint = pcg128.Next();

Console.WriteLine($"{randomByte}, {randomBigint}");
```

### F#
```fs
open Zanaptak.PcgRandom

// generates Byte
let pcg8 = Pcg8()
let randomByte = pcg8.Next()

// generates unsigned 128-bit integer as BigInteger,
// using variant, seed, and stream parameters
let pcg128 = Pcg128(Pcg128Variants.Invertible.RXS_M_XS, 12345I, 67890I)
let randomBigint = pcg128.Next()

printfn $"{randomByte}, {randomBigint}"
```

## Variants

The different algorithm variants and output functions defined by the PCG library are available via overloaded constructors taking a variant parameter.

The overall categories of variants are:

- `Normal`: General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.
- `Fast`: Faster variant but with reduced statistical quality. Based on MCG generator type.
- `Invertible`: Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.

A `Pcg128` generator is available only as an Invertible variant; there are no Normal or Fast variants (which would require 256 bits of state) defined in the PCG source material. Since there is no Normal variant to serve as a default, there is intentionally no parameterless `Pcg128()` constructor; an Invertible variant must be specified.

Please refer the [PCG website](https://www.pcg-random.org/) and [paper](https://www.pcg-random.org/paper.html) for further details.

## Seed and stream

The generator constructors can optionally take a `seed` parameter, and for some variants, a `stream` parameter. The seed specifies the initial internal state of the generator. The stream specifies the increment, which is part of the internal step calculation that advances the generator to each new state when generating values. Identical generators initialized with the same seed and stream will produce the same output sequence. If not specified, the seed will be randomly generated, and the stream will be set to a predefined default value from the [PCG reference C implementation](https://github.com/imneme/pcg-c). (Note that for the stream value, the high bit is discarded; e.g. for 64-bit seed/stream parameters, there are 2<sup>64</sup> possible seeds and 2<sup>63</sup> possible streams.)

## Benchmarks

See the [benchmark project](https://github.com/zanaptak/PcgRandom/tree/main/benchmark).

## Notes

- This implementation is based on the [PCG reference C implementation](https://github.com/imneme/pcg-c), which is available under the [MIT License](https://github.com/imneme/pcg-c/blob/master/LICENSE-MIT.txt).

- Variants that use 128 bit arithmetic are much slower than other variants because they use [`System.Numerics.BigInteger`](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netstandard-2.0) instead of primitive integer types. These can be identified by their constructors taking `BigInteger` seed values; they include `Pcg64Variants.Normal`, `Pcg64Variants.Fast`, and `Pcg128Variants.Invertible`.

- These generators use internal mutable state and do not implement any thread safety mechanisms.
