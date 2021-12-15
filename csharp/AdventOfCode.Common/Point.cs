using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common;
public record Point(int X, int Y)
{
	public static double Distance(Point lhs, Point rhs)
	{
		return Math.Sqrt(DistanceSquared(lhs, rhs));
	}
	public static double DistanceSquared(Point lhs, Point rhs)
	{
		var dX = rhs.X - lhs.X;
		var dY = rhs.Y - lhs.Y;
		return dX * dX + dY * dY;
	}
	public static int ManhattanDistance(Point lhs, Point rhs)
	{
		var dX = rhs.X - lhs.X;
		var dY = rhs.Y - lhs.Y;
		var result = Math.Abs(dX) + Math.Abs(dY);
		return result;
	}
}
