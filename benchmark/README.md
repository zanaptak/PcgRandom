# Benchmarks

## Environment

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT DEBUG
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
```

## Calling method x.Next()

|           Method |     Mean |     Error |    StdDev |
|----------------- |---------:|----------:|----------:|
|     SystemRandom | 6.762 ns | 0.0210 ns | 0.0197 ns |
|              Pcg | 5.668 ns | 0.0323 ns | 0.0287 ns |
|            Pcg32 | 3.522 ns | 0.0917 ns | 0.0901 ns |
|       Pcg32_Fast | 3.307 ns | 0.0065 ns | 0.0061 ns |
| Pcg32_Invertible | 2.928 ns | 0.0065 ns | 0.0057 ns |
| Pcg64_Invertible | 3.190 ns | 0.0085 ns | 0.0080 ns |

## Calling method x.Next(100)

|           Method |      Mean |     Error |    StdDev |
|----------------- |----------:|----------:|----------:|
|     SystemRandom |  9.887 ns | 0.0186 ns | 0.0165 ns |
|              Pcg | 11.229 ns | 0.0094 ns | 0.0083 ns |
|            Pcg32 | 10.154 ns | 0.0173 ns | 0.0145 ns |
|       Pcg32_Fast |  9.991 ns | 0.0130 ns | 0.0101 ns |
| Pcg32_Invertible |  9.707 ns | 0.0141 ns | 0.0118 ns |
| Pcg64_Invertible | 17.473 ns | 0.3754 ns | 0.7583 ns |

## Calling method x.Next(0,100)

|           Method |     Mean |     Error |    StdDev |
|----------------- |---------:|----------:|----------:|
|     SystemRandom | 10.07 ns | 0.0957 ns | 0.0848 ns |
|              Pcg | 11.70 ns | 0.0084 ns | 0.0075 ns |
|            Pcg32 | 10.63 ns | 0.0242 ns | 0.0202 ns |
|       Pcg32_Fast | 10.70 ns | 0.0961 ns | 0.0852 ns |
| Pcg32_Invertible | 10.40 ns | 0.1108 ns | 0.0925 ns |
| Pcg64_Invertible | 17.43 ns | 0.1027 ns | 0.0960 ns |

## Calling method x.NextBytes() with 99 byte array

|           Method |     Mean |     Error |    StdDev |
|----------------- |---------:|----------:|----------:|
|     SystemRandom | 588.1 ns | 0.7290 ns | 0.6462 ns |
|              Pcg | 309.3 ns | 0.5091 ns | 0.4762 ns |
|            Pcg32 | 324.6 ns | 0.3245 ns | 0.2877 ns |
|       Pcg32_Fast | 318.3 ns | 0.4513 ns | 0.4222 ns |
| Pcg32_Invertible | 312.2 ns | 0.4364 ns | 0.3868 ns |
| Pcg64_Invertible | 224.1 ns | 0.2977 ns | 0.2639 ns |

## Calling method x.Next() -- variants using 128 bit operations

|            Method |     Mean |    Error |   StdDev |
|------------------ |---------:|---------:|---------:|
|             Pcg64 | 719.6 ns | 2.029 ns | 1.898 ns |
|        Pcg64_Fast | 653.3 ns | 2.021 ns | 1.791 ns |
| Pcg128_Invertible | 908.0 ns | 1.516 ns | 1.418 ns |
