using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

/// <summary>
/// https://adventofcode.com/2020/day/6
/// </summary>
public class Day06 : BaseDay<string, int>, IDay
{
    public override int Part1(string input)
    {
        var sum = input
            .Replace("\r", "")
            .Split("\n\n")
            .Select(CountAnswersInGroup)
            .Sum();
        return sum;
    }

    public override int Part2(string input)
    {
        var sum = input
            .Replace("\r", "")
            .Trim()
            .Split("\n\n")
            .Select(CountAllAnsweredSameInGroup)
            .Sum();
        return sum;
    }

    public int CountAnswersInGroup(string input)
    {
        var replaceRegex = new Regex(@"[\s\n]");
        return replaceRegex.Replace(input, "")
            .ToCharArray()
            .Distinct()
            .Count();
    }

    public int CountAllAnsweredSameInGroup(string input)
    {
        var personAnswers = input.Split("\n");
        var numPersonsInGroup = personAnswers.Length;

        var count = personAnswers.SelectMany(x =>
            x
                .ToCharArray()
                .Distinct()
                .ToList()
        ).GroupBy(x => x)
        .Count(x => x.Count() == numPersonsInGroup);
        return count;
    }
}
