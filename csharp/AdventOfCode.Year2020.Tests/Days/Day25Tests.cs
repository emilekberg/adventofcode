using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day25Tests
{
    [Fact]
    public void Part1()
    {
        var input = new string[]
        {
                "5764801",
                "17807724"
        };
        var day = new Day25();
        var actual = day.Part1(input);
        var expected = 14897079;
        Assert.Equal(expected, actual);
    }
}
