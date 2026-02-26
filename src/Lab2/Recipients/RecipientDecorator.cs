using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public abstract class RecipientDecorator : IMessageRecipient
{
    // protected readonly IMessageRecipient _messageRecipient;
    protected IMessageRecipient MessageRecipient { get; }

    protected RecipientDecorator(IMessageRecipient? decoratee)
    {
        if (decoratee is null)
            throw new ArgumentException("Decoratee cannot be empty", nameof(decoratee));

        MessageRecipient = decoratee;
    }

    public abstract ReceiveResult Receive(Message? message);
}
