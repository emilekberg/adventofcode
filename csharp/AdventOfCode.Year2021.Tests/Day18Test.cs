using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day18Test
{
	string _input = "target area: x=20..30, y=-10..-5";
	[Fact]
	public void Part1()
	{
		var expected =45;
		var day = new Day18();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 112;
		var day = new Day18();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
