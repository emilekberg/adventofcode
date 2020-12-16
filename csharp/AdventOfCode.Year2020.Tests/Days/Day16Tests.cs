using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day16Tests
	{
		[Fact]
		public void Part1()
		{
			var input = new StringBuilder()
				.AppendLine("class: 1-3 or 5-7")
				.AppendLine("row: 6-11 or 33-44")
				.AppendLine("seat: 13-40 or 45-50")
				.AppendLine("")
				.AppendLine("your ticket:")
				.AppendLine("7,1,14")
				.AppendLine("")
				.AppendLine("nearby tickets:")
				.AppendLine("7,3,47")
				.AppendLine("40,4,50")
				.AppendLine("55,2,20")
				.AppendLine("38,6,12")
				.ToString();
			var day = new Day16();
			var actual = day.Part1(input);
			var expected = 71UL;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2()
		{
			var input = new StringBuilder()
				.AppendLine("class: 0-1 or 4-19")
				.AppendLine("row: 0-5 or 8-19")
				.AppendLine("seat: 0-13 or 16-19")
				.AppendLine("")
				.AppendLine("your ticket:")
				.AppendLine("11,12,13")
				.AppendLine("")
				.AppendLine("nearby tickets:")
				.AppendLine("3,9,18")
				.AppendLine("15,1,5")
				.AppendLine("5,14,9")
				.ToString();
			var day = new Day16();
			var actual = day.Part2(input, "a");
			var expected = 156UL;
			Assert.Equal(expected, actual);
		}
	}
}
