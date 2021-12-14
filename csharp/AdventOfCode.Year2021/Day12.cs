using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day12 : BaseDay<string[], long>, IDay
{
	public static Dictionary<string, List<string>> ParseInput(string[] input)
	{
		var nodes = input.Select(nodes => nodes.Split('-')).ToList();

		var uniqueNodes = nodes.SelectMany(x => x).Distinct().ToList();

		var paths = uniqueNodes.ToDictionary(node => node, node =>
		{
			var list = nodes.FindAll(x => x[0] == node)
				.Select(x => x[1])
				.ToList();

			var list2 = nodes.FindAll(x => x[1] == node)
				.Select(x => x[0])
				.ToList();
			list.AddRange(list2);
			return list;
		});
		return paths;
	}
	public override long Part1(string[] input)
	{
		return Process(input, false);
	}
	public override long Part2(string[] input)
	{
		return Process(input, true);
	}
	public static long Process(string[] input, bool allowPassSmallCaveTwice)
	{
		var paths = ParseInput(input);
		var unique = paths.Values.SelectMany(x => x).Distinct().ToList();
		var passed = unique.ToDictionary(x => x, _ => 0);


		var count = 0;
		foreach (var node in paths["start"])
		{
			var copy = new Dictionary<string, int>(passed);
			count += Traverse(node, paths, copy, allowPassSmallCaveTwice);
		}
		return count;
	}
	public static int Traverse(string node, Dictionary<string, List<string>> paths, Dictionary<string, int> passed, bool allowSmallCaveTwice)
	{
		passed[node]++;
		if (node == "end")
		{
			return 1;
		}
		var hasPassedSmallCaveTwice = passed.Any(group => IsSmallCave(group.Key) && group.Value == 2);
		var canPassSmallCaveTwice = allowSmallCaveTwice && !hasPassedSmallCaveTwice;

		var count = 0;
		var possiblePaths = paths[node]
			.Where(x => x != "start")
			.Where(x => IsBigCave(x) || (canPassSmallCaveTwice ? passed[x] <= 2 : passed[x] == 0))
			.ToList();

		foreach (var path in possiblePaths)
		{
			var passedCopy = new Dictionary<string, int>(passed);
			count += Traverse(path, paths, passedCopy, allowSmallCaveTwice);
		}
		return count;
	}
	public static bool IsBigCave(string s) => s[0] < 'a';
	public static bool IsSmallCave(string s) => s[0] >= 'a';
}