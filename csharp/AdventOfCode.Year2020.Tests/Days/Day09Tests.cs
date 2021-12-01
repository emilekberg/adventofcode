using AdventOfCode.Year2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day09Tests
{
    [Fact]
    public void Part1()
    {
        var input = new[]
        {
            "35",
            "20",
            "15",
            "25",
            "47",
            "40",
            "62",
            "55",
            "65",
            "95",
            "102",
            "117",
            "150",
            "182",
            "127",
            "219",
            "299",
            "277",
            "309",
            "576"
        };

        var day = new Day09();
        var actual = day.Part1(input, 5);
        var expected = 127;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part2()
    {
        var input = new[]
        {
            "35",
            "20",
            "15",
            "25",
            "47",
            "40",
            "62",
            "55",
            "65",
            "95",
            "102",
            "117",
            "150",
            "182",
            "127",
            "219",
            "299",
            "277",
            "309",
            "576"
        };

        var day = new Day09();
        var actual = day.Part2(input, 5);
        var expected = 15 + 47; // 62
        Assert.Equal(expected, actual);
    }
}
