using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Year2018.Tests
{
	public class Day01Test
	{
		[Fact]
		public void IncreaseFrequency()
		{
			var modulator = new FrequencyModulator(2);
			modulator.Change(10);

			Assert.Equal(12, modulator.Frequency);
		}
		[Fact]
		public void DecreaseFrequency()
		{
			var modulator = new FrequencyModulator(1);
			modulator.Change(-3);

			Assert.Equal(-2, modulator.Frequency);
		}
		[Fact]
		public void Part01_FirstExample()
		{
			var modulator = new FrequencyModulator(0);
			modulator.Change(1)
				.Change(-2)
				.Change(3)
				.Change(1);

			Assert.Equal(3, modulator.Frequency);
		}

		[Theory]
		[InlineData(1,1,1,3)]
		[InlineData(1,1,-2,0)]
		[InlineData(-1,-2,-3,-6)]
		public void Part01_Examples(int a, int b, int c, int expected)
		{
			var modulator = new FrequencyModulator(0);
			modulator.Change(a)
				.Change(b)
				.Change(c);

			Assert.Equal(expected, modulator.Frequency);
		}

		[Fact]
		public void Part02_FirstExample()
		{
			var sequence = new List<int>{ 1, -2, 3, 1 };
			var modulator = new FrequencyModulator(0);
			var actual = modulator.FindFirstDuplicate(sequence);

			Assert.Equal(2, actual);
		}

		[Fact]
		public void Part02_SecondExample_Two_values()
		{
			var sequence = new List<int> { 1, -1 };
			var modulator = new FrequencyModulator(0);
			var actual = modulator.FindFirstDuplicate(sequence);

			Assert.Equal(0, actual);
		}

		[Theory]
		[InlineData(3,3,4,-2,-4, 10)]
		[InlineData(-6,3,8,5,-6, 5)]
		[InlineData(7,7,-2,-7,-4, 14)]
		public void Part02_SecondExample(int a, int b, int c, int d, int e, int expected)
		{
			var sequence = new List<int> { a, b, c, d, e };
			var modulator = new FrequencyModulator(0);
			var actual = modulator.FindFirstDuplicate(sequence);
			Assert.Equal(expected, actual);
		}
	}
}
