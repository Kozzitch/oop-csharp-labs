namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

public class Mass
{
    public Mass(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value cannot be negative", nameof(value));
        }

        Value = value;
    }

    public double Value { get; }
}
