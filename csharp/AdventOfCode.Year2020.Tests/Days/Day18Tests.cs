using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days;

public class Day18Tests
{
    public static IEnumerable<object[]> Part1_Data =>
        new List<object[]>
        {
                new object[]
                {
                    new string[]
                    {
                        "1 + 2 + 3 + 4 + 5"
                    },
                    15
                },
                new object[]
                {
                    new string[]
                    {
                        "1 + 3 * 5 + 2"
                    },
                    22
                },
                new object[]
                {
                    new string[]
                    {
                        "2 * 3 + (4 * 5)"
                    },
                    26
                },
                new object[]
                {
                    new string[]
                    {
                        "5 + (8 * 3 + 9 + 3 * 4 * 3)"
                    },
                    437
                },
                new object[]
                {
                    new string[]
                    {
                        "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
                    },
                    12240
                },
                new object[]
                {
                    new string[]
                    {
                        "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
                    },
                    13632
                },
                new object[]
                {
                    new string[]
                    {
                        "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
                        "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
                    },
                    12240 + 13632
                }
        };
    [Theory]
    [MemberData(nameof(Part1_Data))]
    public void Part1(string[] input, int expected)
    {
        var day = new Day18();
        var actual = day.Part1(input);
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> Part2_Data =>
        new List<object[]>
        {
            new object[]
            {
                new string[]
                {
                    "1 + (2 * 3) + (4 * (5 + 6))"
                },
                51
            },
            new object[]
            {
                new string[]
                {
                    "2 * 3 + (4 * 5)"
                },
                46
            },
            new object[]
            {
                new string[]
                {
                    "5 + (8 * 3 + 9 + 3 * 4 * 3)"
                },
                1445
            },
            new object[]
            {
                new string[]
                {
                    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
                },
                669060
            },
            new object[]
            {
                new string[]
                {
                    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
                },
                23340
            }
        };
    [Theory]
    [MemberData(nameof(Part2_Data))]
    public void Part2(string[] input, long expected)
    {
        var day = new Day18();
        var actual = day.Part2(input);
        Assert.Equal(expected, actual);
    }
}
 