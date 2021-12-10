using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day10Test
{
	public string[] _input = new string[]
	{
		"[({(<(())[]>[[{[]{<()<>>",
		"[(()[<>])]({[<{<<[]>>(",
		"{([(<{}[<>[]}>{[]{[(<()>",
		"(((({<>}<{<{<>}{[]{[]{}",
		"[[<[([]))<([[{}[[()]]]",
		"[{[{({}]{}}([{[{{{}}([]",
		"{<[[]]>}<{[{[{[]{()[[[]",
		"[<(<(<(<{}))><([]([]()",
		"<{([([[(<>()){}]>(<<{{",
		"<{([{{}}[<[[[<>{}]]]>[]]"
	};
	[Fact]
	public void Part1()
	{
		var expected = 26397;
		var day = new Day10();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 288957;
		var day = new Day10();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData("}}]])})]", 288957)]
	[InlineData(")}>]})", 5566)]
	[InlineData("}}>}>))))", 1480781)]
	[InlineData("]]}}]}]}>", 995444)]
	[InlineData("])}>", 294)]
	public void ScoreCalculator(string input, int expected)
	{
		var actual = Day10.AggregateScoreForIncompleteRow(input.ToCharArray());
		Assert.Equal(expected, actual);
	}
}
