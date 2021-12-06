using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/5
/// </Summary> 
public class Day05 : BaseDay<string[], int>, IDay
{
	public override int Part1(string[] input)
	{
		return Process(input, true);
	}

	public override int Part2(string[] input)
	{
		return Process(input, false);
	}

	public static int Process(string[] input, bool removeDiagonals)
	{
		var regex = new Regex(@"(\d+),(\d+)\s->\s(\d+),(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		var lines = input.Select(line =>
		{
			var groups = regex.Match(line).Groups;
			var x1 = int.Parse(groups[1].Value);
			var y1 = int.Parse(groups[2].Value);
			var x2 = int.Parse(groups[3].Value);
			var y2 = int.Parse(groups[4].Value);
			return (x1, y1, x2, y2);
		});
		if(removeDiagonals)
		{
			lines = lines.Where(line => line.x1 == line.x2 || line.y1 == line.y2);
		}
		var grid = new Dictionary<(int x, int y), int>();
		foreach (var (x1, y1, x2, y2) in lines)
		{
			var diffX = x2 - x1;
			var diffY = y2 - y1;
			var deltaX = Math.Sign(diffX);
			var deltaY = Math.Sign(diffY);

			var x = x1;
			var y = y1;
			var length = Math.Abs(diffX != 0 ? diffX : diffY);
			for(int i = 0; i <= length; i++)
			{
				var position = (x, y);
				if (!grid.TryGetValue(position, out var count)) count = 0;
				grid[position] = ++count;
				x += deltaX;
				y += deltaY;
			}
		}
		var numberOfOverlaps = grid.Values.Count(value => value > 1);
		return numberOfOverlaps;
	}
}