using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2018;

/// <summary>
/// https://adventofcode.com/2018/day/2
/// </summary>
public class Day02 : BaseDay<string[], int>, IDay
{
    public override int Part1(string[] input)
    {
        return input
            .Select(int.Parse)
            .Sum();
    }

    public override int Part2(string[] input)
    {
        var seen = new HashSet<int>();
        var data = input.Select(int.Parse).ToList();
        var freq = 0;
        seen.Add(freq);
        do
        {
            foreach (var change in data)
            {
                freq += change;
                if (!seen.Add(freq))
                {
                    return freq;
                }
            }
        }
        while (true);
    }
}
