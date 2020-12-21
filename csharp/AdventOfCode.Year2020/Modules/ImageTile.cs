using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Modules
{
	public class ImageTile
	{

		public string BorderNorth { get; set; }
		public string BorderEast { get; set; }
		public string BorderSouth { get; set; }
		public string BorderWest { get; set; }
		public int Id { get; init; }

		public bool FlipX { get; set; }
		public bool FlipY { get; set; }
		public int Rotation { get; set; }

		public List<List<char>> Data { get; set; }
		public ImageTile(int id, string data)
		{
			Id = id;
			Data = data
				.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.ToCharArray().ToList())
				.ToList();
			UpdateBorders();
		}

		public void UpdateBorders()
		{
			var north = String.Join("", Data.First());
			var south = String.Join("", Data.Last());
			var east = String.Join("", Data.Select(x => x.Last()));
			var west = String.Join("", Data.Select(x => x.First()));
			BorderNorth = north;
			BorderSouth = south;
			BorderEast = east;
			BorderWest = west;
		}

		public void DoFlipX()
		{
			Data.ForEach(x => x.Reverse());
			UpdateBorders();
			FlipX = !FlipX;
		}

		public void DoFlipY()
		{
			Data.Reverse();
			UpdateBorders();
			FlipY = !FlipY;
		}

		public void DoRotateRight()
		{
			var result = new List<List<char>>();
			for (int x = 0; x < Data.Count; x++)
			{
				var row = new List<char>();
				for (int y = Data[x].Count-1; y >= 0 ; y--)
				{
					row.Add(Get(x, y));
				}
				result.Add(row);
			}
			Data = result;
			UpdateBorders();
			Rotation = (Rotation + 90) % 360;
		}

		public char Get(int x, int y)
		{
			return Data[y][x];
		}

		public bool HasEdge(string edge)
		{
			var revEdge = edge.Reverse();
			return
				BorderNorth == edge ||
				BorderSouth == edge ||
				BorderEast == edge ||
				BorderWest == edge ||
				BorderNorth == revEdge ||
				BorderSouth == revEdge ||
				BorderEast == revEdge ||
				BorderWest == revEdge;
		}

		public bool ManipulateUntilMatching(char border, string borderToValidate)
		{
			var hasFlippedX = false;
			// var hasFlippedY = false;
			var didChange = false;
			var shouldContinueLoop = true;
			do
			{
				if(hasFlippedX)
				{
					shouldContinueLoop = false;
				}
				for(int i = 0; i < 4; i++)
				{

					bool foundMatch = border switch
					{
						'n' => BorderNorth == borderToValidate,
						's' => BorderSouth == borderToValidate,
						'e' => BorderEast == borderToValidate,
						'w' => BorderWest == borderToValidate,
						_ => throw new NotImplementedException(),
					};
					if(foundMatch)
					{
						return didChange;
					}
					didChange = true;
					DoRotateRight();
				}
				if (!hasFlippedX)
				{
					DoFlipX();
					hasFlippedX = true;
				}
				/*else if (!hasFlippedY)
				{
					DoFlipY();
					hasFlippedY = true;
				}*/
			}
			while (shouldContinueLoop);
			throw new Exception("should not happen");
		}

	}
}
