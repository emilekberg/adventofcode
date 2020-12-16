using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
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
			var hashset = new Dictionary<int, int>(nthNumber);
			var currentTurn = 0;
			var lastNumberMentioned = 0;
			input
				.Split(',')
				.Select(int.Parse)
				.ToList()
				.ForEach(x =>
				{
					hashset.Add(x, ++currentTurn);
					lastNumberMentioned = x;
				});
			hashset.Remove(hashset.Last().Key);

			for(int i = currentTurn; i < nthNumber; i++)
			{
				var numberToMention = 0;
				if (hashset.TryGetValue(lastNumberMentioned, out var age))
				{
					numberToMention = i - age;
				}
				hashset[lastNumberMentioned] = i;
				lastNumberMentioned = numberToMention;
			}
			return lastNumberMentioned;
		}
	}
}
