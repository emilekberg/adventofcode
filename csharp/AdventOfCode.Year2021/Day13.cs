using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/13
/// </Summary> 
public class Day13 : BaseDay<string[], long>, IDay
{
	public static (List<(int x, int y)> dots, List<(string axis, int amount)> instructions) Parse(string[] input)
	{
		var instructions = new List<(string axis, int amount)>();
		var dots = new List<(int x, int y)>();
		foreach(var line in input)
		{
			if (line.StartsWith("fold"))
			{
				var data = line.Replace("fold along ", string.Empty).Split('=');
				instructions.Add((data[0], int.Parse(data[1])));
			}
			else if(line.Contains(','))
			{
				var data = line.Split(',');
				dots.Add((int.Parse(data[0]), int.Parse(data[1])));
			}
		}
		return (dots, instructions);
	}
	public override long Part1(string[] input)
	{
		var (dots, instructions) = Parse(input);
		return FoldPaper(dots, instructions.Take(1)).Count;
	}
	public override long Part2(string[] input)
	{
		var (dots, instructions) = Parse(input);
		dots = FoldPaper(dots, instructions);
		Print(dots);
		return 0;
	}
	public static List<(int x, int y)> FoldPaper(List<(int x, int y)> dots, IEnumerable<(string axis, int amount)> instructions)
	{
		IEnumerable<(int x, int y)> enumerableDots = dots;
		foreach (var (axis, amount) in instructions)
		{
			enumerableDots = enumerableDots.Select(dot =>
			{
				if (axis == "x" && dot.x > amount)
				{
					dot.x = 2 * amount - dot.x;
				}
				if (axis == "y" && dot.y > amount)
				{
					dot.y = 2 * amount - dot.y;
				}
				return dot;
			}).Distinct();
		}
		return enumerableDots.ToList();
	}

	public static void Print(List<(int x, int y)> dots)
	{
		var width = dots.Max(dot => dot.x) + 1;
		var height = dots.Max(dot => dot.y) + 1;
		var lookup = dots.ToHashSet();
		var sb = new StringBuilder();
		for (int y = 0; y < height; y++)
		{
			sb.AppendLine();
			for (int x = 0; x < width; x++)
			{
				sb.Append(lookup.Contains((x, y)) ? "#" : " ");
			}
		}
		Console.WriteLine(sb.ToString());
	}
}