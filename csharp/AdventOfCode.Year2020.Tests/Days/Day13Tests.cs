using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day13Tests
{
    [Fact]
    public void Part1()
    {
        var input = new string[]
        {
                "939",
                "7,13,x,x,59,x,31,19"
        };
        var day = new Day13();

        var expected = 295;
        var actual = day.Part1(input);

        Assert.Equal(expected, actual);

    }

    [Theory]
    // [InlineData("7,13,x,x,59,x,31,19", 1068788)]
    [InlineData("17,x,13,19", 3417)]
    [InlineData("67,7,59,61", 754018)]
    [InlineData("67,x,7,59,61", 779210)]
    [InlineData("67,7,x,59,61", 1261476)]
    // [InlineData("1789,37,47,1889", 1261476)]

    public void Part2(string data, int expected)
    {
        var input = new string[]
        {
                "ignored value",
                data
        };
        var day = new Day13();

        var actual = day.Part2(input);

        Assert.Equal(expected, actual);

    }
}
