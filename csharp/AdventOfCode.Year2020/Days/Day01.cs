using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.IO;

namespace AdventOfCode.Year2020
{
	/// <Summary>
	/// https://adventofcode.com/2020/day/1
	/// </Summary>
	public class Day01 : BaseDay<string[], int>, IDay
	{
		public override Task<string[]> LoadData(string filePath)
		{
			return File.ReadAllLinesAsync(filePath);
		}

		public override int Part1(string[] input)
		{
			var numbers = input.Select(int.Parse).ToList();
			var sumFinder = new SumFinder(numbers);
			List<int> parts = sumFinder.FindSum(2020, 2);
			return parts.Aggregate(1, (acc, next) => acc * next);
		}

		public override int Part2(string[] input)
		{
			var numbers = input.Select(int.Parse).ToList();
			var sumFinder = new SumFinder(numbers);
			List<int> parts = sumFinder.FindSum(2020, 3);
			return parts.Aggregate(1, (acc, next) => acc * next);
		}
	}
}
