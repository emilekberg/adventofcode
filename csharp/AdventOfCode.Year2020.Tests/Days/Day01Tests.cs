using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day01Tests
	{
		string[] Input = new []
		{
			"1721",
			"979",
			"366",
			"299",
			"675",
			"1456"
		};
		[Fact]
		public void Part1()
		{
			var day = new Day01();
			var actual = day.Part1(Input);
			var expected = 514579;
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void Part2()
		{
			var day = new Day01();
			var actual = day.Part2(Input);
			var expected = 241861950;
			Assert.Equal(expected, actual);
		}
	}
}
