namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

public class Distance
{
    public Distance(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value cannot be negative", nameof(value));
        }

        Value = value;
    }

    public double Value { get; }
}
