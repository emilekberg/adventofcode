using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day14Test
{
	public string[] _input = new string[]
	{
		"NNCB",
		"",
		"CH -> B",
		"HH -> N",
		"CB -> H",
		"NH -> C",
		"HB -> C",
		"HC -> B",
		"HN -> C",
		"NN -> C",
		"BH -> H",
		"NC -> B",
		"NB -> B",
		"BN -> B",
		"BB -> N",
		"BC -> B",
		"CC -> N",
		"CN -> C"
	};
	[Fact]
	public void Part1()
	{
		var expected = 1588;
		var day = new Day14();
		var actual = day.Part1(_input);
		Assert.Equal(expected, actual);
	}
	[Fact]
	public void Part2()
	{
		var expected = 2188189693529;
		var day = new Day14();
		var actual = day.Part2(_input);
		Assert.Equal(expected, actual);
	}
}
