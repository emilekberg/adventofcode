using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public record NumberRange(int Lower, int Upper)
	{
		public bool IsInRange(int number)
		{
			return number >= Lower && number <= Upper;
		}
	}

	public class Day16 : BaseDay<string, ulong>, IDay
	{
		public override ulong Part1(string input)
		{
			var cleaned = input.Replace("\r", string.Empty);

			var rules = FormatRules(cleaned);

			var yourTicket = FormatTickets("your ticket", cleaned).First();
			var nearbyTickets = FormatTickets("nearby tickets", cleaned);

			var result = nearbyTickets
				.Select(fields =>
				{
					var ticketResult = fields.Select(field =>
					{
						// check if any field matches the kriteras of any rule.
						var fieldResult = rules.All(kvp =>
						{
							var rule = kvp.Value;
							return !(rule.low.IsInRange(field) || rule.high.IsInRange(field));
						});

						// if any field matches the rules criteras, return 0, otherwise return the field value.
						return fieldResult ? field : 0;
					}).Sum();
					return ticketResult;
				})
				.Sum();
			return (ulong)result;
		}

		public record ValidityResult(string Rule, int Row);
		public override ulong Part2(string input)
		{
			return Part2(input, "departure");
		}
		public ulong Part2(string input, string searchWord)
		{
			var cleaned = input.Replace("\r", string.Empty);

			var rules = FormatRules(cleaned);

			var yourTicket = FormatTickets("your ticket", cleaned).First();
			var nearbyTickets = FormatTickets("nearby tickets", cleaned);

			var validTickets = nearbyTickets
				.Where(ticket =>
				{
					var ticketIsValid = !ticket.Any(field =>
					{
						// check if any field matches the kriteras of any rule.
						var ticketIsInvalid = rules.All(kvp =>
						{
							var rule = kvp.Value;
							return !(rule.low.IsInRange(field) || rule.high.IsInRange(field));
						});

						return ticketIsInvalid;
					});
					return ticketIsValid;
				})
				.ToList();

			
			var results = new List<ValidityResult>();
			rules.Keys.ToList().ForEach(key =>
			{
				var rule = rules[key];

				var width = validTickets[0].Count;
				for (int x = 0; x < width; x++)
				{
					bool entireRowValid = true;
					for (int y = 0; y < validTickets.Count; y++)
					{
						var value = validTickets[y][x];
						entireRowValid = entireRowValid && (rule.low.IsInRange(value) || rule.high.IsInRange(value));
						if (!entireRowValid)
						{
							break;
						}
					}
					if(entireRowValid)
					{
						results.Add(new ValidityResult(key, x));
					}
				}
			});

			
			var resultToMap = results.ToList();
			
			var ruleFieldMap = new Dictionary<string, int>();
			do
			{
				var groups = resultToMap.GroupBy(x => x.Row).ToList();
				var mapping = groups.First(x => x.Count() == 1).Single();
				ruleFieldMap.Add(mapping.Rule, mapping.Row);
				resultToMap = resultToMap.Where(x => x.Rule != mapping.Rule && x.Row != mapping.Row).ToList();
			}
			while (resultToMap.Count > 0);
			// multiply all values of my ticket with the fields that contains searchWord. 
			var result = ruleFieldMap.Keys.Where(x => x.Contains(searchWord))
				.Aggregate(1UL, (acc, next) =>
				{
					var value = ruleFieldMap[next];
					
					return acc * (ulong)yourTicket[value];
				});

			return result;
		}

		public Dictionary<string, (NumberRange low, NumberRange high)> FormatRules(string input)
		{
			var regex = new Regex(@"([\w\s]+):\s(\d+)-(\d+) or (\d+)-(\d+)");
			var result = new Dictionary<string, (NumberRange low, NumberRange high)>();
			var matches = regex.Matches(input);
			matches.ToList().ForEach(match =>
			{
				var key = match.Groups[1].Value.Trim();
				var l1 = int.Parse(match.Groups[2].Value);
				var h1 = int.Parse(match.Groups[3].Value);

				var l2 = int.Parse(match.Groups[4].Value);
				var h2 = int.Parse(match.Groups[5].Value);

				result.Add(key, (new NumberRange(l1, h1), new NumberRange(l2, h2)));

			});
			return result;
		}

		public List<List<int>> FormatTickets(string prefix, string input) =>
			new Regex(@$"{prefix}:\n([\d+,?\n]+)")
			.Match(input)
			.Groups[1].Value
			.Trim()
			.Split('\n')
			.Select(x =>
				x.Split(',')
				.Select(int.Parse)
				.ToList()
			).ToList();
	}
}
