using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day14Tests
	{
		public static IEnumerable<object[]> Part1_InputData =>
			new List<object[]>
			{
				new object[] { 
					new string[] {
						"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
						"mem[8] = 11",
						"mem[7] = 101",
						"mem[8] = 0"
					}, 165UL },
				new object[] { 
					new string[] {
						"mask = 1XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
						"mem[0] = 1"
					}, Convert.ToUInt64("100000000000000000000000000000000001", 2) },
				new object[] { 
					new string[] {
						"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX1",
						"mem[0] = 0",
						"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX1X",
						"mem[1] = 0" 
					}, 3UL
				},
			};
		[Theory]
		[MemberData(nameof(Part1_InputData))]
		public void Part1(string[] input, ulong expected)
		{
			var day = new Day14();
			var actual = day.Part1(input);
			Assert.Equal(expected, actual);

		}

		[Fact]
		public void Part2()
		{
			var input = new string[]
			{
				"mask = 000000000000000000000000000000X1001X",
				"mem[42] = 100",
				"mask = 00000000000000000000000000000000X0XX",
				"mem[26] = 1"
			};
			var expected = 208UL;

			var day = new Day14();
			var actual = day.Part2(input);
			Assert.Equal(expected, actual);
		}
	}
}
