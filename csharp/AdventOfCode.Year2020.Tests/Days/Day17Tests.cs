using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day17Tests
{
    [Fact]
    public void Part1()
    {
        var input = new string[]
        {
                ".#.",
                "..#",
                "###"
        };

        var day = new Day17();
        var expected = 112;
        var actual = day.Part1(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part2()
    {
        var input = new string[]
        {
                ".#.",
                "..#",
                "###"
        };

        var day = new Day17();
        var expected = 848;
        var actual = day.Part2(input);
        Assert.Equal(expected, actual);
    }
}
