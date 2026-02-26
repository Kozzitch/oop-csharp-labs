namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record AddResult
{
    private AddResult() { }

    public sealed record Success : AddResult;

    public sealed record Failure : AddResult;
}