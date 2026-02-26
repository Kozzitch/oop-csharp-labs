using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObject;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class Tests1
{
    [Fact]
    public void Scenario1()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(200), new Strength(1000)),
            new ConventionalMagneticPaths(new Distance(500)),
        };
        var route = new Route(sections, new Speed(40));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveSuccess>(result);
    }

    [Fact]
    public void Scenario2()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(300), new Strength(3000)),
            new ConventionalMagneticPaths(new Distance(500)),
        };
        var route = new Route(sections, new Speed(15));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveFailure>(result);
    }

    [Fact]
    public void Scenario3()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(150), new Strength(1000)),
            new ConventionalMagneticPaths(new Distance(100)),
            new Station(5, new Speed(25)),
            new ConventionalMagneticPaths(new Distance(200)),
        };
        var route = new Route(sections, new Speed(30));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveSuccess>(result);
    }

    [Fact]
    public void Scenario4()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(250), new Strength(2500)),
            new Station(5, new Speed(20)),
            new ConventionalMagneticPaths(new Distance(300)),
        };
        var route = new Route(sections, new Speed(30));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveFailure>(result);
    }

    [Fact]
    public void Scenario5()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(200), new Strength(2200)),
            new ConventionalMagneticPaths(new Distance(400)),
            new Station(5, new Speed(30)),
            new ConventionalMagneticPaths(new Distance(300)),
        };
        var route = new Route(sections, new Speed(25));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveFailure>(result);
    }

    [Fact]
    public void Scenario6()
    {
        var train = new Train(1000, 0, 0, 4000, 0.1d);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(150), new Strength(3000)),
            new ConventionalMagneticPaths(new Distance(200)),
            new PowerMagneticPaths(new Distance(500), new Strength(-1500)),
            new Station(6, new Speed(25)),
            new ConventionalMagneticPaths(new Distance(180)),
            new PowerMagneticPaths(new Distance(120), new Strength(2800)),
            new ConventionalMagneticPaths(new Distance(250)),
            new PowerMagneticPaths(new Distance(500), new Strength(-2000)),
        };
        var route = new Route(sections, new Speed(10));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveSuccess>(result);
    }

    [Fact]
    public void Scenario7()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);
        var sections = new IRouteSection[]
        {
            new ConventionalMagneticPaths(new Distance(500)),
        };
        var route = new Route(sections, new Speed(10));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveFailure>(result);
    }

    [Fact]
    public void Scenario8()
    {
        var train = new Train(1000, 0, 0, 5000, 0.1d);

        const double X = 100;
        const double Y = 1000;

        var sections = new IRouteSection[]
        {
            new PowerMagneticPaths(new Distance(X), new Strength(Y)),
            new PowerMagneticPaths(new Distance(X), new Strength(Y * (-2))),
        };
        var route = new Route(sections, new Speed(10));

        MoveResult result = route.Traverse(train);
        Assert.IsType<MoveResult.MoveFailure>(result);
    }
}





