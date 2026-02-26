using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class Topic
{
    private readonly string _name;
    private readonly List<IMessageRecipient> _recipients;

    public Topic(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Topic name cannot be empty", nameof(name));

        _name = name;
        _recipients = new List<IMessageRecipient>();
    }

    public ArchiveResult AddRecipient(IMessageRecipient? recipient)
    {
        if (recipient is null)
            return new ArchiveResult.Failure();

        _recipients.Add(recipient);
        return new ArchiveResult.Success();
    }

    public ReceiveResult Send(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure();

        foreach (IMessageRecipient recipient in _recipients)
        {
            recipient.Receive(message);
        }

        return new ReceiveResult.Success();
    }

    public override string ToString() => $"Topic: {_name}";
}