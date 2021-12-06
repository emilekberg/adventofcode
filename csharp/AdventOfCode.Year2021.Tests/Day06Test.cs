using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day06Test
{
	public string _input = "3,4,3,1,2";
	[Fact]
	public void Part1()
	{
		var expected = 5934;
		var day = new Day06();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 26984457539;
		var day = new Day06();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
