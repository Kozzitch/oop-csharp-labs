using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Topics;

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

    public IReadOnlyCollection<ReceiveResult> Send(Message? message)
    {
        var results = new List<ReceiveResult>();
        if (message is null)
        {
            results.Add(new ReceiveResult.Failure("Message cannot be empty", message));
            return results;
        }

        foreach (IMessageRecipient recipient in _recipients)
        {
            results.Add(recipient.Receive(message));
        }

        return results;
    }

    public override string ToString() => $"Topic: {_name}";
}