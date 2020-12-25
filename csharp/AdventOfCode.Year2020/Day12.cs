using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public enum Direction
	{
		North,
		East,
		South,
		West,
		Left,
		Forward,
		Right
	}
	public record Position(int X, int Y);
	public class Day12 : BaseDay<string[], int>, IDay
	{

		public override int Part1(string[] input)
		{
			var position = new Position(0,0);
			var direction = Direction.East;
			var regex = new Regex(@"(\w)(\d+)");

			
			input
				.Select(x => regex.Match(x))
				.Where(res => res.Success)
				.Select(res =>
				{
					var directionString = res.Groups[1].Value;
					var amount = int.Parse(res.Groups[2].Value);
					var direction = directionString switch
					{
						"N" => Direction.North,
						"E" => Direction.East,
						"S" => Direction.South,
						"W" => Direction.West,
						"L" => Direction.Left,
						"F" => Direction.Forward,
						"R" => Direction.Right,
						_ => throw new NotImplementedException()
					};
					return (direction, amount);
				})
				.ToList()
				.ForEach((command) =>
				{
					switch(command.direction)
					{
						case Direction.North:
						case Direction.East:
						case Direction.South:
						case Direction.West:
							position = Move(position, command.direction, command.amount);
							break;
						case Direction.Left:
						case Direction.Right:
							direction = Rotate(direction, command.direction, command.amount);
							break;
						case Direction.Forward:
							position = Move(position, direction, command.amount);
							break;
					}
				});


			return Math.Abs(position.X) + Math.Abs(position.Y);
		}
		public static Direction Rotate(Direction from, Direction direction, int amount)
		{
			int to = (int)from;
			switch(direction)
			{
				case Direction.Left:
					to -= (amount / 90);
					break;
				case Direction.Right:
					to += (amount / 90);
					break;
			}
			if (to < 0) to += 4;
			return (Direction)(to % 4);
		}
		public static Position Move(Position position, Direction from, int amount)
		{
			return from switch
			{
				Direction.North => position with { Y = position.Y + amount },
				Direction.East => position with { X = position.X + amount },
				Direction.South => position with { Y = position.Y - amount },
				Direction.West => position with { X = position.X - amount },
				_ => position
			};
		}

		public override int Part2(string[] input)
		{
			var shipPosition = new Position(0,0);
			var waypointPosition = new Position(10,1);

			var regex = new Regex(@"(\w)(\d+)");

			input
				.Select(x => regex.Match(x))
				.Where(res => res.Success)
				.Select(res =>
				{
					var directionString = res.Groups[1].Value;
					var amount = int.Parse(res.Groups[2].Value);
					var direction = directionString switch
					{
						"N" => Direction.North,
						"E" => Direction.East,
						"S" => Direction.South,
						"W" => Direction.West,
						"L" => Direction.Left,
						"F" => Direction.Forward,
						"R" => Direction.Right,
						_ => throw new NotImplementedException()
					};
					return (direction, amount);
				})
				.ToList()
				.ForEach((command) =>
				{
					switch (command.direction)
					{
						case Direction.North:
						case Direction.East:
						case Direction.South:
						case Direction.West:
							waypointPosition = Move(waypointPosition, command.direction, command.amount);
							break;
						case Direction.Left:
						case Direction.Right:
							waypointPosition = RotateWaypoint(waypointPosition, command.direction, command.amount); 
							break;
						case Direction.Forward:
							shipPosition = MoveShipTowardsWaypoint(shipPosition, waypointPosition, command.amount);
							break;
					}
				});

			return Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
		}

		public static Position RotateWaypoint(Position waypointPosition, Direction direction, int degrees)
		{
			var multiplier = direction == Direction.Left ? 1 : -1;
			var degToRad = Math.PI / 180;
			var radians = multiplier * degrees * degToRad;
			var cos = Math.Cos(radians);
			var sin = Math.Sin(radians);
			var x = waypointPosition.X * cos - waypointPosition.Y * sin;
			var y = waypointPosition.X * sin + waypointPosition.Y * cos;
			return new Position(
				(int)Math.Round(x),
				(int)Math.Round(y)
			);
		}
		public static Position MoveShipTowardsWaypoint(Position shipPosition, Position waypointPosition, int amount)
		{
			return shipPosition with
			{
				X = shipPosition.X + waypointPosition.X * amount,
				Y = shipPosition.Y + waypointPosition.Y * amount
			};
		}
	}
}
