using Itmo.ObjectOrientedProgramming.Lab2.Notification;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class NotificationRecipient : IMessageRecipient
{
    private readonly INotificationSystem _notificationSystem;
    private readonly IReadOnlyCollection<string> _suspiciousWords;

    public NotificationRecipient(INotificationSystem notificationSystem, IEnumerable<string> suspiciousWords)
    {
        if (notificationSystem is null)
            throw new ArgumentException("NotificationSystem cannot be empty", nameof(notificationSystem));

        if (suspiciousWords is null)
            throw new ArgumentException("SuspiciousWords cannot be empty", nameof(suspiciousWords));

        _notificationSystem = notificationSystem;

        _suspiciousWords = suspiciousWords.ToList();
    }

    public ReceiveResult Receive(Message? message)
    {
        if (message is null)
            return new ReceiveResult.Failure("Message cannot be empty", message);

        foreach (string word in _suspiciousWords)
        {
            if (message.Body.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                _notificationSystem.Notify($"Suspicious message detected: {message.Id}");
                return new ReceiveResult.Success();
            }
        }

        return new ReceiveResult.Success();
    }
}
