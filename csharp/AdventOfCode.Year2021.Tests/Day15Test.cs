using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day15Test
{
	public string[] _input = new string[]
	{
		"1163751742",
		"1381373672",
		"2136511328",
		"3694931569",
		"7463417111",
		"1319128137",
		"1359912421",
		"3125421639",
		"1293138521",
		"2311944581"
	};
	[Fact]
	public void Part1()
	{
		var expected = 40;
		var day = new Day15();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 315;
		var day = new Day15();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
