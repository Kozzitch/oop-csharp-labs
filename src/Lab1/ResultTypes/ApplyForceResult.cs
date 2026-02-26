using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public abstract record ApplyForceResult
{
    public abstract bool IsSuccess { get; }

    public abstract string? Fault { get; }

    public abstract Train Value { get; }

    public sealed record Success(Train Data) : ApplyForceResult
    {
        public override bool IsSuccess => true;

        public override string? Fault => null;

        public override Train Value => Data;
    }

    public sealed record Failure(string Defect) : ApplyForceResult
    {
        public override bool IsSuccess => false;

        public override string? Fault => Defect;

        public override Train Value =>
            throw new InvalidOperationException($"Cannot access value: {Defect}");
    }
}