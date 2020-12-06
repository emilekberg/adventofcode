using System;
using System.Linq;
using System.Collections.Generic;
namespace AdventOfCode.Year2020.Modules
{
  public enum PositionInformation
  {
    Empty,
    Tree
  }
  public class LoopingMap
  {
    List<List<string>> _map;
    public int Height {get; set;}
    public int Width {get; set;}
    public LoopingMap(string input)
    {
      _map = new List<List<string>>();
      var width = input.IndexOf(Environment.NewLine);

      var currentRow = 0;
      input.ToCharArray()
        .Select(x => x.ToString())
        .ToList()
        .ForEach(x => 
        {
          if(x == "\n")
          {
            currentRow++;
            _map.Add(new List<string>());
            return;
          }
          if(_map.Count-1 < currentRow)
          {
            _map.Add(new List<string>());
          }
          _map[currentRow].Add(x);
        }
      );
      Height = _map.Count;
      Width = _map[Height-1].Count;
      // Console.WriteLine($"currentRow:{currentRow}");
    }

    public PositionInformation GetPosition(int x, int y)
    {
      if(y >= _map.Count)
      {
        throw new ArgumentException($"Position is not valid, y is above height x:{x},y:{y}, width:{Width},height:{Height}");
      }
      var row = _map[y];
      var s = row[x % Width];
      switch(s)
      {
        case ".":
          return PositionInformation.Empty;
        case "#":
          return PositionInformation.Tree;
      }
      throw new ArgumentException($"Position is not valid x:{x},y:{y}, width:{Width},height:{Height}");
    }
  }
}