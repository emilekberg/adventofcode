using AdventOfCode.Common;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day07 : BaseDay<string, long>, IDay
{
	public override long Part1(string input)
	{
		return Process(input, GetRequiredFuel);
	}
	public override long Part2(string input)
	{
		return Process(input, GetRequirerdFuelIncreased);
	}
	public int Process(string input, Func<int[], int, int> getFuelFunction)
	{
		var positions = input.Split(',').Select(int.Parse).ToArray();
		int lowestAmountOfFuelUsed = int.MaxValue;
		for (int i = positions.Min(); i < positions.Max(); i++)
		{
			var value = getFuelFunction(positions, i);
			if (value < lowestAmountOfFuelUsed)
			{
				lowestAmountOfFuelUsed = value;
			}
		}
		return lowestAmountOfFuelUsed;
	}
	public static int GetRequiredFuel(int[] positions, int desiredPosition)
	{
		return positions.Sum(position => Math.Abs(position - desiredPosition));
	}
	public static Dictionary<int, int> Cache = new();
	public static int GetRequirerdFuelIncreased(int[] positions, int desiredPosition)
	{
		return positions.Sum(position =>
		{
			var numSteps = Math.Abs(position - desiredPosition);
			if(!Cache.TryGetValue(numSteps, out var value))
			{
				value = CalculateRequredFuel(numSteps);
				Cache.Add(numSteps, value);
			}
			return value;
		});
	}

	public static int CalculateRequredFuel(int numberOfSteps)
	{
		int usedFuel = 0;
		for (int i = 1; i <= numberOfSteps; i++)
		{
			usedFuel += i;
		}
		return usedFuel;
	}
}