using AdventOfCode.Year2020.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day20Tests
{
    [Fact]
    public void Part1()
    {
        var input = File.ReadAllText("Days/Day20Tests.Data.Part1.txt");
        var day = new Day20();
        var actual = day.Part1(input);
        var expected = 20899048083289UL;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part2()
    {
        var input = File.ReadAllText("Days/Day20Tests.Data.Part1.txt");
        var day = new Day20();
        var actual = day.Part2(input);
        var expected = 273UL;
        Assert.Equal(expected, actual);
    }
}
