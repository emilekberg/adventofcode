using AdventOfCode.Common;

namespace AdventOfCode.Year2021;

public enum RatingMode
{
	Oxygen,
	CO2
}

/// <Summary>
/// https://adventofcode.com/2021/day/3
/// </Summary> 
public class Day03 : BaseDay<string[], int>, IDay
{
	public (int zeroes, int ones) CountOnesAndZeroesInColumn(string[] input, int column)
	{
		int zeroes = 0, ones = 0;
		for (int row = 0; row < input.Length; row++)
		{

			if (input[row][column] == '0')
			{
				zeroes++;
			}
			else
			{
				ones++;
			}
		}
		return (zeroes, ones);
	}
	public override int Part1(string[] input)
	{
		int gammaRate;
		int epsilonRate;

		string gammaRateTemp = "";
		string epsilonRateTemp = "";

		var numberOfLetters = input[0].Length;
		for (int col = 0; col < numberOfLetters; col++)
		{
			var counts = CountOnesAndZeroesInColumn(input, col);
			if (counts.ones > counts.zeroes)
			{
				gammaRateTemp += '1';
				epsilonRateTemp += '0';
			}
			else
			{
				gammaRateTemp += '0';
				epsilonRateTemp += '1';
			}
		}
		gammaRate = Convert.ToInt16(gammaRateTemp, 2);
		epsilonRate = Convert.ToInt16(epsilonRateTemp, 2);
		return gammaRate * epsilonRate;
	}

	public override int Part2(string[] input)
	{
		var oxygenGeneratorRatingString = GetOxygenGeneratorRating(input, RatingMode.Oxygen);
		var co2ScrubberRatingString = GetOxygenGeneratorRating(input, RatingMode.CO2);

		var oxygenGeneratorRating = Convert.ToInt16(oxygenGeneratorRatingString, 2);
		var co2ScrubberRating = Convert.ToInt16(co2ScrubberRatingString, 2);
		return oxygenGeneratorRating * co2ScrubberRating;
	}

	public string GetOxygenGeneratorRating(string[] input, RatingMode mode)
	{
		var result = "";
		var numberOfLetters = input[0].Length;
		for (int col = 0; col < numberOfLetters; col++)
		{
			var counts = CountOnesAndZeroesInColumn(input, col);
			char numberToKeep = '0';
			if (counts.zeroes <= counts.ones)
			{
				numberToKeep = mode is RatingMode.Oxygen ? '1' : '0';
			}
			else if (counts.zeroes > counts.ones)
			{
				numberToKeep = mode is RatingMode.Oxygen ? '0' : '1';
			}
			input = input.Where(x => x[col] == numberToKeep).ToArray();
			if (input.Length is 1)
			{
				return input[0];
			}
		}

		return result;
	}
}
