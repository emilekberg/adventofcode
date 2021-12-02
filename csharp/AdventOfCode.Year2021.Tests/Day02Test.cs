using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day02Test
{
	public string[] _input = new string[] {
		"forward 5",
		"down 5",
		"forward 8",
		"up 3",
		"down 8",
		"forward 2"
	};
	[Fact]
	public void Part1()
	{
		var expected = 150;
		var day = new Day02();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 900;
		var day = new Day02();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
