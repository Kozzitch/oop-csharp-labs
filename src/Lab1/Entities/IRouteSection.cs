using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public interface IRouteSection
{
    MoveResult Move(Train train);
}