using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class ConventionalMagneticPaths : IRouteSection
{
    public ConventionalMagneticPaths(Distance length)
    {
        Length = length;
    }

    public Distance Length { get; }

    public MoveResult Move(Train train)
    {
        if (train.Acceleration.Value == 0)
            return new MoveResult.MoveFailure("Train is not going!", train);

        MoveResult calculateResult = train.CalculateTimeToCoverDistance(Length);

        return calculateResult;
    }
}