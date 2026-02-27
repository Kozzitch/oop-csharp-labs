using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archivers;

public class InMemoryArchiver : IMessageArchiver
{
    private readonly List<Message> _messages;

    public InMemoryArchiver()
    {
        _messages = new List<Message>();
    }

    public IReadOnlyCollection<Message> GetMessages() => _messages.AsReadOnly();

    public ArchiveResult Archive(Message? message)
    {
        if (message is null)
            return new ArchiveResult.Failure();

        _messages.Add(message);

        return new ArchiveResult.Success();
    }
}
