using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Station : IRouteSection
{
    public Station(double boardingDisembarkingTime, Speed maxApproachSpeed)
    {
        BoardingDisembarkingTime = boardingDisembarkingTime;
        MaxApproachSpeed = maxApproachSpeed;
    }

    public double BoardingDisembarkingTime { get; }

    public Speed MaxApproachSpeed { get; }

    public MoveResult Move(Train train)
    {
        if (train.Speed.Value > MaxApproachSpeed.Value)
            return new MoveResult.MoveFailure("Train speed exceeds maximum allowed speed for station", train);

        if (train.Acceleration.Value == 0)
            return new MoveResult.MoveFailure("Train is not going!", train);

        var stoppedTrain = new Train(
            train.Mass,
            new Speed(0),
            train.Acceleration,
            train.MaxStrength,
            train.Precision);

        var time = TimeSpan.FromSeconds(BoardingDisembarkingTime);

        var finalTrain = new Train(
            train.Mass,
            train.Speed,
            train.Acceleration,
            train.MaxStrength,
            train.Precision);

        return new MoveResult.MoveSuccess(time, finalTrain);
    }
}