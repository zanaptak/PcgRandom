# Changelog - Zanaptak.PcgRandom

[![GitHub](https://img.shields.io/badge/-github-gray?logo=github)](https://github.com/zanaptak/PcgRandom) [![NuGet](https://img.shields.io/nuget/v/Zanaptak.PcgRandom?logo=nuget)](https://www.nuget.org/packages/Zanaptak.PcgRandom)

## x.x.x (unreleased)

- Update NextDouble to use 53 random bits instead of 32
- Add NextDouble and NextBoolean to all generators
- Enable source link
- Enable deterministic build

## 0.2.1 (2019-09-29)

- Enable xml doc for tooltips

## 0.2.0 (2019-09-26)

- Add NextBytes overload for array range
- Use crypto RNG if possible for internal seeds

## 0.1.2 (2019-08-28)

- Remove Sample() override

## 0.1.1 (2019-08-28)

- Faster NextDouble() method without modulo operations

## 0.1.0 (2019-08-21)

- Initial release
