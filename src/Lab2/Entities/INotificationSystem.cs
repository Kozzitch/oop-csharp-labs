using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public interface INotificationSystem
{
    NotifyResult Notify(string? message);
}
