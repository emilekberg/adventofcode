using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/6
/// </Summary> 
public class Day06 : BaseDay<string, long>, IDay
{
	public override long Part1(string input)
	{
		var lifetimes = input.Split(',').Select(int.Parse).OrderBy(x => x).ToList();
		return LinkedList(lifetimes, 80);
		// return Naive(lifetimes, 80);
	}

	public long LinkedList(List<int> lifetimes, int numberOfDays)
	{
		var queue = new LinkedList<long>();
		LinkedListNode<long> sixth;
		for(int i = 0; i <= 8; i++)
		{
			var count = lifetimes.Count(x => x == i);
			queue.AddLast(count);
		}
		sixth = queue.Last.Previous.Previous;
		for(int day = 0; day < numberOfDays; day++)
		{
			long fishesToAdd = queue.First.Value;
			queue.RemoveFirst();
			queue.AddLast(fishesToAdd);
			sixth = sixth.Next;
			sixth.ValueRef += fishesToAdd;
		}
		var totalNumberOfFishes = queue.Sum();
		return totalNumberOfFishes;
	}
	public long Naive(List<long> lifetimes, int numberOfDays)
	{
		for (int day = 0; day < numberOfDays; day++)
		{
			int fishesToAdd = 0;
			for (int i = 0; i < lifetimes.Count; i++)
			{
				if (lifetimes[i] == 0)
				{
					lifetimes[i] = 6;
					fishesToAdd++;
				}
				else
				{
					lifetimes[i]--;
				}
			}
			lifetimes.AddRange(Enumerable.Repeat(8, fishesToAdd).Cast<long>());
		}
		return lifetimes.Count;
	}

	public override long Part2(string input)
	{
		var lifetimes = input.Split(',').Select(int.Parse).ToList();
		return LinkedList(lifetimes, 256);
	}
}