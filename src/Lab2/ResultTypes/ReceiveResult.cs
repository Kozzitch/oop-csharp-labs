using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record ReceiveResult
{
    private ReceiveResult() { }

    public sealed record Success : ReceiveResult;

    public sealed record Failure(string Error, Message? Message) : ReceiveResult;

    public sealed record Filtered(string Reason) : ReceiveResult;
}
