using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day05Test
{
	public string[] _input = new string[] {
		"0,9 -> 5,9",
		"8,0 -> 0,8",
		"9,4 -> 3,4",
		"2,2 -> 2,1",
		"7,0 -> 7,4",
		"6,4 -> 2,0",
		"0,9 -> 2,9",
		"3,4 -> 1,4",
		"0,0 -> 8,8",
		"5,5 -> 8,2"
	};
	[Fact]
	public void Part1()
	{
		var expected = 5;
		var day = new Day05();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 12;
		var day = new Day05();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
