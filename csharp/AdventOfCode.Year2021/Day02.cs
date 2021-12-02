using AdventOfCode.Common;

namespace AdventOfCode.Year2021;

/// <Summary>
/// https://adventofcode.com/2021/day/2
/// </Summary>
public class Day02 : BaseDay<string[], long>, IDay
{
	public override long Part1(string[] input)
	{
		long depth = 0;
		long horizontal = 0;

		ParseInput(input).ForEach(e =>
		{
			switch (e.command)
			{
				case "forward":
					horizontal += e.amount;
					break;
				case "down":
					depth += e.amount;
					break;
				case "up":
					depth -= e.amount;
					break;
			}
		});
		return horizontal * depth;
	}

	public override long Part2(string[] input)
	{
		long depth = 0;
		long horizontal = 0;
		long aim = 0;
		ParseInput(input).ForEach(e =>
		{
			switch (e.command)
			{
				case "forward":
					horizontal += e.amount;
					depth += aim * e.amount;
					break;
				case "down":
					aim += e.amount;
					break;
				case "up":
					aim -= e.amount;
					break;
			}
		});
		return horizontal * depth;
	}
	public static List<(string command, int amount)> ParseInput(string[] input) => input.Select(s =>
	{
		var split = s.Split(' ');
		var command = split[0];
		var amount = int.Parse(split[1]);
		return (command, amount);
	}).ToList();
}
