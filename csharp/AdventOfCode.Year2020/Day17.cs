using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day17 : BaseDay<string[], int>, IDay
{
    public HashSet<(int x, int y, int z)> ActiveCubes { get; set; } = new();
    public HashSet<(int x, int y, int z, int w)> ActiveCubes4d { get; set; } = new();
    public override int Part1(string[] input)
    {
        var parsedData = input.Select(x => x.ToCharArray()).ToArray();

        for (int y = 0; y < parsedData.Length; y++)
        {
            for (int x = 0; x < parsedData[y].Length; x++)
            {
                var c = parsedData[y][x];
                if (c != '#')
                {
                    continue;
                }
                ActiveCubes.Add((x, y, 0));
            }
        }

        for (int cycle = 0; cycle < 6; cycle++)
        {
            var toAdd = new List<(int x, int y, int z)>();
            var toRemove = new List<(int x, int y, int z)>();
            var neightboursToParse = new List<(int x, int y, int z)>();
            foreach (var cube in ActiveCubes)
            {
                var neighbours = GetNeighbourCubes(cube.x, cube.y, cube.z);
                var activeCount = neighbours.Count(cube => IsCubeActive(cube.x, cube.y, cube.z));
                if (activeCount is not (2 or 3))
                {
                    toRemove.Add(cube);
                }
                var inactiveNeighbours = neighbours.Where(cube => IsCubeInactive(cube.x, cube.y, cube.z));
                neightboursToParse.AddRange(inactiveNeighbours);
            }
            foreach (var inactiveCube in neightboursToParse.Distinct())
            {
                var activeCount = GetActiveNeighbourCubes(inactiveCube.x, inactiveCube.y, inactiveCube.z).Count();
                if (activeCount == 3)
                {
                    toAdd.Add(inactiveCube);
                }
            }
            toRemove.ForEach(cube => ActiveCubes.Remove(cube));
            toAdd.ForEach(cube => ActiveCubes.Add(cube));
        }
        return ActiveCubes.Count;
    }

    public override int Part2(string[] input)
    {
        var parsedData = input.Select(x => x.ToCharArray()).ToArray();

        for (int y = 0; y < parsedData.Length; y++)
        {
            for (int x = 0; x < parsedData[y].Length; x++)
            {
                var c = parsedData[y][x];
                if (c != '#')
                {
                    continue;
                }
                ActiveCubes4d.Add((x, y, 0, 0));
            }
        }

        for (int cycle = 0; cycle < 6; cycle++)
        {
            var toAdd = new List<(int x, int y, int z, int w)>();
            var toRemove = new List<(int x, int y, int z, int w)>();
            var neightboursToParse = new List<(int x, int y, int z, int w)>();
            foreach (var cube in ActiveCubes4d)
            {
                var neighbours = GetNeighbourCubes(cube.x, cube.y, cube.z, cube.w);
                var activeCount = neighbours.Count(cube => IsCubeActive(cube.x, cube.y, cube.z, cube.w));
                if (activeCount is not (2 or 3))
                {
                    toRemove.Add(cube);
                }
                var inactiveNeighbours = neighbours.Where(cube => IsCubeInactive(cube.x, cube.y, cube.z, cube.w));
                neightboursToParse.AddRange(inactiveNeighbours);
            }
            foreach (var inactiveCube in neightboursToParse.Distinct())
            {
                var activeCount = GetActiveNeighbourCubes(inactiveCube.x, inactiveCube.y, inactiveCube.z, inactiveCube.w).Count();
                if (activeCount == 3)
                {
                    toAdd.Add(inactiveCube);
                }
            }
            toRemove.ForEach(cube => ActiveCubes4d.Remove(cube));
            toAdd.ForEach(cube => ActiveCubes4d.Add(cube));
        }
        return ActiveCubes4d.Count;
    }

    public void DrawCubes(int cycle)
    {
        Console.WriteLine($"After {cycle} cycles");
        var zMin = ActiveCubes.Min(ActiveCubes => ActiveCubes.z);
        var zMax = ActiveCubes.Max(ActiveCubes => ActiveCubes.z);
        var xMin = ActiveCubes.Min(ActiveCubes => ActiveCubes.x);
        var xMax = ActiveCubes.Max(ActiveCubes => ActiveCubes.x);
        var yMin = ActiveCubes.Min(ActiveCubes => ActiveCubes.y);
        var yMax = ActiveCubes.Max(ActiveCubes => ActiveCubes.y);

        for (int z = zMin; z <= zMax; z++)
        {
            Console.WriteLine($"z={z}");
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    Console.Write(IsCubeActive(x, y, z) ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }
        Console.ReadKey();
        Console.WriteLine();
    }

    public bool IsCubeActive(int x, int y, int z)
    {
        return ActiveCubes.Contains((x, y, z));
    }

    public bool IsCubeInactive(int x, int y, int z)
    {
        return !IsCubeActive(x, y, z);
    }

    public bool IsCubeActive(int x, int y, int z, int w)
    {
        return ActiveCubes4d.Contains((x, y, z, w));
    }

    public bool IsCubeInactive(int x, int y, int z, int w)
    {
        return !IsCubeActive(x, y, z, w);
    }

    public static IEnumerable<(int x, int y, int z, int w)> GetNeighbourCubes(int x, int y, int z, int w)
    {
        var result = new List<(int x, int y, int z, int w)>();
        for (int dx = -1; dx < 2; dx++)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dz = -1; dz < 2; dz++)
                {
                    for (int dw = -1; dw < 2; dw++)
                    {
                        if ((dx, dy, dz, dw).Equals((0, 0, 0, 0))) continue;
                        result.Add((x + dx, y + dy, z + dz, w + dw));
                    }
                }
            }
        }
        return result;
    }

    public static IEnumerable<(int x, int y, int z)> GetNeighbourCubes(int x, int y, int z)
    {
        var result = new List<(int x, int y, int z)>();
        for (int dx = -1; dx < 2; dx++)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dz = -1; dz < 2; dz++)
                {
                    if (dx == 0 && dy == 0 && dz == 0) continue;
                    result.Add((x + dx, y + dy, z + dz));
                }
            }
        }
        return result;
    }

    public IEnumerable<(int x, int y, int z)> GetActiveNeighbourCubes(int x, int y, int z)
    {
        return GetNeighbourCubes(x, y, z).Where(cube => ActiveCubes.Contains(cube));
    }

    public IEnumerable<(int x, int y, int z, int w)> GetActiveNeighbourCubes(int x, int y, int z, int w)
    {
        return GetNeighbourCubes(x, y, z, w).Where(cube => ActiveCubes4d.Contains(cube));
    }
}
