# Serialization Benchmark

### 1. Tested Serialization Technologies:
- **MessagePack**
- **Protobuf**
- **JSON.NET**
- **JsonText**
- **BSON**
- **Avro**

### 2. Benchmark Results:

| Method      | Mean     | Error   | StdDev  |
|------------ |---------:|--------:|--------:|
| JsonText    | 386.5 ns | 4.53 ns | 4.24 ns |
| Json        | 742.3 ns | 2.96 ns | 2.62 ns |
| Protobuf    | 299.1 ns | 0.69 ns | 0.61 ns |
| Avro        |       NA |      NA |      NA |
| BSON        | 814.7 ns | 4.16 ns | 3.47 ns |
| MessagePack | 151.9 ns | 0.20 ns | 0.18 ns |

MessagePack is the fastest serialization technology in this benchmark, performing approximately 2x faster than Protobuf, 2.5x faster than JsonText, 4.9x faster than JSON.NET, and 5.4x faster than BSON.
However, MessagePack requires modifications to the original Message record to work correctly, which is a limitation.
JsonText, Json, and BSON were the only technologies that worked without changes. Among them, JsonText was the fastest.
While JsonText is slower than MessagePack, it remains the best choice for this case due to its human readability, compatibility with the existing task, and solid performance.
Avro failed to serialize the Message structure, even with modifications, making it unsuitable for this scenario.

### 3. Notes:

- Initial task:

```csharp
public record Message(int Version, int Counter, int Source, MessageType Type, DateTimeOffset Time, byte[] Data);

public enum MessageType
{
    First,
    Second
}
```

- Avro serialization could not work even with big changes to the initial task, which is already not acceptable.