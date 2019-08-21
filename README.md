# PcgRandom

A [PCG](http://www.pcg-random.org/) pseudorandom number generator implementation for .NET and Fable. [PCG](http://www.pcg-random.org/) is a family of simple fast space-efficient statistically good algorithms for random number generation.

## Basic usage

Add the [NuGet package](https://www.nuget.org/packages/Zanaptak.PcgRandom) to your project:
```
dotnet add package Zanaptak.PcgRandom
```

The basic option is `Pcg()`, which derives from System.Random and wraps an internal 32-bit unsigned PCG generator to produce signed integer output.

### C#
```cs
using Zanaptak.PcgRandom;
var pcg = new Pcg();  // or with seed: Pcg(12345)
var randomValue = pcg.Next();
```

### F#
```fs
open Zanaptak.PcgRandom
let pcg = Pcg()  // or with seed: Pcg(12345)
let randomValue = pcg.Next()
```

## Output sizes

Additional classes are available for specific PCG generators at different unsigned integer output sizes. Without parameters, these use the default algorithms recommended by the [PCG paper](http://www.pcg-random.org/paper.html).

### C#
```cs
using Zanaptak.PcgRandom;

// generates Byte
var pcg8 = new Pcg8();

// generates UInt16
var pcg16 = new Pcg16();

// generates UInt32
var pcg32 = new Pcg32();

// generates UInt64
var pcg64 = new Pcg64();
```

### F#
```fs
open Zanaptak.PcgRandom

// generates Byte
let pcg8 = Pcg8()

// generates UInt16
let pcg16 = Pcg16()

// generates UInt32
let pcg32 = Pcg32()

// generates UInt64
let pcg64 = Pcg64()
```

## Variants

The low-level variants defined by the PCG library are available via overloaded constructors taking a variant parameter.

The overall categories of variants are:

`Normal`: General purpose variant balancing speed and statistical quality. Allows stream selection. Based on LCG generator type.

`Fast`: Faster variant but with reduced statistical quality. Based on MCG generator type.

`Invertible`: Variant with the same number of outbut bits as internal state bits. Easier to reverse-engineer internal state than other variants.

A `Pcg128` generator is available only as an Invertible variant; there are no Normal or Fast variants (which would require 256 bits of state) defined in the PCG source material.

Please refer the [PCG website](http://www.pcg-random.org/) and [paper](http://www.pcg-random.org/paper.html) for further details.

### C#
```cs
using Zanaptak.PcgRandom;
using Pcg128Variants = Zanaptak.PcgRandom.Pcg128Variants;

var pcg128 = new Pcg128(Pcg128Variants.Invertible.XSL_RR_RR);
```

### F#
```fs
open Zanaptak.PcgRandom

let pcg128 = Pcg128(Pcg128Variants.Invertible.XSL_RR_RR)
```

## Benchmarks

See the [benchmark project](https://github.com/zanaptak/PcgRandom/tree/master/benchmark).

## Notes

* Internal calculations are based on the [PCG reference C implementation](https://github.com/imneme/pcg-c), which is available under the [MIT License](https://github.com/imneme/pcg-c/blob/master/LICENSE-MIT.txt).

* The specific output size versions (`Pcg8`, `Pcg16`, etc.) intentionally exclude the `NextDouble()` method; proper unbiased double generation is not a goal of this library. The basic `Pcg` class does provide a trivial implementation (random UInt32 divided by max UInt32) to satisfy the System.Random interface.

* These generators use internal mutable state and do not implement any thread safety mechanisms.
