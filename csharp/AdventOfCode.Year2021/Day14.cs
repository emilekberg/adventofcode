using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/14
/// </Summary> 
public class Day14 : BaseDay<string[], long>, IDay
{
	public static (string data, Dictionary<string, char> lookup) Parse(string[] input)
	{
		var data = input.First();
		var lookup = input.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1].First());
		return (data, lookup);
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
		var (data, lookup) = Parse(input);
		var iteratedData = data;
		for (int i = 0; i < iterations; i++)
		{
			iteratedData = Iterate(iteratedData, lookup);
		}

		var groups = iteratedData.ToCharArray().GroupBy(x => x);

		long min = groups.Min(x => x.Count());
		long max = groups.Max(x => x.Count());

		return max - min;
	}

	public static string Iterate(string data, Dictionary<string, char> lookup)
	{
		var sb = new StringBuilder();
		for(int i = 1; i < data.Length; i++)
		{
			var prev = data[i - 1];
			var current = data[i];
			var key = "" + prev + current;
			var insert = lookup[key];

			sb.Append(prev);
			sb.Append(insert);
		}
		sb.Append(data.Last());
		var result = sb.ToString();
		return result;
	}
}