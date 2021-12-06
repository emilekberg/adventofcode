using AdventOfCode.Common;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/6
/// </Summary> 
public class Day06 : BaseDay<string, long>, IDay
{
	public override long Part1(string input)
	{
		return LinkedList(input, 80);
	}
	public override long Part2(string input)
	{
		return LinkedList(input, 256);
	}
	public static long LinkedList(string input, int numberOfDays)
	{
		var lifetimes = input.Split(',').Select(int.Parse).ToList();
		var list = new LinkedList<long>();
		for(int i = 0; i <= 8; i++)
		{
			var count = lifetimes.Count(x => x == i);
			list.AddLast(count);
		}
		LinkedListNode<long> sixth = list.Last.Previous.Previous;
		for(int day = 0; day < numberOfDays; day++)
		{
			long fishesToAdd = list.First.Value;
			list.RemoveFirst();
			list.AddLast(fishesToAdd);
			sixth = sixth.Next;
			sixth.ValueRef += fishesToAdd;
		}
		var totalNumberOfFishes = list.Sum();
		return totalNumberOfFishes;
	}
}