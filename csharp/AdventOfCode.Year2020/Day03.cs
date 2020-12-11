using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode.Year2020
{
	/// <Summary>
	/// https://adventofcode.com/2020/day/3
	/// </Summary>
	public class Day03 : BaseDay<string, ulong>, IDay
	{
		public override ulong Part1(string input)
		{
			var map = new LoopingMap(input);
			return (ulong)TraverseMap(map, 3, 1);
		}

		public override ulong Part2(string input)
		{
			var traversals = new List<(int x, int y)>
			{
			  (1,1),
			  (3,1),
			  (5,1),
			  (7,1),
			  (1,2)
			};
			var map = new LoopingMap(input);
			var result = traversals
			  .Select((slopes, y) =>
			  {
				  var treesCutDown = TraverseMap(map, slopes.x, slopes.y);
				  return treesCutDown;
			  })
			  .Aggregate(1Lu, (acc, current) =>
			  {
				  var cast = (ulong)current;
				  return acc * (cast);
			  });

			return result;
		}

		public int TraverseMap(LoopingMap map, int xSlope, int ySlope)
		{
			var x = 0;
			var y = 0;
			var cutdownTrees = 0;
			do
			{
				y += ySlope;
				x += xSlope;
				var currentTile = map.GetPosition(x, y);
				if (currentTile == PositionInformation.Tree)
				{
					cutdownTrees++;
				}
			}
			while (y < map.Height - 1);
			Console.WriteLine($"cut down tree, {cutdownTrees}");
			return cutdownTrees;
		}
	}

}
