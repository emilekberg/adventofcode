using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

public class Day01Test
{
    public static IEnumerable<object[]> Part1Data =>
    new List<object[]>
    {
            new object[] { new string[] { "+1", "+1", "+1" }, 3 },
            new object[] { new string[] { "+1", "+1", "-2" }, 0 },
            new object[] { new string[] { "-1", "-2", "-3" }, -6 },
    };
    [Theory]
    [MemberData(nameof(Part1Data))]
    public void Part1(string[] input, int expected)
    {
        var day = new Day01();
        var actual = day.Part1(input);
        Assert.Equal(expected, actual);
    }
    public static IEnumerable<object[]> Part2Data =>
    new List<object[]>
    {
            new object[] { new string[] { "+1", "-1" }, 0 },
            new object[] { new string[] { "+3", "+3", "+4", "-2", "-4" }, 10 },
            new object[] { new string[] { "-6", "+3", "+8", "+5", "-6" }, 5 },
            new object[] { new string[] { "+7", "+7", "-2", "-7", "-4" }, 14 },
    };
    [Theory]
    [MemberData(nameof(Part2Data))]
    public void Part2(string[] input, int expected)
    {
        var day = new Day01();
        var actual = day.Part2(input);
        Assert.Equal(expected, actual);
    }
}
