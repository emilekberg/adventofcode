using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day21Tests
{
    [Fact]
    public void Part1()
    {
        var input = new string[]
        {
                "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                "sqjhc fvjkl (contains soy)",
                "sqjhc mxmxvkd sbzzf (contains fish)",
        };
        var day = new Day21();
        var expected = 5;
        var actual = day.Part1(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part2()
    {
        var input = new string[]
        {
                "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                "sqjhc fvjkl (contains soy)",
                "sqjhc mxmxvkd sbzzf (contains fish)",
        };
        var day = new Day21();
        day.Part2(input);
        var expected = "mxmxvkd,sqjhc,fvjkl";
        var actual = day.DangerousList;

        Assert.Equal(expected, actual);
    }
}
