using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day22Tests
	{
		[Fact]
		public void Part1()
		{
			var input = new StringBuilder()
				.AppendLine("Player 1:")
				.AppendLine("9")
				.AppendLine("2")
				.AppendLine("6")
				.AppendLine("3")
				.AppendLine("1")
				.AppendLine("")
				.AppendLine("Player 2:")
				.AppendLine("5")
				.AppendLine("8")
				.AppendLine("4")
				.AppendLine("7")
				.AppendLine("10")
				.ToString();

			var day = new Day22();
			var actual = day.Part1(input);
			var expected = 306;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2()
		{
			var input = new StringBuilder()
				.AppendLine("Player 1:")
				.AppendLine("9")
				.AppendLine("2")
				.AppendLine("6")
				.AppendLine("3")
				.AppendLine("1")
				.AppendLine("")
				.AppendLine("Player 2:")
				.AppendLine("5")
				.AppendLine("8")
				.AppendLine("4")
				.AppendLine("7")
				.AppendLine("10")
				.ToString();

			var day = new Day22();
			var actual = day.Part2(input);
			var expected = 291;
			Assert.Equal(expected, actual);
		}
	}
}
