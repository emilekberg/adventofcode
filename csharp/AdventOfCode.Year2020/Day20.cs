using AdventOfCode.Common;
using AdventOfCode.Year2020.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public static class StringExtension
	{
		public static string Reverse(this string s) => string.Join(string.Empty, s.ToCharArray().Reverse());
	}
	public record TileResult(int? NorthId, int? EastId, int? SouthId, int? WestId);
	public record MonsterDefinition
	{
		public static char MonsterPart = '#';
		public int Width { get; init; }
		public int Height { get; init; }
		public List<(int x, int y)> Points { get; private init; }
		
		public MonsterDefinition()
		{
			var definition = new string[]
			{
				"                  # ",
				"#    ##    ##    ###",
				" #  #  #  #  #  #   "
			};
			Height = definition.Length;
			Width = definition[0].Length;

			Points = new List<(int x, int y)>();
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					if (definition[y][x] == MonsterPart)
					{
						Points.Add((x, y));
					}
				}
			}
		}
	}
	public class Day20 : BaseDay<string, ulong>, IDay
	{
		
		public override ulong Part1(string input)
		{
			var tiles = input
				.Replace("\r", string.Empty)
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(tile =>
				{
					var regex = new Regex(@"Tile (\d+):\n");
					var split = regex.Split(tile);
					var id = int.Parse(split[1]);
					return new ImageTile(id, split[2]);
				})
				.ToList();

			
			RotateUntilTopLeftIsFound(tiles);

			var orderedTiles = ProcessRelations(tiles);
			
			var topLeft = orderedTiles.First().First();
			var topRight = orderedTiles.First().Last();
			var bottomLeft = orderedTiles.Last().First();
			var bottomRight = orderedTiles.Last().Last();

			var cornerTileArray = new int[] {
				topLeft, topRight,
				bottomLeft, bottomRight
			};

			var relations = GetTileRelations(tiles);
			var corners = relations.Where(x => cornerTileArray.Contains(x.Key)).ToList();

			var sum = cornerTileArray.Aggregate(1UL, (acc, next) => acc * (ulong)next);
			return sum;
		}

		public override ulong Part2(string input)
		{
			var tiles = input
				.Replace("\r", string.Empty)
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(tile =>
				{
					var regex = new Regex(@"Tile (\d+):\n");
					var split = regex.Split(tile);
					var id = int.Parse(split[1]);
					return new ImageTile(id, split[2]);
				})
				.ToList();


			RotateUntilTopLeftIsFound(tiles);
			var orderedTiles = ProcessRelations(tiles)
				.Select(row => row
					.Select(id => tiles
						.Single(tile => tile.Id == id)
					).ToList()
				).ToList();

			const int tileWidth = 8;
			const int tileHeight = 8;

			var width = orderedTiles.Count * tileWidth;
			var height = orderedTiles[0].Count * tileHeight;

			// creates a map.
			var map = new List<List<char>>();
			for(int i = 0; i < width * height; i++)
			{
				if (i % width == 0) map.Add(new List<char>());
				map[^1].Add('!');
			}

			// fills the list with values.
			for (int y = 0; y < orderedTiles.Count; y++)
			{
				for(int x = 0; x < orderedTiles[y].Count; x++)
				{
					var tile = orderedTiles[y][x];
					var data = tile.GetDataWithoutBorder();
					for(int row = 0; row < data.Count; row++)
					{
						for(int col = 0; col < data[row].Length; col++)
						{
							map[(y * tileHeight) + row][(x * tileWidth) + col] = data[row][col];
						}
					}
				}
			}

			var mergedMap = new ImageTile(map);
			var monsterDefinition = new MonsterDefinition();

			var result = SearchForMonsters(mergedMap, monsterDefinition);
			var numMonsterPositions = result.Count;
			var numHashTags = map
				.SelectMany(x => x)
				.Where(x => x == '#')
				.Count();
			return (ulong)(numHashTags - numMonsterPositions);
		}

		/// <summary>
		/// Loops through the map and looks for the defintition.
		/// If no monster is found, it tries to rotate the map, and flipping it until one is found.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="monsterDefinition"></param>
		/// <returns>The list of points where a monster was identified.</returns>
		public List<(int x, int y)> SearchForMonsters(ImageTile map, MonsterDefinition monsterDefinition)
		{
			var monsterPositions = new List<(int x, int y)>();
			var numRotations = 0;
			var hasFlippedX = false;
			do
			{
				for(int y = 0; y < map.Height; y++)
				{
					for(int x = 0; x < map.Width; x++)
					{
						if(!LookForMonsterAtPosition(map, x, y, monsterDefinition))
						{
							continue;
						}
						var positionToAdd = monsterDefinition.Points.Select(point => (point.x + x, point.y + y)).ToList();
						monsterPositions.AddRange(positionToAdd);
					}
				}
				if(monsterPositions.Count > 0)
				{
					return monsterPositions;
				}

				if(numRotations < 4)
				{
					map.DoRotateRight();
					numRotations++;
				}
				else if(map.Rotation == 0 && !hasFlippedX)
				{
					map.DoFlipX();
					numRotations = 0;
					hasFlippedX = true;
				}
			}
			while (true);
		}
		/// <summary>
		/// Looks for a monster defined at the various positions.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="monsterDefinition"></param>
		/// <returns>True if pattern following the description is found.</returns>
		public bool LookForMonsterAtPosition(ImageTile map, int startX, int startY, MonsterDefinition monsterDefinition)
		{
			var mapHeight = map.Height;
			var mapWidth = map.Width;
			if(startX + monsterDefinition.Width > mapWidth)
			{
				return false;
			}
			if(startY + monsterDefinition.Height > mapHeight)
			{
				return false;
			}
			return monsterDefinition.Points.All(point =>
			{
				var x = startX + point.x;
				var y = startY + point.y;

				return map.Get(x,y) == MonsterDefinition.MonsterPart;
			});
		}

		/// <summary>
		/// Picks a corner piece, and rotates until the topleft tile is found.
		/// </summary>
		/// <param name="tiles"></param>
		public void RotateUntilTopLeftIsFound(List<ImageTile> tiles)
		{
			var relations = GetTileRelations(tiles);
			//  find one of the corners
			var initialTile = relations.First(x =>
				(
					x.Value.SouthId.HasValue &&
					x.Value.EastId.HasValue &&
					x.Value.NorthId == null &&
					x.Value.WestId == null
				) ||
				(
					x.Value.SouthId == null &&
					x.Value.EastId.HasValue &&
					x.Value.NorthId.HasValue &&
					x.Value.WestId == null
				) ||
				(
					x.Value.SouthId == null &&
					x.Value.EastId == null &&
					x.Value.NorthId.HasValue &&
					x.Value.WestId.HasValue
				) ||
				(
					x.Value.SouthId.HasValue &&
					x.Value.EastId == null &&
					x.Value.NorthId == null &&
					x.Value.WestId.HasValue
				)
			);
			while (!relations.Any(x =>
				 x.Value.SouthId.HasValue &&
				 x.Value.EastId.HasValue &&
				 x.Value.NorthId == null &&
				 x.Value.WestId == null
			))
			{
				var tile = tiles.Single(x => x.Id == initialTile.Key);
				tile.DoRotateRight();
				relations = GetTileRelations(tiles);
			}
		}

		/// <summary>
		/// takes the top left tile, and fills it row by row until entire grid is filled.
		/// </summary>
		/// <param name="tiles"></param>
		/// <returns></returns>
		public List<List<int>> ProcessRelations(List<ImageTile> tiles)
		{
			var relations = GetTileRelations(tiles);
			try
			{
				var topLeft = relations.First(x =>
					x.Value.SouthId.HasValue &&
					x.Value.EastId.HasValue &&
					x.Value.NorthId == null &&
					x.Value.WestId == null
				);
				var tileLookup = tiles.ToDictionary(x => x.Id, x => x);
				var result = new List<List<int>>();
				result.Add(new List<int>());
				result[0].Add(topLeft.Key);
				var y = 0;
				var x = 0;
				do
				{
					var currentId = result[y][x];
					
					if (relations[currentId].EastId.HasValue)
					{
						var currentTile = tileLookup[currentId];
						// find next
						var nextId = relations[currentId].EastId.Value;
						var nextTile = tileLookup[nextId];
						// add next
						result[y].Add(nextId);
						x++;
						// rotate, flip, until matching
						var didChange = nextTile.ManipulateUntilMatching('w', currentTile.BorderEast);
						// if we rotated a tile, we need to update the relations map.
						if (didChange)
						{
							var relation = GetTileRelation(tiles, nextTile);
							relations[nextId] = relation;
						}
					}
					else
					{
						var firstIdInCurrentRow = result[y][0];
						var firstTileInCurrentRow = tileLookup[firstIdInCurrentRow];
						if (!relations[firstIdInCurrentRow].SouthId.HasValue)
						{
							return result;
						}
						y++;
						result.Add(new List<int>());
						var nextRowTileId = relations[firstIdInCurrentRow].SouthId.Value;
						var nextRowTile = tileLookup[nextRowTileId];
						result[y].Add(nextRowTileId);
						x = 0;

						// rotate, flip, until matching
						var didChange = nextRowTile.ManipulateUntilMatching('n', firstTileInCurrentRow.BorderSouth);
						// if we rotated a tile, we need to update the relations map.
						if (didChange)
						{
							var relation = GetTileRelation(tiles, nextRowTile);
							relations[nextRowTileId] = relation;
						}
					}

				}
				while (true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public TileResult GetTileRelation(List<ImageTile> tiles, ImageTile lhs)
		{
			var lookup = new Dictionary<int, TileResult>();
			int? northId = null, eastId = null, southId = null, westId = null;
			for (int j = 0; j < tiles.Count; j++)
			{
				var rhs = tiles[j];
				if (rhs.Id == lhs.Id) continue;

				if (rhs.HasEdge(lhs.BorderNorth))
				{
					northId = rhs.Id;
				}
				if (rhs.HasEdge(lhs.BorderSouth))
				{
					southId = rhs.Id;
				}
				if (rhs.HasEdge(lhs.BorderEast))
				{
					eastId = rhs.Id;
				}
				if (rhs.HasEdge(lhs.BorderWest))
				{
					westId = rhs.Id;
				}
				if (northId.HasValue && eastId.HasValue && southId.HasValue && westId.HasValue) break;
			}
			return new TileResult(northId, eastId, southId, westId);
		}
		
		/// <summary>
		/// Creates a dictionary with relations how tiles related to each other.
		/// </summary>
		/// <param name="tiles"></param>
		/// <returns></returns>
		public Dictionary<int, TileResult> GetTileRelations(List<ImageTile> tiles)
		{
			var lookup = new Dictionary<int, TileResult>();

			for (int i = 0; i < tiles.Count; i++)
			{
				int? northId = null, eastId = null, southId = null, westId = null;
				var lhs = tiles[i];

				for (int j = 0; j < tiles.Count; j++)
				{
					if (j == i) continue;
					var rhs = tiles[j];

					if(rhs.HasEdge(lhs.BorderNorth))
					{
						northId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderSouth))
					{
						southId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderEast))
					{
						eastId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderWest))
					{
						westId = rhs.Id;
					}
				}
				lookup.Add(lhs.Id, new TileResult(northId, eastId, southId, westId));
			}
			return lookup;
		}
	}
}
