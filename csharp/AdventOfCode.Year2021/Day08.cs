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
			.Select(x => x.Split(" | "))
			.Select(x => x[1])
			.Select(x => x.Split(" "))
			.ToList()
			.Select(x => x.Count(entry =>
			{
				var number = (entry.Length is oneLength or fourLength or sevenLength or eightLength);
				return number;
			}))
			.Sum();
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
			sum += TryFindDigitsForRow(definition, numbers);
		}
		return sum;
	}

	public int TryFindDigitsForRow(string[] definition, string[] outputValue)
	{
		var orderedDefinitions = definition.Select(x => x.OrderBy(y => y).ToArray()).ToList();
		char[] zero;
		char[] one = definition.Single(x => x.Length is oneLength).OrderBy(x => x).ToArray();
		char[] two;     // done
		char[] three;   // done
		char[] four = definition.Single(x => x.Length is fourLength).OrderBy(x => x).ToArray();
		char[] five;    // done
		char[] six;     // done
		char[] seven = definition.Single(x => x.Length is sevenLength).OrderBy(x => x).ToArray();
		char[] eight = definition.Single(x => x.Length is eightLength).OrderBy(x => x).ToArray();
		char[] nine; //eight without bottomleft.

		var panelUses = definition.SelectMany(x => x.ToArray()).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
		var bottomLeftUses = 4;
		var topLeftUses = 6;
		var bottomRightUses = 9;


		var top = seven.Except(one).Single();
		var bottomLeft = panelUses.Keys.FirstOrDefault(x => panelUses[x] == bottomLeftUses);
		var topLeft = panelUses.Keys.FirstOrDefault(x => panelUses[x] == topLeftUses);
		var bottomRight = panelUses.Keys.FirstOrDefault(x => panelUses[x] == bottomRightUses);
		var topRight = one.Except(new char[] { bottomRight }).Single();

		two = eight.Except(new char[] { topLeft, bottomRight }).OrderBy(x => x).ToArray();
		three = eight.Except(new char[] { bottomLeft, topLeft }).OrderBy(x => x).ToArray();
		five = eight.Except(new char[] { bottomLeft, topRight }).OrderBy(x => x).ToArray();
		six = eight.Except(new char[] { topRight }).OrderBy(x => x).ToArray();
		nine = eight.Except(new char[] { bottomLeft }).OrderBy(x => x).ToArray();

		var oneIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(one)));
		var twoIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(two)));
		var threeIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(three)));
		var fourIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(four)));
		var fiveIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(five)));
		var sixIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(six)));
		var sevenIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(seven)));
		var eightIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(eight)));
		var nineIndex = orderedDefinitions.IndexOf(orderedDefinitions.Single(x => x.SequenceEqual(nine)));

		var zeroIndex = Enumerable.Range(0, 10).Except(new int[] { oneIndex, twoIndex, threeIndex, fourIndex, fiveIndex, sixIndex, sevenIndex, eightIndex, nineIndex }).Single();
		var lookup = new Dictionary<string, int>
		{
			[definition[zeroIndex]] = 0,
			[definition[oneIndex]] = 1,
			[definition[twoIndex]] = 2,
			[definition[threeIndex]] = 3,
			[definition[fourIndex]] = 4,
			[definition[fiveIndex]] = 5,
			[definition[sixIndex]] = 6,
			[definition[sevenIndex]] = 7,
			[definition[eightIndex]] = 8,
			[definition[nineIndex]] = 9,
		};

		var resultString = outputValue
			.Select(x => x.OrderBy(y => y).ToArray())
			.Select(x => orderedDefinitions.IndexOf(orderedDefinitions.Single(y => y.SequenceEqual(x))))
			.Select(x => definition[x])
			.Select(x => lookup[x])
			.Aggregate(new StringBuilder(), (acc, next) => acc.Append(next))
			.ToString();

		int result = int.Parse(resultString);
		return result;
	}
}