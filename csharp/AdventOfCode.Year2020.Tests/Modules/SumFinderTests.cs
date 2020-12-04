using Xunit;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Year2020.Modules;

namespace AdventOfCode.Year2020.Tests.Modules
{
  public class SumFinderTests
  {
    [Theory]
    [InlineData(2, 2020, 514579)]
    [InlineData(3, 2020, 241861950)]
    public void FindSum_FindsCorrectSum(int count, int expectedSum, int expectedMulti)
    {
      var numbers = new List<int>
      {
        1721,
        979,
        366,
        299,
        675,
        1456
      };
      var finder = new SumFinder(numbers);
      var result = finder.FindSum(2020, count);
      var sum = result.Aggregate(0, (acc, next) => acc+next);
      var multi = result.Aggregate(1, (acc, next) => acc*next);
      Assert.Equal(count, result.Count);
      Assert.Equal(expectedSum, sum);
      Assert.Equal(expectedMulti, multi);
    }

  }
}