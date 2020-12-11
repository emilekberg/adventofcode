using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public static class Position
	{
		public const char FLOOR = '.';
		public const char EMPTY_SEAT = 'L';
		public const char OCCUPIED_SEAT = '#';
	}
	public class Day11 : BaseDay<string[], long>, IDay
	{
		public override long Part1(string[] input)
		{
			var height = input.Length;
			var width = input[0].Length;

			var currentState = input.Select(x => x.ToCharArray().ToList()).ToList();
			var nextState = input.Select(x => x.ToCharArray().ToList()).ToList();

			var rounds = 0;
			var changeHasBeenMade = true;

			do
			{
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						if (SeatIsEmpty(currentState, x, y) && NumberOfAdjacentOccupantSeats(currentState, x, y) == 0)
						{
							nextState[y][x] = Position.OCCUPIED_SEAT;
						}
						if (SeatIsOccupied(currentState, x, y) && NumberOfAdjacentOccupantSeats(currentState, x, y) >= 4)
						{
							nextState[y][x] = Position.EMPTY_SEAT;
						}
					}
				}
				rounds++;
				changeHasBeenMade = currentState.Zip(nextState, (x, y) => x.SequenceEqual(y)).All(x => x);
				currentState = nextState.Select(x => x.ToList()).ToList();
				
			}
			while (!changeHasBeenMade);
			return currentState.SelectMany(x => x).Where(x => x == Position.OCCUPIED_SEAT).Count();
		}


		public override long Part2(string[] input)
		{
			var height = input.Length;
			var width = input[0].Length;

			var currentState = input.Select(x => x.ToCharArray().ToList()).ToList();
			var nextState = input.Select(x => x.ToCharArray().ToList()).ToList();

			var rounds = 0;
			var changeHasBeenMade = true;

			do
			{
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						if (SeatIsEmpty(currentState, x, y) && NumberOfOccupantSeatsInLineOfSight(currentState, x, y).occupied == 0)
						{
							nextState[y][x] = Position.OCCUPIED_SEAT;
						}
						if (SeatIsOccupied(currentState, x, y) && NumberOfOccupantSeatsInLineOfSight(currentState, x, y).occupied >= 5)
						{
							nextState[y][x] = Position.EMPTY_SEAT;
						}
					}
				}
				rounds++;
				changeHasBeenMade = currentState.Zip(nextState, (x, y) => x.SequenceEqual(y)).All(x => x);
				currentState = nextState.Select(x => x.ToList()).ToList();

			}
			while (!changeHasBeenMade);
			return currentState.SelectMany(x => x).Where(x => x == Position.OCCUPIED_SEAT).Count();
		}

		public bool SeatIsEmpty(List<List<char>> state, int seatX, int seatY)
		{
			return state[seatY][seatX] == Position.EMPTY_SEAT;
		}
		public bool SeatIsOccupied(List<List<char>> state, int seatX, int seatY)
		{
			return state[seatY][seatX] == Position.OCCUPIED_SEAT;
		}
		public int NumberOfAdjacentOccupantSeats(List<List<char>> state, int seatX, int seatY)
		{
			var height = state.Count();
			var width = state[0].Count();
			var seenOccupied = 0;
			var seenEmpty = 0;
			for(int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					var checkX = seatX + x;
					var checkY = seatY + y;
					if (checkX < 0 || checkX >= width)
						continue;
					if (checkY < 0 || checkY >= height)
						continue;
					if (x == 0 && y == 0)
						continue;

					var seat = state[checkY][checkX];
					switch (seat)
					{
						case Position.OCCUPIED_SEAT:
							seenOccupied++;
							break;
						case Position.EMPTY_SEAT:
							seenEmpty++;
							break;
					}
				}
			}
			return seenOccupied;
		}

		public (int occupied, int empty) NumberOfOccupantSeatsInLineOfSight(List<List<char>> state, int seatX, int seatY)
		{
			var height = state.Count();
			var width = state[0].Count();
			var directionsToCheck = new List<(int dX, int dY)>
			{
				(-1, -1), (-1, 0), (-1, 1),
				(0, -1),           (0, 1),
				(1, -1),  (1, 0),  (1, 1),
			};
			var data = directionsToCheck.Select(direction =>
			{
				var x = 0;
				var y = 0;
				do
				{
					x += direction.dX;
					y += direction.dY;
					var checkX = seatX + x;
					var checkY = seatY + y;
					if (checkX < 0 || checkX >= width)
						return (0, 0);
					if (checkY < 0 || checkY >= height)
						return (0, 0);

					var seat = state[checkY][checkX];
					switch (seat)
					{
						case Position.OCCUPIED_SEAT:
							return (1, 0);
						case Position.EMPTY_SEAT:
							return (0, 1);
					}
				}
				while (true);
			})
			.Aggregate((0, 0), (acc, curr) =>
			 {
				 return (acc.Item1 + curr.Item1, acc.Item2 + curr.Item2);
			 });
			return data;
		}
	}
}
