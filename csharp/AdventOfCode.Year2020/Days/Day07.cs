using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class BagContainInfo
	{
		public string Color { get; set; }
		public int Amount { get; set; }
	}
	public class Day07 : IDay
	{
		public async Task ExecuteAsync()
		{
			var input = await System.IO.File.ReadAllTextAsync("./Data/Day07.txt");
			var resultPart1 = Part1(input);
			var resultPart2 = Part2(input);

			Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");
		}
		public int Part1(string input)
		{
			var dictionary = StringToDictionary(input);
			var a = dictionary
				.Keys
				.Where(x => CanContainShinyGold(dictionary, x))
				.ToList();

			return a.Count();
		}

		public Dictionary<string, List<BagContainInfo>> StringToDictionary(string input)
		{
			var regex = new Regex(@"(\d)\s(\w+\s\w+)");
			return input.Split('\n')
				.Select(row =>
				{
					var split = row.Split("contain", StringSplitOptions.TrimEntries);
					var key = split[0].Replace("bags", "").Trim();
					var value = split[1]
						.Split(',', StringSplitOptions.TrimEntries)
						.Select(x =>
						{
							var match = regex.Match(x);
							if (!match.Success)
							{
								return null;
							}
							var amount = int.Parse(match.Groups[1].Value);
							var color = match.Groups[2].Value;
							return new BagContainInfo
							{
								Amount = amount,
								Color = color
							};
						})
						.Where(x => x != null)
						.ToList();
					return (key, value);
				})
				.ToDictionary(x => x.key, x => x.value);
		}

		public bool CanContainShinyGold(Dictionary<string, List<BagContainInfo>> dictionary, string bag)
		{
			if(!dictionary.TryGetValue(bag, out var set))
			{
				return false;
			}
			
			if (set.Find(x => x.Color == "shiny gold") != null)
			{
				return true;
			}

			return set
				.Where(x => CanContainShinyGold(dictionary, x.Color))
				.Any();
		}

		public int Part2(string input)
		{
			var dictionary = StringToDictionary(input);
			if (!dictionary.TryGetValue("shiny gold", out var bag))
			{
				throw new ArgumentException("input not valid");
			}
			var a = bag.Select(x => GetBagsInContainer(dictionary, x));

			return a.Sum();
		}

		public int GetBagsInContainer(Dictionary<string, List<BagContainInfo>> dictionary, BagContainInfo bag)
		{
			if (!dictionary.TryGetValue(bag.Color, out var set))
			{
				return bag.Amount;
			}

			if(set.Count == 0)
			{
				return bag.Amount;
			}

			var amount = set
				.Select(x => GetBagsInContainer(dictionary, x))
				.Sum();
			return bag.Amount + (bag.Amount * amount);
		}
	}
}
