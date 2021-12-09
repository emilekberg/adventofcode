using AdventOfCode.Common;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day08 : BaseDay<string[], long>, IDay
{
	const int oneLength = 2;
	const int fourLength = 4;
	const int sevenLength = 3;
	const int eightLength = 7;
	public override long Part1(string[] input)
	{

		var parsedInput = input
			.Select(x => x.Split(" | ")[1])
			.SelectMany(x => x.Split(" "))
			.Count(entry => entry.Length is 2 or 4 or 3 or 7);
		return parsedInput;
	}
	public override long Part2(string[] input)
	{
		var parsedInput = input
			.Select(x => x.Split(" | "))
			.ToList();

		var sum = 0;
		foreach (var row in parsedInput)
		{
			var definition = row[0].Split(' ');
			var numbers = row[1].Split(' ');
			sum += TryFindDigits(definition, numbers);
		}
		return sum;
	}

	public static bool ContainsCharArray(char[] a, char[] b)
	{
		return b.Aggregate(true, (acc, next) =>
		{
			return acc && a.Contains(next);
		});
	}

	public static int TryFindDigits(string[] definitions, string[] outputValue)
	{
		var orderedOutput = outputValue.Select(x => x.OrderBy(x => x).ToArray());
		var orderedDefinitions = definitions.Select(x => x.OrderBy(x => x).ToArray()).ToList();
		char[] one = orderedDefinitions.Single(x => x.Length is 2);
		char[] four = orderedDefinitions.Single(x => x.Length is 4);
		char[] fourWithoutOne = four.Except(one).ToArray();
		var digits = orderedDefinitions.Select((definition, index) =>
		{
			var result = -1;
			if (definition.Length is 2) result = 1;
			else if (definition.Length is 4) result = 4;
			else if (definition.Length is 3) result = 7;
			else if (definition.Length is 7) result = 8;
			else if (definition.Length == 5)
			{
				// 5 length: 2,3,5
				if (ContainsCharArray(definition, one)) result = 3;
				else if (ContainsCharArray(definition, fourWithoutOne)) result = 5;
				else result = 2;
			}
			else if (definition.Length == 6)
			{
				// 6 length: 0,6,9
				if (ContainsCharArray(definition, four)) result = 9;
				else if (ContainsCharArray(definition, one)) result = 0;
				else result = 6;
			}
			var key = new string(definition);
			return (key, result);

		}).ToDictionary(x => x.key, x => x.result);
		var stringValue = orderedOutput
			.Select(x => new string(x))
			.Select(x => digits[x])
			.Aggregate(new StringBuilder(), (acc, next) => acc.Append(next))
			.ToString();
		return int.Parse(stringValue);
	}
}