using AdventOfCode.Year2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day10Tests
{
    public static IEnumerable<object[]> Part1Data =>
    new List<object[]>
    {
        new object[] { new string[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, 35 },
        new object[] { new string[] { "28","33","18","42","31","14","46","20","48","47","24","23","49","45","19","38","39","11","1","32","25","35","8","17","7","9","4","2","34","10","3" }, 220 },
    };
    [Theory]
    [MemberData(nameof(Part1Data))]
    public void Part1(string[] input, ulong expected)
    {
        var day = new Day10();
        var actual = day.Part1(input);
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> Part2Data =>
        new List<object[]>
        {
            new object[] { new string[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, 8 },
            new object[] { new string[] { "28","33","18","42","31","14","46","20","48","47","24","23","49","45","19","38","39","11","1","32","25","35","8","17","7","9","4","2","34","10","3" }, 19208 },
        };
    [Theory]
    [MemberData(nameof(Part2Data))]
    public void Part2(string[] input, ulong expected)
    {
        var day = new Day10();
        var actual = day.Part2(input);
        Assert.Equal(expected, actual);
    }
}
