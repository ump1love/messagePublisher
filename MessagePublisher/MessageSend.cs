using RabbitMQ.Client;
using Spectre.Console;
using System.Diagnostics;
using System.Text;

public class MessageSend
{
	private int COUNTER = 1;
	private MessageHandler messageHandler = new MessageHandler();
	private ConsoleOutput consoleOutput = new ConsoleOutput();

	private T GetValueFromDictionary<T>(Dictionary<string, string> options, string key, T defaultValue = default)
	{
		if (options.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
		return defaultValue;
	}

	public async Task MessageBroadcast(Dictionary<string, string> options, string messageBody)
	{
		try
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();
			string exchangeName = GetValueFromDictionary(options, "exchange", "broadcast");

			if (string.IsNullOrEmpty(exchangeName) || exchangeName == "default")
			{
				throw new ArgumentException("Invalid exchange name. Please provide a valid custom exchange name.");
			}

			await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout);

			// amount of needed rebroadcasts
			int count = GetValueFromDictionary(options, "count", 1);

			// needed variables to get a Message
			int source = GetValueFromDictionary(options, "source", 1);
			int dataSize = GetValueFromDictionary(options, "data", 1024);
			string messageTypeString = GetValueFromDictionary(options, "type", "Second");

			// try to parse the MessageType
			if (!Enum.TryParse<MessageHandler.MessageType>(messageTypeString, true, out var messageType))
			{
				consoleOutput.HelpOption();
				throw new ArgumentException($"Invalid message type: {messageTypeString}");
			}

			// speed of the message sending
			uint speed = GetValueFromDictionary(options, "speed", uint.MaxValue);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			var progress = new Progress<int>(percent =>
			{
				AnsiConsole.MarkupLine($"[green]Progress:[/] {percent}%");
			});

			await AnsiConsole.Progress()
				.StartAsync(async ctx =>
				{
					var task = ctx.AddTask("[green]Sending messages[/]", maxValue: count);

					for (int counterLoop = 0; counterLoop < count; counterLoop++)
					{
						var message = messageHandler.GetMessage(COUNTER, messageBody, source, messageType, dataSize);
						var body = Encoding.UTF8.GetBytes(message);

						var messageStopwatch = Stopwatch.StartNew();
						await channel.BasicPublishAsync(exchange: exchangeName, routingKey: string.Empty, body: body);
						messageStopwatch.Stop();

						COUNTER++;

						task.Increment(1);

						double elapsedMilliseconds = messageStopwatch.Elapsed.TotalMilliseconds;
						double messagesPerSecond = 1000 / elapsedMilliseconds;
						AnsiConsole.MarkupLine($"[yellow]Current Speed:[/] {messagesPerSecond:F2} messages/second");

						// if speed is not set to default - add a delay
						if (speed != uint.MaxValue)
						{
							await Task.Delay((int)speed);
						}
					}
				});

			stopwatch.Stop();

			AnsiConsole.MarkupLine($"[green]Send {COUNTER - 1} message(s)[/]\nPress \"ENTER\" to exit.");
			Console.ReadLine();
		}
		catch (Exception ex)
		{
			AnsiConsole.MarkupLine($"[red]An error occurred:[/] {ex.Message}");
			AnsiConsole.WriteException(ex);
		}
	}
}
