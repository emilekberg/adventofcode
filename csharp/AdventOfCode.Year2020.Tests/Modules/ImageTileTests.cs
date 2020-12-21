using AdventOfCode.Year2020.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Year2020.Tests.Modules
{
	public class ImageTileTests
	{
		public static string TestTileData => new StringBuilder()
			.AppendLine("..##.#..#.")
			.AppendLine("##..#.....")
			.AppendLine("#...##..#.")
			.AppendLine("####.#...#")
			.AppendLine("##.##.###.")
			.AppendLine("##...#.###")
			.AppendLine(".#.#.#..##")
			.AppendLine("..#....#..")
			.AppendLine("###...#.#.")
			.AppendLine("..###..###")
			.ToString()
			.Replace("\r", string.Empty);

		public static string TestTileDataNoBorder => new StringBuilder()
			.AppendLine("#..#....")
			.AppendLine("...##..#")
			.AppendLine("###.#...")
			.AppendLine("#.##.###")
			.AppendLine("#...#.##")
			.AppendLine("#.#.#..#")
			.AppendLine(".#....#.")
			.AppendLine("##...#.#")
			.ToString()
			.Trim()
			.Replace("\r", string.Empty);
		[Fact]
		public void FlipX()
		{
			var tileData = TestTileData;
			var tile = new ImageTile(0, tileData);
			tile.DoFlipX();
			var expectedNorth = ".#..#.##..";
			Assert.Equal(expectedNorth, tile.BorderNorth);
		}

		[Fact]
		public void FlipY()
		{
			var tileData = TestTileData;
			var tile = new ImageTile(0, tileData);
			tile.DoFlipY();

			var expectedEast = "#..##.#...";
			Assert.Equal(expectedEast, tile.BorderEast);
		}

		[Fact]
		public void Rotate()
		{
			var tileData = TestTileData;
			var tile = new ImageTile(0, tileData);

			var expectedNorth = tile.BorderWest;
			var expectedEast = tile.BorderNorth;
			var expectedSouth = tile.BorderEast;
			var expectedWest = tile.BorderSouth;
			
			
			tile.DoRotateRight();

			Assert.Equal(90, tile.Rotation);
			Assert.Equal(expectedNorth, tile.BorderNorth.Reverse());
			Assert.Equal(expectedEast, tile.BorderEast);
			Assert.Equal(expectedSouth, tile.BorderSouth.Reverse());
			Assert.Equal(expectedWest, tile.BorderWest);
		}

		[Fact]
		public void Rotate_4Times()
		{
			var tileData = TestTileData;
			var tile = new ImageTile(0, tileData);

			var (n, s, e, w) = (tile.BorderNorth, tile.BorderSouth, tile.BorderEast, tile.BorderWest);

			tile.DoRotateRight();
			tile.DoRotateRight();
			tile.DoRotateRight();
			tile.DoRotateRight();

			Assert.Equal(0, tile.Rotation);
			Assert.Equal(n, tile.BorderNorth);
			Assert.Equal(s, tile.BorderSouth);
			Assert.Equal(e, tile.BorderEast);
			Assert.Equal(w, tile.BorderWest);
		}


		[Fact]
		public void GetDataWithoutBorder()
		{
			var tileData = TestTileData;
			var tile = new ImageTile(0, tileData);

			var expected = TestTileDataNoBorder;
			var actual = string.Join("\n", tile.GetDataWithoutBorder());

			Assert.Equal(expected, actual);
		}
	}
}
