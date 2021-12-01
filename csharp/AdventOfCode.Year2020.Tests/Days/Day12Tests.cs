using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day12Tests
{
    [Theory]
    [InlineData(Direction.North, Direction.Right, 90, Direction.East)]
    [InlineData(Direction.North, Direction.Left, 90, Direction.West)]
    [InlineData(Direction.East, Direction.Left, 90, Direction.North)]
    [InlineData(Direction.East, Direction.Right, 90, Direction.South)]
    [InlineData(Direction.South, Direction.Left, 90, Direction.East)]
    [InlineData(Direction.South, Direction.Right, 90, Direction.West)]
    [InlineData(Direction.West, Direction.Left, 90, Direction.South)]
    [InlineData(Direction.West, Direction.Right, 90, Direction.North)]
    [InlineData(Direction.North, Direction.Right, 180, Direction.South)]
    [InlineData(Direction.North, Direction.Left, 180, Direction.South)]
    [InlineData(Direction.West, Direction.Right, 180, Direction.East)]
    [InlineData(Direction.West, Direction.Left, 180, Direction.East)]
    [InlineData(Direction.North, Direction.Right, 270, Direction.West)]
    [InlineData(Direction.East, Direction.Left, 270, Direction.South)]
    public void Rotate(Direction heading, Direction direction, int amount, Direction expected)
    {
        var actual = Day12.Rotate(heading, direction, amount);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part1()
    {
        var input = new string[]
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11"
        };
        var day = new Day12();
        var expected = 25;
        var actual = day.Part1(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(10, 4, Direction.Right, 90, 4, -10)]
    [InlineData(10, 4, Direction.Left, 90, -4, 10)]
    [InlineData(10, 4, Direction.Right, 180, -10, -4)]
    [InlineData(10, 4, Direction.Right, 270, -4, 10)]
    public void RotateWaypoint(int x, int y, Direction direction, int amount, int expectedX, int expectedY)
    {
        var position = new Position(x, y);
        var expected = new Position(expectedX, expectedY);
        var actualPosition = Day12.RotateWaypoint(position, direction, amount);

        Assert.Equal(expected, actualPosition);
    }

    [Fact]
    public void Part2()
    {
        var input = new string[]
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11"
        };
        var day = new Day12();
        var expected = 286;
        var actual = day.Part2(input);

        Assert.Equal(expected, actual);
    }
}
