using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{

	public class Day24Tests
	{
		private readonly static string[] TestInput = new string[]
		{
			"sesenwnenenewseeswwswswwnenewsewsw",
			"neeenesenwnwwswnenewnwwsewnenwseswesw",
			"seswneswswsenwwnwse",
			"nwnwneseeswswnenewneswwnewseswneseene",
			"swweswneswnenwsewnwneneseenw",
			"eesenwseswswnenwswnwnwsewwnwsene",
			"sewnenenenesenwsewnenwwwse",
			"wenwwweseeeweswwwnwwe",
			"wsweesenenewnwwnwsenewsenwwsesesenwne",
			"neeswseenwwswnwswswnw",
			"nenwswwsewswnenenewsenwsenwnesesenew",
			"enewnwewneswsewnwswenweswnenwsenwsw",
			"sweneswneswneneenwnewenewwneswswnese",
			"swwesenesewenwneswnwwneseswwne",
			"enesenwswwswneneswsenwnewswseenwsese",
			"wnwnesenesenenwwnenwsewesewsesesew",
			"nenewswnwewswnenesenwnesewesw",
			"eneswnwswnwsenenwnwnwwseeswneewsenese",
			"neswnwewnwnwseenwseesewsenwsweewe",
			"wseweeenwnesenwwwswnew"
		};
		[Fact]
		public void Part1_FlipsReferenceTile()
		{
			var input = new string[] { "nwwswee" };
			var day = new Day24();

			var expected = 1;
			var actual = day.Part1(input);
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void Part1()
		{
			var day = new Day24();
			var expected = 10;
			var actual = day.Part1(TestInput);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(1, 15)]
		[InlineData(2, 12)]
		[InlineData(3, 25)]
		[InlineData(4, 14)]
		[InlineData(5, 23)]
		[InlineData(6, 28)]
		[InlineData(7, 41)]
		[InlineData(8, 37)]
		[InlineData(9, 49)]
		[InlineData(10, 37)]
		[InlineData(20, 132)]
		[InlineData(30, 259)]
		[InlineData(40, 406)]
		[InlineData(50, 566)]
		[InlineData(60, 788)]
		[InlineData(70, 1106)]
		[InlineData(80, 1373)]
		[InlineData(90, 1844)]
		[InlineData(100, 2208)]
		public void Part2(int numDays, int expected)
		{
			var day = new Day24();
			var actual = day.Part2(TestInput, numDays);
			Assert.Equal(expected, actual);
		}
	}
}
