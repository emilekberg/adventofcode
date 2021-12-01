using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day24 : BaseDay<string[], int>, IDay
{
    private static string[] Direction = { "e", "se", "sw", "w", "nw", "ne" };
    public static List<List<string>> ParseInput(string[] input) => input
            .Select(x =>
                Regex.Split(x, string.Join("|", Direction.Select(x => $"({x})")))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList()
            ).ToList();
    public override int Part1(string[] input)
    {
        var lines = ParseInput(input);

        var blackTiles = new HashSet<(int x, int y)>();
        lines.ForEach(line =>
        {
            var pos = Traverse(line);
            if (blackTiles.Contains(pos))
            {
                blackTiles.Remove(pos);
            }
            else
            {
                blackTiles.Add(pos);
            }
        });
        return blackTiles.Count;
    }

    public override int Part2(string[] input) => Part2(input, 100);

    public int Part2(string[] input, int numDays)
    {
        var lines = ParseInput(input);

        var tileStates = new HashSet<(int x, int y)>();

        // setup initial state.
        lines.ForEach(line =>
        {
            var pos = Traverse(line);
            if (tileStates.Contains(pos))
                tileStates.Remove(pos);
            else
                tileStates.Add(pos);
        });

        for (int days = 0; days < numDays; days++)
        {
            var stateDelta = new List<(int x, int y, bool add)>();
            var whiteAdjacentTiles = new List<(int x, int y)>();

            // check state of every black tile.
            foreach (var tile in tileStates)
            {
                var adjacent = GetAdjacentPositions(tile.x, tile.y);
                var blackAdjacentCount = adjacent.Count(x => tileStates.Contains(x));
                if (blackAdjacentCount is 0 or > 2)
                {
                    stateDelta.Add((tile.x, tile.y, false));
                }
                whiteAdjacentTiles.AddRange(adjacent.Where(x => !tileStates.Contains(x)));
            }
            // check state of every white tile.
            foreach (var tile in whiteAdjacentTiles)
            {
                var adjacent = GetAdjacentPositions(tile.x, tile.y);
                var blackAdjacentCount = adjacent.Count(x => tileStates.Contains(x));
                if (blackAdjacentCount is 2)
                {
                    stateDelta.Add((tile.x, tile.y, true));
                }
            };
            foreach (var (x, y, add) in stateDelta)
            {
                if (add)
                    tileStates.Add((x, y));
                else
                    tileStates.Remove((x, y));
            }
        }

        return tileStates.Count;
    }

    public static List<(int x, int y)> GetAdjacentPositions(int posX, int posY)
    {
        return new (int x, int y)[]
        {
				// east
				(2, 0),
				// south east
				(1, 1),
				// south west
				(-1, 1),
				// west
				(-2, 0),
				// north west
				(-1, -1),
				// north east
				(1, -1),
        }.Select(tuple => (tuple.x + posX, tuple.y + posY)).ToList();
    }

    public static (int x, int y) Traverse(List<string> line)
    {
        int x = 0, y = 0;
        line.ForEach(instruction =>
        {
            switch (instruction)
            {
                case "e":
                    x += 2;
                    break;
                case "se":
                    x += 1;
                    y += 1;
                    break;
                case "sw":
                    x -= 1;
                    y += 1;
                    break;
                case "w":
                    x -= 2;
                    break;
                case "nw":
                    x -= 1;
                    y -= 1;
                    break;
                case "ne":
                    x += 1;
                    y -= 1;
                    break;
            }
        });
        return (x, y);
    }
}
