using AdventOfCode.Common;
using System.Text;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/7
/// </Summary> 
public class Day10 : BaseDay<string[], long>, IDay
{
	public override long Part1(string[] input)
	{
		var result = input
			.Select(x => ProcessRow(x))
			.Where(x => x.score != -1)
			.ToList();

		return result.Sum(x => x.score);
	}
	public override long Part2(string[] input)
	{
		var result = input
			.Select(x => ProcessRow(x))
			.Where(x => x.score == -1)
			.Select(x => x.remaining.Select(GetCloseVariant).ToList())
			.Select(x => AggregateScoreForIncompleteRow(x))
			.OrderByDescending(x => x).ToList();
		var middle = result.Count * 0.5;
		return result[(int)middle];
	}
	public static long AggregateScoreForIncompleteRow(IEnumerable<char> characters)
	{
		return characters.Aggregate(0L, (acc, next) =>
		{
			return (acc * 5L) + GetScoreForIncompleteRows(next);
		});
	}
	public static (long score, List<char> remaining) ProcessRow(string row)
	{
		var stack = new Stack<char>();
		foreach (var c in row.ToCharArray())
		{
			if (IsOpen(c))
			{
				stack.Push(c);
			}
			else if (IsClose(c))
			{
				if (c == GetCloseVariant(stack.Peek()))
				{
					stack.Pop();
				}
				else
				{
					return (GetScoreForCorruptedRows(c), stack.ToList());
				}
			}
		}
		return (-1, stack.ToList());
	}


	public static bool IsOpen(char c)
		=> c is '[' or '(' or '<' or '{';
	public static bool IsClose(char c)
		=> c is ']' or ')' or '>' or '}';
	public static char GetCloseVariant(char c) =>
		c switch
		{
			'[' => ']',
			'(' => ')',
			'{' => '}',
			'<' => '>',
			_ => throw new ArgumentException($"{c} is not valid opening", nameof(c))
		};
	public static long GetScoreForCorruptedRows(char c) =>
		c switch
		{
			')' => 3,
			']' => 57,
			'}' => 1197,
			'>' => 25137,
			_ => throw new ArgumentException("not valid opening", nameof(c))
		};
	public static long GetScoreForIncompleteRows(char c) =>
		c switch
		{
			')' => 1,
			']' => 2,
			'}' => 3,
			'>' => 4,
			_ => throw new ArgumentException("not valid opening", nameof(c))
		};
}