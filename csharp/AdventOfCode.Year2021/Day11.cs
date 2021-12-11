using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day11 : BaseDay<string[], long>, IDay
{
	public override long Part1(string[] input) => Part1(input, 100);
	public static long Part1(string[] input, int numSteps = 100)
	{
		var octopuses = input
			.Select(x => x
				.ToCharArray()
				.Select(y => (int)char.GetNumericValue(y))
				.ToArray()
			).ToArray();
		int numFlashes = 0;

		Console.WriteLine($"Before any steps:");
		GridHelper.PrintToConsole(octopuses);

		for (int step = 0; step < numSteps; step++)
		{
			numFlashes += Step(octopuses);

			Console.WriteLine($"After step {step + 1}:");
			GridHelper.PrintToConsole(octopuses);
		}
		return numFlashes;
	}
	public override long Part2(string[] input)
	{
		var octopuses = input
			.Select(x => x
				.ToCharArray()
				.Select(y => (int)char.GetNumericValue(y))
				.ToArray()
			).ToArray();
		int step = 0;
		do
		{
			step++;
			var numFlashes = Step(octopuses);
			if(numFlashes == octopuses[0].Length * octopuses.Length)
			{
				return step;
			}
		}
		while (true);
	}
	public static int Step(int[][] octopuses)
	{
		int count = 0;
		var height = octopuses.Length;
		var width = octopuses[0].Length; 
		var toFlash = new List<(int x, int y)>();

		// increase energy by 1
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (++octopuses[y][x] > 9)
				{
					toFlash.Add((x, y));
					count++;
				}
			}
		}

		for (int i = 0; i < toFlash.Count; i++)
		{
			var pos = (toFlash[i].x, toFlash[i].y);
			var adjacent = GetAdjacent(toFlash[i].x, toFlash[i].y, width, height);
			foreach (var adjacentPosition in adjacent)
			{
				// - increase adjacent octopuses with 1
				if (++octopuses[adjacentPosition.y][adjacentPosition.x] > 9 && !toFlash.Contains(adjacentPosition))
				{
					// - adjacent octopuses flashes if it's beyond 9
					// - only 1 flash per step
					toFlash.Add(adjacentPosition);
					count++;
				}
			}
		}


		// - reset energy to 0
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (octopuses[y][x] > 9)
				{
					octopuses[y][x] = 0;
				}
			}
		}
		return count;
	}
	public static List<(int x, int y)> GetAdjacent(int x, int y, int width, int height)
	{
		return GridHelper.GetRelativeAdjacentPositions()
			.Select(pos => (x + pos.x, y + pos.y))
			.Where(pos => pos.Item1 >= 0 && pos.Item1 < width && pos.Item2 >= 0 && pos.Item2 < height)
			.ToList();
	}
}