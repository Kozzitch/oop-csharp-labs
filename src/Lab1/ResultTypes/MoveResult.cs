using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public abstract record MoveResult
{
    public abstract bool IsSuccess { get; }

    public abstract string? Fault { get; }

    public abstract TimeSpan Time { get; }

    public abstract Train Train { get; }

    public sealed record MoveSuccess(TimeSpan Time, Train Train) : MoveResult
    {
        public override bool IsSuccess => true;

        public override string? Fault => null;

        public override Train Train { get; } = Train;

        public override TimeSpan Time { get; } = Time;
    }

    public sealed record MoveFailure(string? Fault, Train Train) : MoveResult
    {
        public override bool IsSuccess => false;

        public override string? Fault { get; } = Fault;

        public override Train Train { get; } = Train;

        public override TimeSpan Time => TimeSpan.Zero;
    }
}
