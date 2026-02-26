namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record LogResult
{
    private LogResult() { }

    public sealed record Success : LogResult;

    public sealed record Failure : LogResult;
}