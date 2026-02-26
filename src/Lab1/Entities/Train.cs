using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Train
{
    public Train(double mass, double speed, double acceleration, double maxStrength, double precision)
    {
        Mass = new Mass(mass);
        Speed = new Speed(speed);
        Acceleration = new Acceleration(acceleration);
        MaxStrength = new Strength(maxStrength);
        Precision = precision;
    }

    public Train(Mass mass, Speed speed, Acceleration acceleration, Strength maxStrength, double precision)
    {
        Mass = mass;
        Speed = speed;
        Acceleration = acceleration;
        MaxStrength = maxStrength;
        Precision = precision;
    }

    public Mass Mass { get; }

    public Speed Speed { get; }

    public Acceleration Acceleration { get; }

    public Strength MaxStrength { get; }

    public double Precision { get; }

    public ApplyForceResult ApplyForce(Strength strength)
    {
        if (Math.Abs(strength.Value) > MaxStrength.Value)
            return new ApplyForceResult.Failure("Force exceeds maximum allowed strength");

        var newAcceleration = new Acceleration(strength.Value / Mass.Value);

        return new ApplyForceResult.Success(new Train(
            Mass,
            Speed,
            newAcceleration,
            MaxStrength,
            Precision));
    }

    public MoveResult CalculateTimeToCoverDistance(Distance distance)
    {
        double remainingDistance = distance.Value;
        double currentTime = 0;
        Train currentTrain = this;

        while (remainingDistance > 0)
        {
            double resultingSpeed = currentTrain.Speed.Value + (currentTrain.Acceleration.Value * Precision);

            if (resultingSpeed < 0)
                return new MoveResult.MoveFailure("Negative speed encountered", currentTrain);

            double coveredDistance = resultingSpeed * Precision;

            if (coveredDistance >= remainingDistance)
                break;

            remainingDistance -= coveredDistance;
            currentTime += Precision;

            currentTrain = new Train(
                currentTrain.Mass,
                new Speed(resultingSpeed),
                currentTrain.Acceleration,
                currentTrain.MaxStrength,
                currentTrain.Precision);
        }

        return new MoveResult.MoveSuccess(TimeSpan.FromSeconds(currentTime), currentTrain);
    }
}
