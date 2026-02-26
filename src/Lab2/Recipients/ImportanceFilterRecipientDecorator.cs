using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class ImportanceFilterRecipientDecorator : RecipientDecorator
{
    private readonly ImportanceLevel _minimumImportance;

    public ImportanceFilterRecipientDecorator(IMessageRecipient? decoratee, ImportanceLevel minimumImportance)
        : base(decoratee)
    {
        _minimumImportance = minimumImportance;
    }

    public override ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure();

        // Guard clause logic
        if (message.Importance < _minimumImportance)
            return new ReceiveResult.Success();

        MessageRecipient.Receive(message);

        return new ReceiveResult.Success();
    }
}