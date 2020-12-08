using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class SeatData
	{
		public int Row { get; set; }
		public int Column { get; set; }
		public int SeatId => Row * 8 + Column;
	}
	/// <summary>
	/// https://adventofcode.com/2020/day/5
	/// </summary>
	public class Day05 : BaseDay<string[], int>, IDay
	{
		public override int Part1(string[] input)
		{
			var highest = input
				.ToList()
				.Select(seatData => TraverseBSP(seatData))
				.Max(seatInfo => seatInfo.SeatId);
			return highest;
		}

		public override int Part2(string[] input)
		{
			var list = input
				.ToList()
				.Select(seatData => TraverseBSP(seatData))
				.OrderBy(x => x.SeatId)
				.ToList();
			SeatData prev = null;
			int id = 0;
			list.ForEach(seatInfo =>
			{
				if (prev != null)
				{
					if (prev.SeatId != seatInfo.SeatId - 1)
					{
						id = seatInfo.SeatId - 1;
					}
				}
				prev = seatInfo;
			});
			return id;
		}

		public SeatData TraverseBSP(string input)
		{
			var rowMax = 127;
			var rowMin = 0;
			var colMax = 7;
			var colMin = 0;

			int diff;
			input.ToCharArray()
				.ToList()
				.ForEach(c =>
				{
					switch (c)
					{
						case 'F':
							diff = (int)Math.Ceiling((rowMax - rowMin) * 0.5);
							rowMax -= diff;
							break;
						case 'B':
							diff = (int)Math.Ceiling((rowMax - rowMin) * 0.5);
							rowMin += diff;
							break;
						case 'L':
							diff = (int)Math.Ceiling((colMax - colMin) * 0.5);
							colMax -= diff;
							break;
						case 'R':
							diff = (int)Math.Ceiling((colMax - colMin) * 0.5);
							colMin += diff;
							break;
					}
				});
			return new SeatData
			{
				Row = rowMin,
				Column = colMin
			};
		}
	}
}
