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

		var result = 0;
		var points = GetDeepestPoints(values);


		var visited = new HashSet<(int x, int y)>();
		foreach(var (x, y) in points)
		{
			TraverseBasin(values, x, y);
		}


		// for each point traverse neighbours until you find a 9.


		return result;
	}
	public static void TraverseBasin(int[][] data, int x, int y)
	{
		var visited = new HashSet<(int x, int y)>();
		var value = data[y][x];
		var neighbours = GetNeighbours(data, x, y);
		var toVisit = new List<(int x, int y)>();
		foreach(var neighbour in neighbours)
		{
			var neighbourValue = data[neighbour.y][neighbour.x];
			if (neighbourValue > value && neighbourValue != 9 && !visited.Contains(neighbour))
			{
				toVisit.Add(neighbour);
				visited.Add(neighbour);
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