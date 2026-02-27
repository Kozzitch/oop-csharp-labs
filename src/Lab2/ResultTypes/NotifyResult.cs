namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record NotifyResult
{
    private NotifyResult() { }

    public sealed record Success : NotifyResult;

    public sealed record SuccessWithLog(LogResult LogResult) : NotifyResult;

    public sealed record Failure : NotifyResult;
}
