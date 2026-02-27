using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatters;

public class FileFormatter : MessageFormatter
{
    private readonly string _filePath;

    public FileFormatter(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty", nameof(filePath));

        _filePath = filePath;
    }

    public override FormatResult Format(Message message)
    {
        if (message is null)
            return new FormatResult.Failure("Message cannot be empty", message);

        FormatResult markdownMessage = BuildMarkdown(message);
        File.AppendAllText(_filePath, markdownMessage + Environment.NewLine);
        return markdownMessage;
    }
}
