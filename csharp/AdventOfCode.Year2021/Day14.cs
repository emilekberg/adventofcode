using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/14
/// </Summary> 
public class Day14 : BaseDay<string[], long>, IDay
{
	public static (Dictionary<string, long>, Dictionary<string, char> lookup) Parse(string[] input)
	{
		var data = input.First();
		var lookup = input.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1].First());

		var pairs = data
			.Zip(data.Skip(1))
			.Select(x => "" + x.First + x.Second)
			.GroupBy(x => x)
			.ToDictionary(x => x.Key, x => (long)x.Count());
		return (pairs, lookup);
	}
	public override long Part1(string[] input)
	{
		return Process(input, 10);
	}
	public override long Part2(string[] input)
	{
		return Process(input, 40);
	}
	public static long Process(string[] input, int iterations)
	{
		var (pairs, lookup) = Parse(input);
		for (int i = 0; i < iterations; i++)
		{
			pairs = Iterate(pairs, lookup);
		}

		var target = new Dictionary<char, long>();
		foreach(var pair in pairs)
		{
			target.TryAdd(pair.Key[1], 0);
			target[pair.Key[1]] += pair.Value;
		}

		long min = target.Values.Min(x => x);
		long max = target.Values.Max(x => x);

		return max - min;
	}

	public static Dictionary<string, long> Iterate(Dictionary<string, long> pairs, Dictionary<string, char> lookup)
	{
		var newPairs = new Dictionary<string, long>();

		foreach(var pair in pairs)
		{
			var first = pair.Key[0];
			var second = pair.Key[1];
			var elementToAdd = lookup[pair.Key];
			
			var pairA = "" + first + elementToAdd;
			var pairB = "" + elementToAdd + second;

			newPairs.TryAdd(pairA, 0);
			newPairs[pairA] += pair.Value;
			newPairs.TryAdd(pairB, 0);
			newPairs[pairB] += pair.Value;
		}
		return newPairs;
	}
}