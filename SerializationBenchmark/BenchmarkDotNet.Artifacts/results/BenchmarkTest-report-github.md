```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5487/22H2/2022Update)
Intel Core i5-9400F CPU 2.90GHz (Coffee Lake), 1 CPU, 6 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method    | Mean      | Error     | StdDev    |
|---------- |----------:|----------:|----------:|
| SortArray | 55.980 μs | 0.6288 μs | 0.5574 μs |
| SumArray  |  4.355 μs | 0.0086 μs | 0.0067 μs |
