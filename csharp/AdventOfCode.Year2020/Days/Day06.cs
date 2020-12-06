﻿using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class Day06 : IDay
	{
        public async Task ExecuteAsync()
        {
            var input = await System.IO.File.ReadAllTextAsync("./Data/Day06.txt");
            var resultPart1 = Part1(input);
            var resultPart2 = Part2(input);

            Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");
        }
        public int Part1(string input)
        {
            var sum = input
                .Replace("\r", "")
                .Split("\n\n")
                .Aggregate(0, (acc, current) => acc + CountAnswersInGroup(current));
            return sum;
        }

        public int Part2(string input)
        {
            var sum = input
                .Replace("\r", "")
                .Trim()
                .Split("\n\n")
                .Aggregate(0, (acc, current) => acc + CountAllAnsweredSameInGroup(current));

            var test = input
                .Replace("\r", "")
                .Trim()
                .Split("\n\n").ToList();
            return sum;
        }

        public int CountAnswersInGroup(string input)
		{
            
            var replaceRegex = new Regex(@"[\s\n]");
            var allowedCharacters = new Regex(@"[a-z]");

            return replaceRegex.Replace(input, "")
                .ToCharArray()
                .Where(text => allowedCharacters.IsMatch(text.ToString()))
                .Distinct()
                .Count();
        }

        public int CountAllAnsweredSameInGroup(string input)
		{
            var personAnswers = input.Split("\n").ToList();
            var numPersonsInGroup = personAnswers.Count();

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
}
