using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day01Test
{
	string[] _input = new string[] {
		"199",
		"200",
		"208",
		"210",
		"200",
		"207",
		"240",
		"269",
		"260",
		"263"
	};
	[Fact]
	public void Part1()
	{
		var expected = 7;

		var day = new Day01();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 5;
		var day = new Day01();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
