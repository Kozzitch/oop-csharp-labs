using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Notification;

// Proxy Pattern: Isolation and Control
public class NotificationSystemProxy : INotificationSystem
{
    private readonly INotificationSystem _realSystem;
    private readonly ILogger _logger;

    public NotificationSystemProxy(INotificationSystem? realSystem, ILogger? logger)
    {
        if (realSystem is null)
            throw new ArgumentException("RealSystem cannot be empty", nameof(realSystem));

        if (logger is null)
            throw new ArgumentException("Logger cannot be empty", nameof(logger));

        _realSystem = realSystem;
        _logger = logger;
    }

    public NotifyResult Notify(string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return new NotifyResult.Success();

        _logger.Log($"Notification requested: {message}");
        _realSystem.Notify(message);

        return new NotifyResult.Success();
    }
}
