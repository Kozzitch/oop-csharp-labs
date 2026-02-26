using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class User
{
    private readonly Dictionary<Guid, MessageStatus> _messageStatuses;

    public User(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name cannot be empty", nameof(name));

        Id = id;
        Name = name;

        // Field initialization in constructor per codestyle.md
        _messageStatuses = new Dictionary<Guid, MessageStatus>();
    }

    public Guid Id { get; }

    public string Name { get; }

    public ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure();

        // Status exists only in user context
        _messageStatuses[message.Id] = MessageStatus.Unread;
        return new ReceiveResult.Success();
    }

    public MarkAsReadResult MarkAsRead(Guid messageId)
    {
        if (!_messageStatuses.ContainsKey(messageId))
            return new MarkAsReadResult.NotFound();

        MessageStatus status = _messageStatuses[messageId];

        if (status == MessageStatus.Read)
            return new MarkAsReadResult.AlreadyRead();

        _messageStatuses[messageId] = MessageStatus.Read;

        return new MarkAsReadResult.Success();
    }
}
