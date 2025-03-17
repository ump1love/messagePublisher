using Spectre.Console;

public class ConsoleOutput
{
	// checks if every user inputted options have the correct type
	private void CorrectTypesCheck(Dictionary<string, string> options)
	{
		bool result = true;
		foreach (var kvp in options)
		{
			// because almost all possible and allowed options are int, exceptions are type and exchange
			if (kvp.Key != "type" && kvp.Key != "exchange")
			{
				result = IntOptionCheck(options, kvp.Key, out _);
			}
			else
			{
				result = StringOptionCheck(options, kvp.Key, out _);
			}

			if (!result)
			{
				break;
			}
		}

		if (!result)
		{
			Console.WriteLine("Last if statement CorrectTypesCheck");
			HelpOption();
		}
	}

	private bool IntOptionCheck(Dictionary<string, string> options, string key, out int result)
	{
		result = 0;

		if (options.TryGetValue(key, out var value) && int.TryParse(value, out result))
		{
			return true;
		}
		AnsiConsole.MarkupLine($"[red]{key} option has to be an int[/]");
		return false;
	}

	private bool StringOptionCheck(Dictionary<string, string> options, string key, out string result)
	{
		result = string.Empty;

		if (options.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value))
		{
			result = value;
			return true;
		}
		AnsiConsole.MarkupLine($"[red]{key} option has to be a valid non-empty string[/]");
		return false;
	}

	public void HelpOption()
	{
		string line;
		try
		{
			StreamReader streamReader = new StreamReader("Assets/helpOutput.txt");
			line = streamReader.ReadLine();
			while (line != null)
			{
				Console.WriteLine(line);
				line = streamReader.ReadLine();
			}
			streamReader.Close();
			Console.WriteLine("");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	// checks for --help and --version options, also if there is an incorrect input from the user
	public void OptionsCheck(Dictionary<string, string> options)
	{
		if (options.ContainsKey("help") && string.IsNullOrEmpty(options["help"]))
		{
			Console.WriteLine("First if statement OptionsCheck");
			HelpOption();
			Environment.Exit(0);
		}
		else if (options.ContainsKey("help") && !string.IsNullOrEmpty(options["help"]))
		{
			AnsiConsole.MarkupLine($"[red]There is no such option as: {options["help"]}[/]");
			HelpOption();
			Environment.Exit(0);
		}
		else if (options.ContainsKey("version"))
		{
			const int version = default(int);

			AnsiConsole.MarkupLine($"[green]Current version is: {version}[/]");

			Environment.Exit(0);
		}

		CorrectTypesCheck(options);
	}
}
