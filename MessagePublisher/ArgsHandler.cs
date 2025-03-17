public class ArgsHandler
{  
    // HashMap of all allowed options
    private readonly Dictionary<string, string> optionKeys = new()
    {
        { "--source", "source" }, { "-s", "source" },
        { "--type", "type" }, { "-t", "type" },
        { "--data", "data" }, { "-d", "data" },
        { "--frequency", "frequency" }, { "-f", "frequency" },
        { "--exchange", "exchange" }, { "-e", "exchange" },
        { "--count", "count" }, { "-c", "count" },
        { "--help", "help" }, { "-h", "help" },
        { "--version", "version" }
    };

    public (Dictionary<string, string>, string) SliceArgs(string[] args)
    {
        var options = new Dictionary<string, string>();

        var remainingArgs = new List<string>();
        
        // if user did not enter anything - brings help option
        if (args.Length == 0)
        {
            options["help"] = string.Empty;
            return(options, string.Empty);
        }

        int i = 0;
        while (i < args.Length)
        {
            // if input is a correct one using optionKeys HashMap, if so - puts it into options HashMap
            // else if a user has entered a wrong option, and it was an intention from the user to enter any option(checks using \-\ and \--\ symbols), the help option will be triggered with the user's typo
            // else, the program assumes everything else is a message that the user wanted to enter
            if (optionKeys.TryGetValue(args[i].ToLower(), out var key))
            {
                if (key == "help" || key == "version")
                {
                    options[key] = string.Empty;
                }
                else if (i + 1 < args.Length)
                {
                    options[key] = args[i + 1];
                    i++;
                }
            }
			else if (args[i].StartsWith("-") || args[i].StartsWith("--"))
			{
                options["help"] = args[i];
            }
            else
            {
                remainingArgs.Add(args[i]);
            }

            i++;
        }
        string messageBody = string.Join(" ", remainingArgs);

        return (options, messageBody);
    }
}