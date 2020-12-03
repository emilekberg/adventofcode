using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018
{
	/// <summary>
	/// https://adventofcode.com/2018/day/1
	/// </summary>
	public class Day01 : IDay
	{
		public Day01()
		{
	
		}
		public Task ExecuteAsync()
		{
			var sequence = System.IO.File.ReadAllText("./Data/Day01.txt")
				.Trim('\r', '\n', '+')
				.Split('\r')
				.Select(s => s.Trim('\n'))
				.Select(s => int.Parse(s))
				.ToList();
			var part1Result = Part1(sequence);
			Console.WriteLine($"Part1Result {part1Result}");
			var part2Result = Part2(sequence);
			Console.WriteLine($"Part2Result {part2Result}");
			return Task.CompletedTask;
		}

		public int Part1(List<int> sequence)
		{
			var modulator = new FrequencyModulator(0);
			sequence.ForEach(value => modulator.Change(value));
			var result = modulator.Frequency;
			return result;
		}

		public int Part2(List<int> sequence)
		{
			var modulator = new FrequencyModulator(0);
			return modulator.FindFirstDuplicate(sequence);
		}
	}

	public class FrequencyModulator
	{
		public int Frequency { get; protected set; }
		public FrequencyModulator(int frequency = 0)
		{			
			Frequency = frequency;
		}
		public FrequencyModulator Change(int value)
		{
			Frequency += value;
			return this;
		}

		public int FindFirstDuplicate(List<int> sequence)
		{
			var seenFrequencies = new HashSet<int>();
			seenFrequencies.Add(0);
			while (true)
			{
				foreach(var change in sequence)
				{
					Change(change);
					if (seenFrequencies.Contains(Frequency))
					{
						return Frequency;
					}
					seenFrequencies.Add(Frequency);
				}
			}
		}
	}
}
