using AdventOfCode.Common;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/17
/// </Summary> 
public class Day17 : BaseDay<string, long>, IDay
{
	public static (int x1, int x2, int y1, int y2) Parse(string input)
	{
		var regex = new Regex(@"x=(-?\d+)\.\.(-?\d+), y=(-?\d+)\.\.(-?\d+)");
		var groups = regex.Match(input).Groups;

		var x1 = int.Parse(groups[1].Value);
		int x2 = int.Parse(groups[2].Value);
		var y1 = int.Parse(groups[3].Value);
		int y2 = int.Parse(groups[4].Value);

		return (x1, x2, y1, y2);
	}
	public override long Part1(string input)
	{
		var hitbox= Parse(input);
		var result = int.MinValue;
		for(int dx = 0; dx < 1000; dx++)
		{
			for(int dy = 0; dy < 1000; dy++)
			{
				if (TryHitBox(dx, dy, hitbox, out var maxY))
				{
					result = Math.Max(result, maxY);
				}
			}
		}

		return result;
	}
	public override long Part2(string input)
	{
		var hitbox = Parse(input);
		int counter = 0;
		for (int dx = -500; dx < 500; dx++)
		{
			for (int dy = -500; dy < 500; dy++)
			{
				if (TryHitBox(dx, dy, hitbox, out var _))
				{
					counter++;
				}
			}
		}

		return counter;
	}

	public static bool TryHitBox(int dx, int dy, (int x1, int x2, int y1, int y2) hitbox, out int maxY)
	{
		int x = 0;
		int y = 0;
		const int gravity = 1;
		var positions = new List<(int x, int y)>();
		while (true)
		{
			x += dx;
			y += dy;

			dx += -Math.Sign(dx);
			dy -= gravity;
			positions.Add((x, y));
			if (y < Math.Min(hitbox.y1, hitbox.y2) || x > hitbox.x2)
			{
				maxY = 0;
				return false;
			}
			else if (AABB(x, y, hitbox.x1, hitbox.x2, hitbox.y1, hitbox.y2))
			{

				maxY = positions.Max(position => position.y);
				return true;
			}

		}
	}

	public static bool AABB(int x, int y, int x1, int x2, int y1, int y2)
	{
		var xIsInTarget = x >= x1 && x <= x2;
		var yIsInTarget = y >= y1 && y <= y2;
		return xIsInTarget && yIsInTarget;
	}

}