using AdventOfCode.Common;
using AdventOfCode.Common.Grid;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/15
/// </Summary> 
public class Day15 : BaseDay<string[], long>, IDay
{
	public static List<AStarNode> Parse(string[] input)
	{
		var nodes = new List<AStarNode>();
		for (int y = 0; y < input.Length; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				var node = new AStarNode(new Point(x, y), (int)char.GetNumericValue(input[y][x]));
				nodes.Add(node);
			}
		}
		return nodes;
	}
	public static Dictionary<(int x, int y), AStarNode> BuildGrid(List<AStarNode> nodes, int multiplier, int width, int height)
	{
		var grid = new Dictionary<(int x, int y), AStarNode>();

		for (int row = 0; row < multiplier; row++)
		{
			for (int col = 0; col < multiplier; col++)
			{
				foreach (var node in nodes)
				{
					var cost = ((node.Cost - 1 + row + col) % 9) + 1;
					var x = node.Position.X + width * col;
					var y = node.Position.Y + height * row;
					var newNode = new AStarNode(new Point(x, y), cost);
					grid.Add((x, y), newNode); 
				}
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
		var nodes = Parse(input);
		var grid = BuildGrid(nodes, gridMultiplier, input[0].Length, input.Length);

		var end = grid.Values.MaxBy(x => (x.Position.X, x.Position.Y));
		var start = grid.Values.MinBy(x => (x.Position.X, x.Position.Y));
		var path = FindShortestPath(start, end, grid);

		Print(path, grid);

		return path.Skip(1).Sum(x => x.Cost);
	}

	public static void Print(List<AStarNode> path, Dictionary<(int x, int y), AStarNode> grid)
	{
		var max = grid.Values.MaxBy(x => (x.Position.X, x.Position.Y));
		var sb = new StringBuilder();
		sb.AppendLine();
		for (int row = 0; row < max.Position.Y; row++)
		{
			sb.AppendLine();
			for (int col = 0; col < max.Position.X; col++)
			{
				if (path.Find(x => x.Position.X == col && x.Position.Y == row) != null)
				{
					sb.Append('*');
				}
				else
				{
					sb.Append(grid[(col, row)].Cost); 
				}

			}
		}
		Console.WriteLine(sb.ToString());
	}
	public static List<AStarNode>? FindShortestPath(AStarNode start, AStarNode end, Dictionary<(int x, int y), AStarNode> nodes)
	{
		var h = (AStarNode current, AStarNode end) => Point.ManhattanDistance(current.Position, end.Position);
		var d = (AStarNode lhs, AStarNode rhs) => rhs.Cost;

		var openSet = new PriorityQueue<AStarNode, float>();
		openSet.Enqueue(start, start.Cost);
		var openHashSet = new HashSet<AStarNode>
		{ 
			start
		};

		var cameFrom = new Dictionary<AStarNode, AStarNode>();
		var gScore = new DefaultDictionary<AStarNode, float>(float.MaxValue)
		{
			[start] = 0,
		};
		while (openSet.TryDequeue(out var current, out var _))
		{
			openHashSet.Remove(current);
			if (current == end)
			{
				return ReconstructPath(cameFrom, current);
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

		return null;
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

public record AStarNode(Point Position, int Cost);
