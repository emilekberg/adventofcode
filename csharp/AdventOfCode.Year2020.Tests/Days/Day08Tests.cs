using AdventOfCode.Year2020.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day08Tests
	{
		[Fact]
		public void Part1()
		{
			var input = new string[]
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"jmp -4",
				"acc +6"
			};

			var day = new Day08();
			var expected = 5;
			var actual = day.Part1(input);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2()
		{
			var input = new string[]
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"jmp -4",
				"acc +6"
			};

			var day = new Day08();
			var expected = 8;
			var actual = day.Part2(input);

			Assert.Equal(expected, actual);
		}
	}
}
