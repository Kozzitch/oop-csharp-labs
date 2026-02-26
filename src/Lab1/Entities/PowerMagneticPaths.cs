using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class PowerMagneticPaths : IRouteSection
{
    public PowerMagneticPaths(Distance length, Strength force)
    {
        Length = length;
        Force = force;
    }

    public Distance Length { get; }

    public Strength Force { get; }

    public MoveResult Move(Train train)
    {
        ApplyForceResult applyForceResult = train.ApplyForce(Force);

        if (!applyForceResult.IsSuccess)
            return new MoveResult.MoveFailure(applyForceResult.Fault, train);

        MoveResult calculateResult = applyForceResult.Value.CalculateTimeToCoverDistance(Length);

        return calculateResult;
    }
}
