using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class Day19 : BaseDay<string, int>, IDay
	{
		public override int Part1(string input)
		{
			var split = input.Replace("\r", string.Empty).Split("\n\n");
			var rules = ParseRules(split[0]);
			var messages = ParseMessages(split[1]);
			return Solve(rules, messages);
		}

		public override int Part2(string input)
		{
			var split = input.Replace("\r", string.Empty).Split("\n\n");
			var rules = ParseRules(split[0]);
			var messages = ParseMessages(split[1]);

			ReplaceRule8(rules);
			ReplaceRule11(rules);

			return Solve(rules, messages);
		}


		public int Solve(Dictionary<string, List<string>> rules, List<string> messages)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("^(");
			BuildRegex(rules, rules["0"], stringBuilder);
			stringBuilder.Append(")$");
			var regexString = stringBuilder.ToString();
			var valid = messages.Where(message => Regex.IsMatch(message, regexString)).ToList();
			return valid.Count;
		}
		/// <summary>
		/// Updated rule 8 is rule 42 multiple times, if rule 42 was a, then rule 8 would be /a+/
		/// </summary>
		/// <param name="rules"></param>
		public void ReplaceRule8(Dictionary<string, List<string>> rules)
		{
			var sb = new StringBuilder();
			BuildRegex(rules, rules["42"], sb);
			sb.Append('+');
			rules["8"] = new List<string> { sb.ToString() };
		}
		/// <summary>
		/// Updated Rule 11 is rule 42 and 31 recursive. If rule 42 was a and rule 31 was be, rule 8 would be
		/// /a|b|aa|bb|aaa|bbb...../
		/// has a limit on number of recusions since this can be infinite.
		/// </summary>
		/// <param name="rules"></param>
		public void ReplaceRule11(Dictionary<string, List<string>> rules)
		{
			var sb = new StringBuilder();
			int limit = 20;
			sb.Append('(');
			for (int i = 1; i < limit; i++)
			{
				for(int j = 0; j < i; j++)
				{
					BuildRegex(rules, rules["42"], sb);
				}
				for (int j = 0; j < i; j++)
				{
					BuildRegex(rules, rules["31"], sb);
				}
				if (i+1 != limit)
				{
					sb.Append('|');
				}
			}
			sb.Append(')');
			rules["11"] = new List<string> { sb.ToString() };
		}

		public void BuildRegex(Dictionary<string, List<string>> rules, List<string> ruleToProcess, StringBuilder sb)
		{
			if(ruleToProcess.Contains("|"))
			{
				var splitIndex = ruleToProcess.IndexOf("|");
				var a = ruleToProcess.Take(splitIndex).ToList();
				var b = ruleToProcess.Skip(splitIndex + 1).Take(ruleToProcess.Count - splitIndex - 1).ToList();

				sb.Append('(');
				BuildRegex(rules, a, sb);
				sb.Append('|');
				BuildRegex(rules, b, sb);
				sb.Append(')');
				return;
			}
			for(int i = 0; i < ruleToProcess.Count; i++)
			{
				var rule = ruleToProcess[i];
				if(Regex.IsMatch(rule, @"\d+"))
				{
					var subRule = rules[rule];
					BuildRegex(rules, subRule, sb);
				}
				else
				{
					sb.Append(rule);
				}
			}
		}

		public Dictionary<string, List<string>> ParseRules(string rulesString)
		{
			var split = rulesString.Split('\n')
				.Select(x => x.Split(":", StringSplitOptions.RemoveEmptyEntries))
				.ToDictionary(
					x => x[0], 
					x => x[1]
						.Replace("\"", string.Empty)
						.Split(' ', StringSplitOptions.RemoveEmptyEntries)
						.ToList()
				);

			return split;
		}
		public List<string> ParseMessages(string messagesString)
		{
			return messagesString
				.Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.ToList();
		}
	}
}
