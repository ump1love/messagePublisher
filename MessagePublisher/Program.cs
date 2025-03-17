using RabbitMQ.Client;

public class Program
{
    private static ArgsHandler argsHandler = new ArgsHandler();
    private static ConsoleOutput consoleOutput= new ConsoleOutput();
    private static MessageSend messageSend= new MessageSend();
    

    public static async Task Main(string[] args)
    {
		var (options, messageBody) = argsHandler.SliceArgs(args);

        consoleOutput.OptionsCheck(options);

		await messageSend.MessageBroadcast(options, messageBody);
        
    }

}