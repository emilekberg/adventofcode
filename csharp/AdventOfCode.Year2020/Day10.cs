using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day10 : BaseDay<string[], long>, IDay
	{
		public override long Part1(string[] input)
		{
			var data = input.Select(int.Parse).ToList();
			var deviceJolts = data.Max() + 3;
			data.Add(0);
			data.Add(deviceJolts);
			data.Sort();
			var diffs = data.Zip(data.Skip(1), (a, b) => Diff(a, b)).ToList();

			var ones = diffs.Count(x => x == 1);
			var threes = diffs.Count(x => x == 3);

			return ones * threes;
		}


		/// <summary>
		/// Did not solve this yet... don't fully understand how to sort this yet.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public override long Part2(string[] input)
		{
			var data = input.Select(int.Parse).ToList();
			var deviceJolts = data.Max() + 3;
			data.Add(0);
			data.Add(deviceJolts);
			data.Sort();
			data.Reverse();

			return 0;
		}

		/// <summary>
		/// Calculates the difference between two integers.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int Diff(int a, int b)
		{
			return b - a;
		}
	}
}
