using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020
{
    public class PasswordData
    {
      public PasswordPolicy Policy {get; set;}
      public string Password {get; set;}
    }
    /// <Summary>
    /// https://adventofcode.com/2020/day/2
    /// </Summary>
    public class Day02 : IDay
    {
      public Day02()
      {
      }

      public Task ExecuteAsync()
      {
        var regex = new Regex(@"\d+-\d+\s\w:\s(\w+)");
        var input = System.IO.File.ReadAllText("../AdventOfCode.Year2020/Data/Day02.txt")
          .Split(Environment.NewLine)
          .Select(x => {
            var match = regex.Match(x);
            var password = match.Groups[1].Value;
            return new PasswordData(){
              Policy = PasswordPolicy.FromString(x),
              Password = password
            };
          })
          .ToList();
        var resultPart1 = Part1(input);
        var resultPart2 = Part2(input);

        Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");

        return Task.CompletedTask;
      }
      public int Part1(List<PasswordData> input)
      {
        var passwordValidator = new PasswordValidator();
        var result = input.Aggregate(0, (acc, passwordData) =>
        {
          var toAdd = 0;
          if(passwordValidator.ValidatePassword(passwordData.Password, passwordData.Policy))
          {
            toAdd = 1;
          }
          return acc + toAdd;
        });
        return result;
      }

      public int Part2(List<PasswordData> input)
      {
        var passwordValidator = new PasswordValidator();
        var result = input.Aggregate(0, (acc, passwordData) =>
        {
          var toAdd = 0;
          if(passwordValidator.ValidatePasswordPositions(passwordData.Password, passwordData.Policy))
          {
            toAdd = 1;
          }
          return acc + toAdd;
        });
        return result;
      }
    }

}
