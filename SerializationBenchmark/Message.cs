using MessagePack;
using ProtoBuf;

[ProtoContract]
[MessagePackObject]
public class Message
{
	[ProtoMember(1)]
	[Key(0)]
	public int Version { get; set; }

	[ProtoMember(2)]
	[Key(1)]
	public int Counter { get; set; }

	[ProtoMember(3)]
	[Key(2)]
	public int Source { get; set; }

	[ProtoMember(4)]
	[Key(3)]
	public MessageType Type { get; set; }

	[ProtoMember(5)]
	[Key(4)]
	public byte[] Data { get; set; }

	[ProtoMember(6)]
	[Key(5)]
	public long Time { get; set; }
}

public enum MessageType
{
	First,
	Second
}