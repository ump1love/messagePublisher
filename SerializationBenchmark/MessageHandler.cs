public class MessageHandler
{
	public Message GetMessage()
	{
		return new Message
		{
			Version = 1,
			Counter = 2,
			Source = 3,
			Type = MessageType.First,
			Data = new byte[] { 1, 2, 3 },
			Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() // ✅ Ensure Avro gets a valid timestamp
		};
	}
}