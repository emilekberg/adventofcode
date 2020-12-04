using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode.Year2020.Modules
{
  /// 
  public class SumResult
  {
    public List<int> Values {get; set;} = new List<int>();
    public int Result {get; set;} = 0;
  }
  public class SumFinder
  {
    List<int> Numbers {get; set;}
    public SumFinder(List<int> numbers) 
    {
      Numbers = numbers;
    }

    public List<int> FindSum(int sumToFind, int iterations = 2)
    {
      var result = Recursive(sumToFind, iterations, new List<int>() );
      if(result != null)
      {
        return result;
      }
      return null;
    }

    public List<int> Recursive(int sumToFind, int iterations, List<int> toAdd)
    {
      var iteration = iterations-1;
      foreach(int value in Numbers)
      {
        if(iteration == 0)
        {
          var result = toAdd.ToList();
          result.Add(value);
          var sum = result.Aggregate(0, (acc, next) => acc + next);
          if(sum == sumToFind)
          {
            return result;
          }
        }
        else 
        {
          var copy = toAdd.ToList();
          copy.Add(value);
          var result = Recursive(sumToFind, iteration, copy);
          if(result != null)
          {
            return result;
          }
        }
      }
      return null;
    }
  }
}