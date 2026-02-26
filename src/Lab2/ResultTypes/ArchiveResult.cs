namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record ArchiveResult
{
    private ArchiveResult() { }

    public sealed record Success : ArchiveResult;

    public sealed record Failure : ArchiveResult;
}
