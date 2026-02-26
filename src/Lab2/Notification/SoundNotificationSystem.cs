using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Notification;

public class SoundNotificationSystem : INotificationSystem
{
    public NotifyResult Notify(string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return new NotifyResult.Failure();

        Console.Beep();
        Console.WriteLine($"[SOUND NOTIFICATION] {message}");

        return new NotifyResult.Success();
    }
}
