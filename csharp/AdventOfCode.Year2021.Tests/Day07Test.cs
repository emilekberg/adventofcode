using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day07Test
{
	public string _input = "16,1,2,0,4,2,7,1,2,14";
	[Fact]
	public void Part1()
	{
		var expected = 37;
		var day = new Day07();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 168;
		var day = new Day07();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(16,5, 66)]
	[InlineData(1, 5, 10)]
	[InlineData(2, 5, 6)]
	[InlineData(0, 5, 15)]
	[InlineData(4, 5, 1)]
	[InlineData(2, 5, 6)]
	[InlineData(7, 5, 3)]
	[InlineData(1, 5, 10)]
	[InlineData(2, 5, 6)]
	[InlineData(14, 5, 45)]
	public void GetRequiredFuelToMoveToPosition2(int input, int target, int expected)
	{
		var actual = Day07.GetRequirerdFuelIncreased(new int[] { input }, target);
		Assert.Equal(expected, actual);
	}
}
