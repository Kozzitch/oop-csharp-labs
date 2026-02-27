using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record FormatResult
{
    public abstract Message? MessageBase { get; }

    public abstract string? FormatingString { get; }

    public abstract string? Fault { get; }

    private FormatResult() { }

    public sealed record Success(string FormatingString, Message Message) : FormatResult
    {
        public override Message? MessageBase { get; } = Message;

        public override string? FormatingString { get; } = FormatingString;

        public override string? Fault => null;
    }

    public sealed record Failure(string? Fault, Message? Message) : FormatResult
    {
        public override Message? MessageBase { get; } = Message;

        public override string? FormatingString { get; } = null;

        public override string? Fault { get; } = Fault;
    }
}