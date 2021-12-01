using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.IO;

namespace AdventOfCode.Year2020;

/// <Summary>
/// https://adventofcode.com/2020/day/1
/// </Summary>
public class Day01 : BaseDay<string[], int>, IDay
{
	public override int Part1(string[] input)
	{
		var numbers = input.Select(int.Parse).ToList();

		foreach (var a in numbers)
		{
			foreach (var b in numbers)
			{
				if (a + b == 2020)
				{
					return a * b;
				}
			}
		}

		return -1;
	}

	public override int Part2(string[] input)
	{
		var numbers = input.Select(int.Parse).ToList();
		foreach (var a in numbers)
		{
			foreach (var b in numbers)
			{
				foreach (var c in numbers)
				{
					if (a + b + c == 2020)
					{
						return a * b * c;
					}
				}
			}
		}
		return 0;
	}
}
