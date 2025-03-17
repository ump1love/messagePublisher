using Avro.IO;
using Avro.Reflect;
using Avro;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MessagePack;
using MongoDB.Bson;
using ProtoBuf;
using System.Text.Json;

public class Program
{
	private MessageHandler _message = new MessageHandler();
	private readonly Schema _avroSchema;

	public Program()
	{
		_avroSchema = Schema.Parse(@"
        {
            ""type"": ""record"",
            ""name"": ""Message"",
            ""fields"": [
                { ""name"": ""Version"", ""type"": ""int"" },
                { ""name"": ""Counter"", ""type"": ""int"" },
                { ""name"": ""Source"", ""type"": ""int"" },
                { ""name"": ""Type"", ""type"": ""int"" },
                { ""name"": ""Time"", ""type"": ""long"", ""logicalType"": ""timestamp-millis"" },
                { ""name"": ""Data"", ""type"": ""bytes"" }
            ]
        }");
	}

	[Benchmark]
	public void JsonText()
		=> JsonSerializer.Serialize(_message.GetMessage());

	[Benchmark]
	public void Json()
		=> Newtonsoft.Json.JsonConvert.SerializeObject(_message.GetMessage());

	[Benchmark]
	public void Protobuf()
	{
		using var protobufMs = new MemoryStream();
		Serializer.Serialize(protobufMs, _message.GetMessage());
	}

	[Benchmark]
	public void Avro()
	{
		using var avroMs = new MemoryStream();
		var writer = new ReflectWriter<Message>(_avroSchema);
		writer.Write(_message.GetMessage(), new BinaryEncoder(avroMs));
	}

	[Benchmark]
	public void BSON()
		=> _message.GetMessage().ToBson();

	[Benchmark]
	public void MessagePack()
		=> MessagePackSerializer.Serialize(_message.GetMessage());

	public static void Main(string[] args)
	{
		var summary = BenchmarkRunner.Run<Program>();
	}
}