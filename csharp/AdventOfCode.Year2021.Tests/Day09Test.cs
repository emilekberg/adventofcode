using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day09Test
{
	public string[] _input = new string[]
	{
		"2199943210",
		"3987894921",
		"9856789892",
		"8767896789",
		"9899965678"
	};
	[Fact]
	public void Part1()
	{
		var expected = 15;
		var day = new Day09();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 1134;
		var day = new Day09();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
