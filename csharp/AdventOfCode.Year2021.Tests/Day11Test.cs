using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day11Test
{
	public string[] _input = new string[]
	{
		"5483143223",
		"2745854711",
		"5264556173",
		"6141336146",
		"6357385478",
		"4167524645",
		"2176841721",
		"6882881134",
		"4846848554",
		"5283751526"
	};
	[Theory]
	[InlineData(1, 0)]
	[InlineData(2, 35)]
	[InlineData(10, 204)]
	[InlineData(100, 1656)]
	public void Part1(int numSteps, int expected)
	{
		var day = new Day11();
		var actual = Day11.Part1(_input, numSteps);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part1_2_steps()
	{
		var input = new string[]
		{
			"11111",
			"19991",
			"19191",
			"19991",
			"11111"
		};
		var expected = 9;
		var day = new Day11();
		var actual = Day11.Part1(input, 2);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 195;
		var day = new Day11();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void GetAdjacent_begin()
	{
		var expected = new List<(int x, int y)>
		{
			(1, 0),
			(0, 1), (1, 1)
		};
		var actual = Day11.GetAdjacent(0, 0, 10, 10);
		Assert.Equal(expected.Count, actual.Count);
		Assert.All(actual, x => expected.Contains(x));
	}

	[Fact]
	public void GetAdjacent_end()
	{
		var expected = new List<(int x, int y)>
		{
			(8, 8), (9, 8),
			(8, 9)
		};
		var actual = Day11.GetAdjacent(9, 9, 10, 10);
		Assert.Equal(expected.Count, actual.Count);
		Assert.All(actual, x => expected.Contains(x));
	}

	[Fact]
	public void GetAdjacent_mid()
	{
		var expected = new List<(int x, int y)>
		{
			(4, 4), (5, 4), (6, 4),
			(4, 5),			(6, 5),
			(4, 6), (5, 6), (6, 6),
		};
		var actual = Day11.GetAdjacent(5, 5, 10, 10);
		Assert.Equal(expected.Count, actual.Count);
		Assert.All(actual, x => expected.Contains(x));
	}
}
