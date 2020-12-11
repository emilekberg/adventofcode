using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day11Tests
	{
		[Fact]
		public void Part1()
		{


			var input = new StringBuilder()
				.AppendLine("L.LL.LL.LL")
				.AppendLine("LLLLLLL.LL")
				.AppendLine("L.L.L..L..")
				.AppendLine("LLLL.LL.LL")
				.AppendLine("L.LL.LL.LL")
				.AppendLine("L.LLLLL.LL")
				.AppendLine("..L.L.....")
				.AppendLine("LLLLLLLLLL")
				.AppendLine("L.LLLLLL.L")
				.AppendLine("L.LLLLL.LL")
				.ToString()
				.Trim()
				.Split(Environment.NewLine);

			var day = new Day11();
			var expected = 37;
			var actual = day.Part1(input);

			Assert.Equal(expected, actual);

		}

		[Fact]
		public void Part2()
		{
			var input = new StringBuilder()
				.AppendLine("L.LL.LL.LL")
				.AppendLine("LLLLLLL.LL")
				.AppendLine("L.L.L..L..")
				.AppendLine("LLLL.LL.LL")
				.AppendLine("L.LL.LL.LL")
				.AppendLine("L.LLLLL.LL")
				.AppendLine("..L.L.....")
				.AppendLine("LLLLLLLLLL")
				.AppendLine("L.LLLLLL.L")
				.AppendLine("L.LLLLL.LL")
				.ToString()
				.Trim()
				.Split(Environment.NewLine);

			var day = new Day11();
			var expected = 26;
			var actual = day.Part2(input);

			Assert.Equal(expected, actual);
		}


		public static IEnumerable<object[]> Part2_LineOfsightData =>
			new List<object[]>
			{
						new object[] { new string[] {
							".......#.", 
							"...#.....",
							".#.......",
							".........",
							"..#L....#",
							"....#....",
							".........",
							"#........",
							"...#....."},3,4, 8, 0 },

						new object[] { new string[] {
							".............",
							".L.L.#.#.#.#.",
							"............." } , 1, 1, 0, 1 },

						new object[] { new string[] {
							".##.##.",
							"#.#.#.#",
							"##...##",
							"...L...",
							"##...##",
							"#.#.#.#",
							".##.##." }, 3, 3, 0, 0 }

		};
		[Theory]
		[MemberData(nameof(Part2_LineOfsightData))]
		public void NumberOfOccupantSeatsInLineOfSight(string[] input, int startX, int startY, int expectedOccupied, int expectedEmpty)
		{
			/*var input = new StringBuilder()
				.AppendLine(".......#.")
				.AppendLine("...#.....")
				.AppendLine(".#.......")
				.AppendLine(".........")
				.AppendLine("..#L....#")
				.AppendLine("....#....")
				.AppendLine(".........")
				.AppendLine("#........")
				.AppendLine("...#.....")
				.ToString()
				.Trim()
				.Split(Environment.NewLine)
				.Select(x => x.ToCharArray().ToList()).ToList();
			*/
			var data = input.Select(x => x.ToCharArray().ToList()).ToList();
			var day = new Day11();
			var actual = day.NumberOfOccupantSeatsInLineOfSight(data, startX, startY);

			Assert.Equal(expectedOccupied, actual.occupied);
			Assert.Equal(expectedEmpty, actual.empty);
		}
	}
}
