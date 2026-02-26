namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record ReceiveResult
{
    private ReceiveResult() { }

    public sealed record Success : ReceiveResult;

    public sealed record Failure : ReceiveResult;
}
