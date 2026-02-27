using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Notification;

public class TextNotificationSystem : INotificationSystem
{
    public NotifyResult Notify(string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return new NotifyResult.Failure();

        Console.WriteLine($"[NOTIFICATION] {message}");

        return new NotifyResult.Success();
    }
}
