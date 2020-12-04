
using Xunit;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Year2020.Modules;

namespace AdventOfCode.Year2020.Tests.Modules
{
  public class LoopingMapTests
  {

    [Theory]
    [InlineData(0,0, PositionInformation.Empty)]
    [InlineData(2,0, PositionInformation.Tree)]
    [InlineData(2,2, PositionInformation.Empty)]
    // tests position loops
    [InlineData(12,0, PositionInformation.Empty)]
    
    public void GetPosition_ReturnsCorrectInformation(int x, int y, PositionInformation expected)
    {
      var input = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";
      var map = new LoopingMap(input);
      var actual = map.GetPosition(x, y);
      Assert.Equal(expected, actual);
    }
  }
}