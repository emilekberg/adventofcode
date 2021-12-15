using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common.Grid;

public static class GridHelper
{
	public static void PrintToConsole(int[][] grid)
	{
		var height = grid.Length;
		var width = grid[0].Length;
		var sb = new StringBuilder();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				sb.Append(grid[y][x]);
			}
			sb.AppendLine();
		}
		Console.WriteLine(sb.ToString());
	}
	public static List<(int x, int y)> GetAdjacentPositionsWithDiagonals()
	{
		var result = new List<(int x, int y)>
		{
			(-1, -1), (0, -1), (1, -1),
			(-1, 0),           (1, 0),
			(-1, 1),  (0, 1),  (1, 1)
		};
		return result;
	}
	public static List<(int x, int y)> GetAdjacentPositions()
	{
		var result = new List<(int x, int y)>
		{
			(0, -1),(-1, 0),(1, 0),	(0, 1)
		};
		return result;
	}
}
