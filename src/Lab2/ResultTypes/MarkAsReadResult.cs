namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record MarkAsReadResult
{
    private MarkAsReadResult() { }

    public sealed record Success : MarkAsReadResult;

    public sealed record AlreadyRead : MarkAsReadResult;

    public sealed record NotFound : MarkAsReadResult;
}
