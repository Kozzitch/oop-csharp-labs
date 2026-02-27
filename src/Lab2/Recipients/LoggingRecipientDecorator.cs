using Itmo.ObjectOrientedProgramming.Lab2.Loggers;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class LoggingRecipientDecorator : RecipientDecorator
{
    private readonly ILogger _logger;

    public LoggingRecipientDecorator(IMessageRecipient decoratee, ILogger? logger)
        : base(decoratee)
    {
        if (logger is null)
            throw new ArgumentException("Logger cannot be empty", nameof(logger));

        _logger = logger;
    }

    public override ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure("Message cannot be empty", message);

        _logger.Log($"Message received: {message.Id}");

        MessageRecipient.Receive(message);

        return new ReceiveResult.Success();
    }
}