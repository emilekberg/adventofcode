using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day23Tests
	{
		[Theory]
		[InlineData("389125467", 10, 92658374)]
		[InlineData("389125467", 100, 67384529)]
		public void Part1(string input, ulong numMoves, ulong expected)
		{
			var day = new Day23();
			var actual = day.Part1(input, numMoves);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("389125467", 149245887792UL)]
		public void Part2(string input, ulong expected)
		{
			var day = new Day23();
			var actual = day.Part2(input);
			Assert.Equal(expected, actual);
		}
	}
}
