using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day03Test
{
	public string[] _input = new string[] {
		"00100",
		"11110",
		"10110",
		"10111",
		"10101",
		"01111",
		"00111",
		"11100",
		"10000",
		"11001",
		"00010",
		"01010"
	};
	[Fact]
	public void Part1()
	{
		var expected = 198;
		var day = new Day03();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 230;
		var day = new Day03();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
