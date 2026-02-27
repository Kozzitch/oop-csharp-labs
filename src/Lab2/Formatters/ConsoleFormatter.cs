using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatters;

public class ConsoleFormatter : MessageFormatter
{
    public override FormatResult Format(Message message)
    {
        if (message is null)
            return new FormatResult.Failure("Message cannot be empty", message);

        FormatResult markdownMessage = BuildMarkdown(message);
        Console.WriteLine(markdownMessage.FormatingString);
        return markdownMessage;
    }
}