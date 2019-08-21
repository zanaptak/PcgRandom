# Benchmarks

Below is a benchmark run with several Pcg variants and System.Random for comparison.

Note that Pcg64 and Pcg128 are slow due to 128-bit arithmetic on the internal state (using System.Numerics.BigInteger).

The Fast variant at 32-bits seems it may not have enough difference in the algorithm to overcome the implementation overhead and/or environmental noise.

## Environment

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT DEBUG
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

## Results for calling method x.Next()

|            Method |         Mean |     Error |    StdDev |
|------------------ |-------------:|----------:|----------:|
|      SystemRandom |     7.185 ns | 0.0398 ns | 0.0373 ns |
|               Pcg |     6.736 ns | 0.0463 ns | 0.0411 ns |
|             Pcg32 |     3.336 ns | 0.0467 ns | 0.0437 ns |
|        Pcg32_Fast |     3.458 ns | 0.0270 ns | 0.0211 ns |
|  Pcg32_Invertible |     3.098 ns | 0.0278 ns | 0.0247 ns |
|             Pcg64 |   834.122 ns | 6.0910 ns | 5.6975 ns |
|        Pcg64_Fast |   770.894 ns | 6.6217 ns | 6.1939 ns |
|  Pcg64_Invertible |     3.156 ns | 0.0190 ns | 0.0169 ns |
| Pcg128_Invertible | 1,057.431 ns | 7.0344 ns | 6.2358 ns |

## Results for calling method x.Next(100)

|            Method |         Mean |      Error |     StdDev |       Median |
|------------------ |-------------:|-----------:|-----------:|-------------:|
|      SystemRandom |     9.668 ns |  0.0529 ns |  0.0495 ns |     9.671 ns |
|               Pcg |    12.184 ns |  0.1093 ns |  0.1022 ns |    12.173 ns |
|             Pcg32 |    11.304 ns |  0.2517 ns |  0.5578 ns |    11.013 ns |
|        Pcg32_Fast |    10.907 ns |  0.1017 ns |  0.0951 ns |    10.872 ns |
|  Pcg32_Invertible |     9.845 ns |  0.0761 ns |  0.0712 ns |     9.841 ns |
|             Pcg64 |   848.677 ns |  7.7337 ns |  7.2341 ns |   849.135 ns |
|        Pcg64_Fast |   787.962 ns |  4.8762 ns |  4.3226 ns |   787.300 ns |
|  Pcg64_Invertible |    18.877 ns |  0.0963 ns |  0.0901 ns |    18.873 ns |
| Pcg128_Invertible | 1,653.513 ns | 15.4690 ns | 14.4697 ns | 1,647.583 ns |

## Results for calling method x.Next(0,100)

|            Method |        Mean |      Error |     StdDev |
|------------------ |------------:|-----------:|-----------:|
|      SystemRandom |    10.03 ns |  0.0732 ns |  0.0685 ns |
|               Pcg |    12.26 ns |  0.0634 ns |  0.0529 ns |
|             Pcg32 |    11.32 ns |  0.1093 ns |  0.1023 ns |
|        Pcg32_Fast |    11.46 ns |  0.1044 ns |  0.0976 ns |
|  Pcg32_Invertible |    10.61 ns |  0.0532 ns |  0.0471 ns |
|             Pcg64 |   853.73 ns |  5.2473 ns |  4.3818 ns |
|        Pcg64_Fast |   792.05 ns |  8.4213 ns |  7.8773 ns |
|  Pcg64_Invertible |    17.91 ns |  0.0794 ns |  0.0620 ns |
| Pcg128_Invertible | 1,641.20 ns | 13.8302 ns | 12.9367 ns |
