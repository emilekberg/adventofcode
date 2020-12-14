using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class Day14 : BaseDay<string[], ulong>, IDay
	{
		private Regex MaskRegex = new Regex(@"mask\s=\s([\w\d]+)");
		private Regex MemoryRegex = new Regex(@"mem\[(\d+)\]\s=\s(\d+)");
		public override ulong Part1(string[] input)
		{

			var memory = new Dictionary<ulong, ulong>();
			List<(char n, int i)> currentMask = null;

			var enumerator = input.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var s = (string)enumerator.Current;
				Match regexResult = null;
				if ((regexResult = MaskRegex.Match(s)).Success)
				{
					var maskString = regexResult.Groups[1].Value;
					currentMask = maskString
						.ToCharArray()
						.Reverse()
						.Select((c, i) => (c, i))
						.Where(tuple => tuple.c != 'X')
						.ToList();
				} 
				else if ((regexResult = MemoryRegex.Match(s)).Success)
				{
					var memoryAddress = ulong.Parse(regexResult.Groups[1].Value);
					var memoryValue = ulong.Parse(regexResult.Groups[2].Value);

					var maskedValue = memoryValue;

					currentMask.ForEach((tuple) =>
					{
						switch(tuple.n)
						{
							case '1':
								maskedValue |= 1UL << tuple.i;
								break;
							case '0':
								maskedValue &= ~(1UL << tuple.i);
								break;
						}						
					});
					memory[memoryAddress] = maskedValue;
				}
			}
			ulong result = memory.Values.Aggregate(0UL, (acc, next) => acc + next);
			return result;
		}

		public override ulong Part2(string[] input)
		{
			var memory = new Dictionary<ulong, ulong>();
			List<int> currentMask = null;
			List<int> currentFloatingBits = null;
			var enumerator = input.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var s = (string)enumerator.Current;
				Match regexResult = null;
				if ((regexResult = MaskRegex.Match(s)).Success)
				{
					var maskString = regexResult.Groups[1].Value;
					var mask = maskString
						.ToCharArray()
						.Reverse()
						.Select((c, i) => (c, i));

					currentMask = mask
						.Where(tuple => tuple.c == '1')
						.Select(tuple => tuple.i)
						.ToList();
					currentFloatingBits = mask
						.Where(tuple => tuple.c == 'X')
						.Select(tuple => tuple.i)
						.ToList();
				}
				else if ((regexResult = MemoryRegex.Match(s)).Success)
				{
					var memoryAddress = ulong.Parse(regexResult.Groups[1].Value);
					var memoryValue = ulong.Parse(regexResult.Groups[2].Value);

					var addressMask = memoryAddress;

					currentMask.ForEach(i => addressMask |= 1UL << i);
					WriteMemory(memory, addressMask, currentFloatingBits, memoryValue);
				}
			}
			ulong result = memory.Values.Aggregate(0UL, (acc, next) => acc + next);
			return result;
		}

		public void WriteMemory(Dictionary<ulong, ulong> memory, ulong addressMask, List<int> indices, ulong value)
		{
			var shouldWriteToMemory = indices.Count == 1;
			var valueBitOn = addressMask;
			var valueBitOff = addressMask;
			var bitIndex = indices.First();

			// set bit to 0
			valueBitOff &= ~(1UL << bitIndex);
			
			// set bit to 1
			valueBitOn |= 1UL << bitIndex;
			if (shouldWriteToMemory)
			{
				memory[valueBitOn] = value;
				memory[valueBitOff] = value;
			}
			else
			{
				WriteMemory(memory, valueBitOff, indices.Skip(1).ToList(), value);
				WriteMemory(memory, valueBitOn, indices.Skip(1).ToList(), value);
			}

		}
	}
}
