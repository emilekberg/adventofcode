using AdventOfCode.Year2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day06Tests
{
    [Fact]
    public void CountAnswersInGroup()
    {
        string input = @"abcx
abcy
abcz";
        var day = new Day06();
        var actual = day.CountAnswersInGroup(input.Replace("\r", ""));

        Assert.Equal(6, actual);
    }

    [Fact]
    public void Part1_CountsAllGroups()
    {
        var input = @"abc

a
b
c

ab
ac

a
a
a
a

b";
        var day = new Day06();
        var actual = day.Part1(input.Replace("\r", ""));
        var expected = 11;
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(@"abc", 3)]
    [InlineData(@"a
b
c", 0)]
    [InlineData(@"ab
ac", 1)]
    [InlineData(@"a
a
a
a", 1)]
    [InlineData(@"b", 1)]
    public void CountAllAnsweredSameInGroup(string input, int expectedCount)
    {
        var day = new Day06();
        var actual = day.CountAllAnsweredSameInGroup(input.Replace("\r", ""));
        Assert.Equal(expectedCount, actual);
    }

    [Fact]
    public void Part2()
    {
        var input = @"abc

a
b
c

ab
ac

a
a
a
a

b";
        var day = new Day06();
        var actual = day.Part2(input.Replace("\r", ""));
        var expected = 6;
        Assert.Equal(expected, actual);
    }
}
