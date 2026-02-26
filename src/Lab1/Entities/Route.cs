using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Route
{
    public Route(IReadOnlyCollection<IRouteSection> sections, Speed maxEndSpeed)
    {
        Sections = sections;
        MaxEndSpeed = maxEndSpeed;
    }

    public IReadOnlyCollection<IRouteSection> Sections { get; }

    public Speed MaxEndSpeed { get; }

    public MoveResult Traverse(Train train)
    {
        Train currentTrain = train;
        TimeSpan time = TimeSpan.Zero;

        foreach (IRouteSection section in Sections)
        {
            MoveResult sectionResult = section.Move(currentTrain);

            time += sectionResult.Time;

            if (!sectionResult.IsSuccess)
                return sectionResult;

            currentTrain = sectionResult.Train;
        }

        if (currentTrain.Speed.Value > MaxEndSpeed.Value)
            return new MoveResult.MoveFailure("Train has not stopped at the end of the route", currentTrain);

        return new MoveResult.MoveSuccess(time, currentTrain);
    }
}
