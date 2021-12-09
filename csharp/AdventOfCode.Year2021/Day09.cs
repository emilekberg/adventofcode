using AdventOfCode.Common;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day09 : BaseDay<string[], long>, IDay
{
	public override long Part1(string[] input)
	{
		var values = input.Select(x => x
			.ToCharArray()
			.Select(x => (int)char.GetNumericValue(x))
			.ToArray()
		).ToArray();

		var result = 0;
		var points = GetDeepestPoints(values);

		result = points.Sum(point => values[point.y][point.x] + 1);

		return result;
	}
	public override long Part2(string[] input)
	{
		var values = input.Select(x => x
			.ToCharArray()
			.Select(x => (int)char.GetNumericValue(x))
			.ToArray()
		).ToArray();

		var points = GetDeepestPoints(values);


		var count = new List<int>();
		foreach(var (x, y) in points)
		{
			var visited = new HashSet<(int x, int y)>();
			visited.Add((x, y));
			TraverseBasin(values, x, y, visited);
			count.Add(visited.Count());
		}

		var result = points.Select(point =>
		{
			var visited = new HashSet<(int x, int y)>();
			visited.Add((point.x, point.y));
			TraverseBasin(values, point.x, point.y, visited);
			return visited.Count();
		}).OrderByDescending(x => x)
		.Take(3)
		.Aggregate(1, (acc, next) => acc *= next);
		return result;
	}
	public static void TraverseBasin(int[][] data, int x, int y, HashSet<(int x, int y)> visited)
	{
		var neighbours = GetNeighbours(data, x, y).Except(visited).ToList();

		for(int i = 0; i < neighbours.Count; i++)
		{
			var value = data[y][x];
			var neighbour = neighbours[i];
			var neighbourValue = data[neighbour.y][neighbour.x];
			if (neighbourValue > value && neighbourValue < 9)
			{
				visited.Add(neighbour);
			}
			if (neighbourValue != 9)
			{
				TraverseBasin(data, neighbour.x, neighbour.y, visited);
			}
		}
	}
	public static List<(int x, int y)> GetDeepestPoints(int[][] data)
	{
		var points = new List<(int x, int y)>();
		for (int y = 0; y < data.Length; y++)
		{
			for (int x = 0; x < data[y].Length; x++)
			{
				if (TraverseNeighbours(data, x, y, (a,b) => a <= b, out var point))
				{
					points.Add(point);
				}
			}
		}
		return points;
	}
	public static bool TraverseNeighbours(int[][] data, int x, int y, Func<int,int, bool> checkFunction, out (int x, int y) points)
	{
		var value = data[y][x];
		points = (0, 0);

		if(GetNeighbours(data, x, y).Any(point => checkFunction(data[point.y][point.x], value)))
		{
			return false;
		}
		points = (x, y);
		return true;
	}
	public static List<(int x, int y)> GetNeighbours(int[][] data, int x, int y)
	{
		var result = new List<(int x, int y)>();
		if (y > 0)
		{
			result.Add((x, y - 1));
		}
		if (y < data.Length - 1)
		{
			result.Add((x, y + 1));
		}
		if (x > 0)
		{
			result.Add((x - 1, y));
		}
		if (x < data[y].Length - 1)
		{
			result.Add((x + 1, y));
		}
		return result;
	}
}