using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Days
{
	
	public class Day19Tests
	{
		[Fact]
		public void Part1()
		{
			var input = new StringBuilder()
				.AppendLine("0: 4 1 5")
				.AppendLine("1: 2 3 | 3 2")
				.AppendLine("2: 4 4 | 5 5")
				.AppendLine("3: 4 5 | 5 4")
				.AppendLine("4: \"a\"")
				.AppendLine("5: \"b\"")
				.AppendLine("")
				.AppendLine("ababbb")
				.AppendLine("bababa")
				.AppendLine("abbbab")
				.AppendLine("aaabbb")
				.AppendLine("aaaabbb")
				.ToString();
			var day = new Day19();
			var expected = 2;
			var actual = day.Part1(input);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Part2()
		{
			var input = new StringBuilder()
				.AppendLine("42: 9 14 | 10 1")
				.AppendLine("9: 14 27 | 1 26")
				.AppendLine("10: 23 14 | 28 1")
				.AppendLine("1: \"a\"")
				.AppendLine("11: 42 31")
				.AppendLine("5: 1 14 | 15 1")
				.AppendLine("19: 14 1 | 14 14")
				.AppendLine("12: 24 14 | 19 1")
				.AppendLine("16: 15 1 | 14 14")
				.AppendLine("31: 14 17 | 1 13")
				.AppendLine("6: 14 14 | 1 14")
				.AppendLine("2: 1 24 | 14 4")
				.AppendLine("0: 8 11")
				.AppendLine("13: 14 3 | 1 12")
				.AppendLine("15: 1 | 14")
				.AppendLine("17: 14 2 | 1 7")
				.AppendLine("23: 25 1 | 22 14")
				.AppendLine("28: 16 1")
				.AppendLine("4: 1 1")
				.AppendLine("20: 14 14 | 1 15")
				.AppendLine("3: 5 14 | 16 1")
				.AppendLine("27: 1 6 | 14 18")
				.AppendLine("14: \"b\"")
				.AppendLine("21: 14 1 | 1 14")
				.AppendLine("25: 1 1 | 1 14")
				.AppendLine("22: 14 14")
				.AppendLine("8: 42")
				.AppendLine("26: 14 22 | 1 20")
				.AppendLine("18: 15 15")
				.AppendLine("7: 14 5 | 1 21")
				.AppendLine("24: 14 1")
				.AppendLine("")
				.AppendLine("abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa")
				.AppendLine("bbabbbbaabaabba")
				.AppendLine("babbbbaabbbbbabbbbbbaabaaabaaa")
				.AppendLine("aaabbbbbbaaaabaababaabababbabaaabbababababaaa")
				.AppendLine("bbbbbbbaaaabbbbaaabbabaaa")
				.AppendLine("bbbababbbbaaaaaaaabbababaaababaabab")
				.AppendLine("ababaaaaaabaaab")
				.AppendLine("ababaaaaabbbaba")
				.AppendLine("baabbaaaabbaaaababbaababb")
				.AppendLine("abbbbabbbbaaaababbbbbbaaaababb")
				.AppendLine("aaaaabbaabaaaaababaa")
				.AppendLine("aaaabbaaaabbaaa")
				.AppendLine("aaaabbaabbaaaaaaabbbabbbaaabbaabaaa")
				.AppendLine("babaaabbbaaabaababbaabababaaab")
				.AppendLine("aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba")
				.ToString();
			var day = new Day19();
			var expected = 12;
			var actual = day.Part2(input);
			Assert.Equal(expected, actual);
		}
	}
}
