using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class GroupRecipient : IMessageRecipient
{
    private readonly List<IMessageRecipient> _children;

    public GroupRecipient()
    {
        _children = new List<IMessageRecipient>();
    }

    public AddResult Add(IMessageRecipient? recipient)
    {
        if (recipient is null)
            return new AddResult.Failure();

        _children.Add(recipient);

        return new AddResult.Success();
    }

    public ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure("Message cannot be empty", message);

        foreach (IMessageRecipient child in _children)
        {
            child.Receive(message);
        }

        return new ReceiveResult.Success();
    }
}