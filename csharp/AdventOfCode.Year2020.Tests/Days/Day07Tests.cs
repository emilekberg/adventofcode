using AdventOfCode.Year2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	public class Day07Tests
	{
		[Fact]
		public void Part1()
		{
			var input = "light red bags contain 1 bright white bag, 2 muted yellow bags." + "\n"
						+ "dark orange bags contain 3 bright white bags, 4 muted yellow bags." + "\n"
						+ "bright white bags contain 1 shiny gold bag." + "\n"
						+ "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags." + "\n"
						+ "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags." + "\n"
						+ "dark olive bags contain 3 faded blue bags, 4 dotted black bags." + "\n"
						+ "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags." + "\n"
						+ "faded blue bags contain no other bags." + "\n"
						+ "dotted black bags contain no other bags.";

			var day = new Day07();
			var actual = day.Part1(input);
			var expected = 4;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2()
		{
			var input = "shiny gold bags contain 2 dark red bags." + "\n"
						+ "dark red bags contain 2 dark orange bags." + "\n"
						+ "dark orange bags contain 2 dark yellow bags." + "\n"
						+ "dark yellow bags contain 2 dark green bags." + "\n"
						+ "dark green bags contain 2 dark blue bags." + "\n"
						+ "dark blue bags contain 2 dark violet bags." + "\n"
						+ "dark violet bags contain no other bags.";

			var day = new Day07();
			var actual = day.Part2(input);
			var expected = 126;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2_2()
		{
			var input = "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags." + "\n"
						+ "faded blue bags contain no other bags." + "\n"
						+ "dotted black bags contain no other bags." + "\n"
						+ "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags." + "\n"
						+ "dark olive bags contain 3 faded blue bags, 4 dotted black bags.";

			var day = new Day07();
			var actual = day.Part2(input);
			var expected = 32;

			Assert.Equal(expected, actual);
		}
	}
}
