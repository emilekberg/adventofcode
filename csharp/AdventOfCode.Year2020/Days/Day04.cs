using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020
{
    /// <Summary>
    /// https://adventofcode.com/2020/day/2
    /// </Summary>
    public class Day04 : IDay
    {
      public Day04()
      {
      }

      public Task ExecuteAsync()
      {
        var input = System.IO.File.ReadAllText("../AdventOfCode.Year2020/Data/Day04.txt");
        var resultPart1 = Part1(input);
        var resultPart2 = Part2(input);

        Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");

        return Task.CompletedTask;
      }
      public int Part1(string input)
      {
        var factory = new PassportFactory();
        var validator = new PassportValidator();
        var passports = factory.Create(input);
        var validPassports = passports.Where(passport => validator.HasRequiredFields(passport));
        return validPassports.Count();
      }

      public ulong Part2(string input)
      {
        return 0;
      }
    }

}
