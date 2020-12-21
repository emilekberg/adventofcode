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
			var orderedTiles = ProcessRelations(tiles);

			for(int y = 0; y < orderedTiles.Count; y++)
			{
				for(int x = 0; x < orderedTiles[y].Count; x++)
				{
					var tile = tiles.Single(tile => tile.Id == orderedTiles[y][x]);
				}
				Console.WriteLine();
			}

			return 0;
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
						if (northId.HasValue) throw new Exception("bajs med, allt sket sig");
						northId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderSouth))
					{
						if (southId.HasValue) throw new Exception("bajs med, allt sket sig");
						southId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderEast))
					{
						if (eastId.HasValue) throw new Exception("bajs med, allt sket sig");
						eastId = rhs.Id;
					}
					if (rhs.HasEdge(lhs.BorderWest))
					{
						if (westId.HasValue) throw new Exception("bajs med, allt sket sig");
						westId = rhs.Id;
					}
				}
				lookup.Add(lhs.Id, new TileResult(northId, eastId, southId, westId));
			}
			return lookup;
		}
	}
}
