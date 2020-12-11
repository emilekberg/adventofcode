using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
namespace AdventOfCode.Year2020
{
	public class Day09 : BaseDay<string[], long>, IDay
	{
		public override long Part1(string[] input) => Part1(input, 25);
		public long Part1(string[] input, int window)
		{
			var data = input.Select(long.Parse).ToList();
			int offset = 0;
			do
			{
				var preamble = data.Skip(offset).Take(window).ToList();
				var toFind = data.Skip(offset + window).Take(1).Single();

				var exists = Permutations(preamble)
					.Select(x => x.a + x.b)
					.Any(x => x == toFind);
				offset++;
				if (exists) continue;
				return toFind;
			}
			while (true);
		}

		/// <summary>
		/// Returns a list of permutations from the input data.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public List<(long a, long b)> Permutations(List<long> data)
		{
			return data
				.SelectMany((x, i) => data.Skip(i+1), (a, b) => (a, b))
				.Where((x, y) => x.a != x.b)
				.ToList();
		}

		public override long Part2(string[] input) => Part2(input, 25);
		public long Part2(string[] input, int window)
		{
			var toFind = Part1(input, window);

			var data = input.Select(long.Parse).ToList();
			var start = 0;
			var offset = 2;

			do
			{
				var subSetOfNumbers = data.Skip(start).Take(offset);
				var sum = subSetOfNumbers.Sum();
				if (sum < toFind)
				{
					offset++;
				}
				else if(sum > toFind)
				{
					start++;
					offset = 2;
				}
				else if (sum == toFind)
				{
					return subSetOfNumbers.Min() + subSetOfNumbers.Max();
				}
			}
			while (true);
		}
	}
}
