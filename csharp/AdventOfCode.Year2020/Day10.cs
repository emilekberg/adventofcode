using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day10 : BaseDay<string[], ulong>, IDay
	{
		public override ulong Part1(string[] input)
		{
			var data = input.Select(int.Parse).ToList();
			var deviceJolts = data.Max() + 3;
			data.Add(0);
			data.Add(deviceJolts);
			data.Sort();
			var diffs = data.Zip(data.Skip(1), (a, b) => b - a).ToList();

			var ones = diffs.Count(x => x == 1);
			var threes = diffs.Count(x => x == 3);

			return (ulong)(ones * threes);
		}


		/// <summary>
		/// Did not solve this yet... don't fully understand how to sort this yet.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public override ulong Part2(string[] input)
		{
			var data = input.Select(ulong.Parse).ToList();
			var deviceJolts = data.Max() + 3;
			data.Add(0);
			data.Sort();

			// creates a lookup table for previous values.
			var storage = Enumerable.Range(0, data.Count).Select(_ => 0UL).ToList();
			
			// the first value is 1.
			storage[0] = 1;
			for (int i = 1; i < data.Count; i++)
			{
				ulong counter = 0;
				int index = i - 3;
				if (index < 0)
				{
					index = 0;
				}
				// Loop the previous seen values.
				for(int j = index; j < i; j++)
				{
					// check how many of them are within the range of the adapter.
					if(data[i] - data[j] <= 3)
					{
						// store how many paths they had available to themselves.
						counter += storage[j];
					}
				}
				// store the number of paths available as the number of paths to this adapter.
				storage[i] = counter;
			}

			// return the last element, which is the number of combinations available to the final device.
			return storage.Last();
		}

		/// <summary>
		/// Calculates the difference between two integers.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int Diff(int a, int b)
		{
			return b - a;
		}
	}
}
