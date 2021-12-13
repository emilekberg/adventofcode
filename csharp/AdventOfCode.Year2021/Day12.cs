using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day12 : BaseDay<string[], long>, IDay
{
	public Dictionary<string, List<string>> ParseInput(string[] input)
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
		var paths = ParseInput(input);

		var count = 0;
		foreach(var node in paths["start"])
		{
			count += Traverse(node, paths, new List<string> { "start" });
		}

		return count;
	}
	public override long Part2(string[] input)
	{
		var paths = ParseInput(input);

		var count = 0;
		foreach (var node in paths["start"])
		{
			count += Traverse(node, paths, new List<string> { "start" });
		}

		return count;
	}

	public static int Traverse(string node, Dictionary<string, List<string>> paths, List<string> passed)
	{
		passed.Add(node);
		if (node == "end")
		{
			Console.WriteLine(string.Join(',', passed));
			return 1;
		}
		var count = 0;
		var possiblePaths = paths[node]
			.Where(x => IsBigCave(x) || !passed.Contains(x))
			.ToList();
		
		foreach (var path in possiblePaths)
		{
			var set = passed.ToList();
			count += Traverse(path, paths, set);
		}
		return count;
	}
	public static bool IsBigCave(string s) => s[0] < 'a';
	public static bool IsSmallCave(string s) => s[0] >= 'a';
}