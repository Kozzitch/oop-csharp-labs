using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class UserRecipient : IMessageRecipient
{
    private readonly User _user;

    public UserRecipient(User user)
    {
        if (user is null)
            throw new ArgumentException("User cannot be empty", nameof(user));

        _user = user;
    }

    public ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure();

        _user.Receive(message);

        return new ReceiveResult.Success();
    }
}