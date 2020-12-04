using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
namespace AdventOfCode.Year2020
{
    /// <Summary>
    /// https://adventofcode.com/2020/day/1
    /// </Summary>
    public class Day01 : IDay
    {
      public Day01()
      {
      }

      public Task ExecuteAsync()
      {
        var input = System.IO.File.ReadAllText("../AdventOfCode.Year2020/Data/Day01.txt")
          .Split(Environment.NewLine)
          .Select(x => int.Parse(x))
          .ToList();
        var resultPart1 = Part1(input);
        var resultPart2 = Part2(input);

        Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");

        return Task.CompletedTask;
      }
      public int Part1(List<int> numbers)
      {
        var sumFinder = new SumFinder(numbers);
        List<int> parts = sumFinder.FindSum(2020, 2);
        return parts.Aggregate(1, (acc, next) => acc * next);
      }

      public int Part2(List<int> numbers)
      {
        var sumFinder = new SumFinder(numbers);
        List<int> parts = sumFinder.FindSum(2020, 3);
        return parts.Aggregate(1, (acc, next) => acc * next);
      }
    }
}
