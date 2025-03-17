```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5487/22H2/2022Update)
Intel Core i5-9400F CPU 2.90GHz (Coffee Lake), 1 CPU, 6 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method      | Mean     | Error   | StdDev  |
|------------ |---------:|--------:|--------:|
| JsonText    | 386.5 ns | 4.53 ns | 4.24 ns |
| Json        | 742.3 ns | 2.96 ns | 2.62 ns |
| Protobuf    | 299.1 ns | 0.69 ns | 0.61 ns |
| Avro        |       NA |      NA |      NA |
| BSON        | 814.7 ns | 4.16 ns | 3.47 ns |
| MessagePack | 151.9 ns | 0.20 ns | 0.18 ns |

Benchmarks with issues:
  Program.Avro: DefaultJob
