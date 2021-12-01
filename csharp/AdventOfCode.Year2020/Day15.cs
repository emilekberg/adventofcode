using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day15 : BaseDay<string, int>, IDay
{
    public override int Part1(string input)
    {
        return NumberGame(input, 2020);
    }
    public override int Part2(string input)
    {
        return NumberGame(input, 30_000_000);
    }
    public int NumberGame(string input, int nthNumber)
    {
        var numbers = Enumerable.Repeat(-1, nthNumber).ToArray();
        for (int i = 0; i < nthNumber; i++) numbers[i] = -1;
        var currentTurn = 0;
        var lastNumberMentioned = 0;
        input
            .Split(',')
            .Select(int.Parse)
            .ToList()
            .ForEach(number =>
            {
                numbers[number] = ++currentTurn;
                lastNumberMentioned = number;
            });
        // remove this number from being mentioned, as this will be added to the list further down.
        numbers[lastNumberMentioned] = -1;
        for (int i = currentTurn; i < nthNumber; i++)
        {
            var numberToMention = 0;
            if (numbers[lastNumberMentioned] != -1)
            {
                numberToMention = i - numbers[lastNumberMentioned];
            }
            numbers[lastNumberMentioned] = i;
            lastNumberMentioned = numberToMention;
        }
        return lastNumberMentioned;
    }
}
