using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Year2021;

public record AStarNode(Point Position, int Cost);
/// <Summary>
/// https://adventofcode.com/2021/day/15
/// </Summary> 
public class Day15 : BaseDay<string[], long>, IDay
{
	public static Dictionary<(int x, int y), AStarNode> Parse(string[] input, int multiplier)
	{
		var baseWidth = input[0].Length;
		var baseHeight = input.Length;
		var grid = new Dictionary<(int x, int y), AStarNode>();
		for (int y = 0; y < baseHeight * multiplier; y++)
		{
			for (int x = 0; x < baseWidth * multiplier; x++)
			{
				var baseCost = (int)char.GetNumericValue(input[y % baseHeight][x % baseWidth]);
				int xCostAdd = x / baseWidth;
				int yCostAdd = y / baseHeight;
				// wraps around 1 and 9.
				var cost = ((baseCost - 1 + xCostAdd + yCostAdd) % 9) + 1;
				var xPos = x;
				var yPos = y;


				var node = new AStarNode(new Point(xPos, yPos), cost);
				grid.Add((x, y), node);
			}
		}
		return grid;
	}
	public override long Part1(string[] input)
	{
		return Solve(input, 1);
	}
	public override long Part2(string[] input)
	{
		return Solve(input, 5);
	}

	public static long Solve(string[] input, int gridMultiplier)
	{
		var grid = Parse(input, gridMultiplier);

		var end = grid.Values.MaxBy(x => (x.Position.X, x.Position.Y));
		var start = grid.Values.MinBy(x => (x.Position.X, x.Position.Y));
		var path = FindShortestPath(start, end, grid);

		return path;
	}
	public static int FindShortestPath(AStarNode start, AStarNode end, Dictionary<(int x, int y), AStarNode> nodes)
	{
		var h = (AStarNode current, AStarNode end) => Point.ManhattanDistance(current.Position, end.Position);
		var d = (AStarNode lhs, AStarNode rhs) => rhs.Cost;

		var openSet = new PriorityQueue<AStarNode, int>();
		openSet.Enqueue(start, start.Cost);
		var openHashSet = new HashSet<AStarNode>
		{ 
			start
		};

		var cameFrom = new Dictionary<AStarNode, AStarNode>();
		var gScore = new DefaultDictionary<AStarNode, int>(int.MaxValue)
		{
			[start] = 0,
		};
		while (openSet.TryDequeue(out var current, out var cost))
		{
			openHashSet.Remove(current);
			if (current == end)
			{
				// for this task we only want the cost of the path to the last node.
				// to get full path, return ReconstructPath(cameFrom, current); 
				return cost;
			}

			foreach (var neighbour in GetNeighbours(nodes, (current.Position.X, current.Position.Y)))
			{
				var tenativeGScore = gScore[current] + d(current, neighbour);
				if (tenativeGScore < gScore[neighbour])
				{
					cameFrom[neighbour] = current;
					gScore[neighbour] = tenativeGScore;
					
					var neighbourFScore = tenativeGScore + h(neighbour, end);
					if (!openHashSet.Contains(neighbour))
					{
						openHashSet.Add(neighbour);
						openSet.Enqueue(neighbour, neighbourFScore);
					}
				}
			}
		}

		throw new Exception("Could not find path");
	}

	public static List<AStarNode> GetNeighbours(Dictionary<(int x, int y), AStarNode> nodes, (int x, int y) position)
	{
		var neighbours = GridHelper
			.GetAdjacentPositions()
			.Select(neighbourPosition =>
			{
				if (!nodes.TryGetValue((neighbourPosition.x + position.x, neighbourPosition.y + position.y), out var node))
				{
					return null;
				}
				return node;
			})
			.Where(x => x is not null)
			.Cast<AStarNode>()
			.ToList();
		return neighbours;

	}

	public static List<AStarNode> ReconstructPath(Dictionary<AStarNode, AStarNode> cameFrom, AStarNode current)
	{
		var result = new List<AStarNode>
		{
			current
		};
		foreach (var _ in cameFrom)
		{
			if(!cameFrom.TryGetValue(current, out current))
			{
				break;
			}
			result.Add(current);
		}
		result.Reverse();
		return result;
	}
}


