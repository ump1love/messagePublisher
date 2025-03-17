using System.Buffers;
using System.Text;
using System.Text.Json;

public class MessageHandler
{
	public enum MessageType
	{
		First = 1,
		Second = 2,
	}

	public record Message(
		int Version,
		int Counter,
		int Source,
		MessageType Type,
		DateTimeOffset Time,
		byte[] Data
	);

	public string GetMessage(int counter, string messageBody, int source = 1, MessageType type = MessageType.Second, int dataSize = 1024)
	{
		const int version = default(int);
		DateTimeOffset date = DateTimeOffset.Now;

		byte[] buffer;
		if (!string.IsNullOrEmpty(messageBody))
		{
			byte[] stringBytes = Encoding.UTF8.GetBytes(messageBody);
			int lengthToCopy = Math.Min(stringBytes.Length, dataSize);
			buffer = new byte[lengthToCopy];
			Array.Copy(stringBytes, buffer, lengthToCopy);
		}
		else
		{
			buffer = new byte[dataSize];
		}
		var message = new Message(
			version,
			counter,
			source,
			type,
			date,
			buffer
		);

		return JsonSerializer.Serialize(message, new JsonSerializerOptions { WriteIndented = true });
	}
}
