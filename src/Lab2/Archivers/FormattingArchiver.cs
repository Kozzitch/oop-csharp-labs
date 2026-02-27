using Itmo.ObjectOrientedProgramming.Lab2.Formatters;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archivers;

public class FormattingArchiver : IMessageArchiver
{
    private readonly MessageFormatter _formatter;
    private readonly IMessageArchiver _innerArchiver;

    public FormattingArchiver(MessageFormatter? formatter, IMessageArchiver? innerArchiver)
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

        if (formattedMessage is null)
            return new ArchiveResult.Failure();

        if (formattedMessage.FormatingString is null)
            return new ArchiveResult.Failure();

        _innerArchiver.Archive(message);

        // Или:
        // _innerArchiver.Archive(new Message(message.Id, message.Header, formattedMessage.FormatingString, message.Importance));
        return new ArchiveResult.Success();
    }
}
