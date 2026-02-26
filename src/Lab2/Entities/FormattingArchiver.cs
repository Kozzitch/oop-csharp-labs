using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Adapter Pattern: Adapts Formatter to Archiver
public class FormattingArchiver : IMessageArchiver
{
    private readonly IMessageFormatter _formatter;
    private readonly IMessageArchiver _innerArchiver;

    public FormattingArchiver(IMessageFormatter? formatter, IMessageArchiver? innerArchiver)
    {
        if (formatter is null)
            throw new ArgumentException("Formatter cannot be empty", nameof(formatter));

        if (innerArchiver is null)
            throw new ArgumentException("InnerArchiver cannot be empty", nameof(innerArchiver));

        _formatter = formatter;
        _innerArchiver = innerArchiver;
    }

    public ArchiveResult Archive(Message? message)
    {
        if (message is null)
            return new ArchiveResult.Failure();

        FormatResult formattedMessage = _formatter.Format(message);

        // In real scenario, save formattedMessage.
        // Here we delegate to inner archiver for demonstration of chain.
        _innerArchiver.Archive(message);

        return new ArchiveResult.Success();
    }
}
