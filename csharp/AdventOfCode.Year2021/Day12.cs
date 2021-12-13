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
		Console.WriteLine("part 1");
		Console.WriteLine("");
		var paths = ParseInput(input);
		var unique = paths.Values.SelectMany(x => x).Distinct().ToList();
		var passed = unique.ToDictionary(x => x, _ => 0);
		passed["start"]++;
		var count = 0;
		foreach(var node in paths["start"])
		{
			count += Traverse(node, paths, new Dictionary<string, int>(passed));
		}
		return count;
	}
	public override long Part2(string[] input)
	{
		Console.WriteLine("");
		Console.WriteLine("");
		Console.WriteLine("part 1");
		Console.WriteLine("");
		var paths = ParseInput(input);
		var unique = paths.Values.SelectMany(x => x).Distinct().ToList();
		var passed = unique.ToDictionary(x => x, _ => 0);
		

		var count = 0;
		foreach (var node in paths["start"])
		{
			count += Traverse2(node, paths, new List<string> { "start" });
		}
		return count;
	}

	public static int Traverse(string node, Dictionary<string, List<string>> paths, Dictionary<string, int> passed)
	{
		passed[node]++;
		if (node == "end")
		{
			Console.WriteLine(string.Join(',', passed));
			return 1;
		}
		var count = 0;
		var possiblePaths = paths[node]
			.Where(x => IsBigCave(x) || passed[x] == 0)
			.ToList();
		
		foreach (var path in possiblePaths)
		{
			var passedCopy = new Dictionary<string, int>(passed);
			count += Traverse(path, paths, passedCopy);
		}
		return count;
	}
	public static int Traverse2(string node, Dictionary<string, List<string>> paths, List<string> passed)
	{
		passed.Add(node);
		if (node == "end")
		{
			Console.WriteLine(string.Join(',', passed));
			return 1;
		}
		var passedGroups = passed
			.Where(x => x != "start" || x != "end")
			.GroupBy(x => x)
			.ToDictionary(x => x.Key, x => x.Count());
		var passedSmallCaveTwice = passedGroups.Any(group => IsSmallCave(group.Key) && group.Value == 2);

		var count = 0;
		var possiblePaths = paths[node]
			.Where(x => x != "start")
			.Where(x => IsBigCave(x) || !(passedGroups.ContainsKey(x)) || (passedSmallCaveTwice ? passedGroups[x] == 0 : passedGroups[x] <= 2))
			.ToList();

		foreach (var path in possiblePaths)
		{
			var passedCopy = passed.ToList();
			count += Traverse2(path, paths, passedCopy);
		}
		return count;
	}
	public static bool IsBigCave(string s) => s[0] < 'a';
	public static bool IsSmallCave(string s) => s[0] >= 'a';
}