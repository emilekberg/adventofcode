using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day05Tests
	{
		[Theory]
		[InlineData("FBFBBFFRLR", 44, 5, 357)]
		[InlineData("BFFFBBFRRR", 70, 7, 567)]
		[InlineData("FFFBBBFRRR", 14, 7, 119)]
		[InlineData("BBFFBBFRLL", 102, 4, 820)]
		public void TraverseBSP(string input, int expectedRow, int expectedColumn, int expectedSeatId)
		{
			var day = new Day05();
			var actual = day.TraverseBSP(input);

			Assert.Equal(expectedRow, actual.Row);
			Assert.Equal(expectedColumn, actual.Column);
			Assert.Equal(expectedSeatId, actual.SeatId);
		}
	}
}
