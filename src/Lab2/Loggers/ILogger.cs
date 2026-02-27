using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Loggers;

public interface ILogger
{
    LogResult Log(string? entry);
}
