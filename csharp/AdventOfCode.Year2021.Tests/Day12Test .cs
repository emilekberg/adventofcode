using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day12Test
{
	public string[] _input = new string[]
	{
		"start-A",
		"start-b",
		"A-c",
		"A-b",
		"b-d",
		"A-end",
		"b-end"
	};
	[Fact]
	public void Part1()
	{
		var expected = 10;
		var day = new Day12();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 3509;
		var day = new Day12();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData("start", true)]
	[InlineData("end", true)]
	[InlineData("a", true)]
	[InlineData("c", true)]
	[InlineData("A", false)]
	[InlineData("C", false)]
	public void IsSmallCave(string name, bool expected)
	{
		var actual = Day12.IsSmallCave(name);
		Assert.Equal(expected, actual);
	}
	[Theory]
	[InlineData("start", false)]
	[InlineData("end", false)]
	[InlineData("a", false)]
	[InlineData("c", false)]
	[InlineData("A", true)]
	[InlineData("C", true)]
	public void IsBigCave(string name, bool expected)
	{
		var actual = Day12.IsBigCave(name);
		Assert.Equal(expected, actual);
	}
}
