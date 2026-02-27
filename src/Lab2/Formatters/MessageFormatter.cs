using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatters;

public abstract class MessageFormatter
{
    public abstract FormatResult Format(Message message);

    protected static FormatResult BuildMarkdown(Message message)
    {
        if (message is null)
            return new FormatResult.Failure("Message cannot be empty", message);

        string markdownMessage = $"# {message.Header}\n\n{message.Body}\n\n*Importance: {message.Importance}*";
        return new FormatResult.Success(markdownMessage, message);
    }
}