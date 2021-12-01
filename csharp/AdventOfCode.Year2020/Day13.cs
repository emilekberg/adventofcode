using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day13 : BaseDay<string[], long>, IDay
{
    public override long Part1(string[] input)
    {
        var departureTime = int.Parse(input[0]);
        var bussIds = input[1]
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x != "x")
            .Select(int.Parse)
            .ToList();

        var nextBuss = bussIds
            .Select(bussId =>
            {
                var nextDepartureTime = ((departureTime / bussId) + 1) * bussId;
                return (bussId, nextDepartureTime);
            })
            .OrderBy(x => x.nextDepartureTime)
            .First();

        var diff = nextBuss.nextDepartureTime - departureTime;
        return diff * nextBuss.bussId;
    }

    public override long Part2(string[] input)
    {
        var bussIds = input[1]
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select((s, i) => (s, i))
            .Where(tuple => Regex.IsMatch(tuple.s, @"\d+"))
            .Select(tuple =>
            {
                var bussId = int.Parse(tuple.s);
                var mod = bussId - tuple.i;
                return (bussId, mod);
            })
            .ToList();

        var n = bussIds.Select(x => x.bussId).ToList();
        var a = bussIds.Select(x => x.mod).ToList();

        var num = CRT(n, a);

        return num;
    }

    // from https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
    public long CRT(List<int> n, List<int> a)
    {
        long product = n.Aggregate(1L, (acc, next) => acc * next);
        long p;
        long sm = 0L;
        for (int i = 0; i < n.Count; i++)
        {
            p = product / n[i];
            sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
        }
        return sm % product;
    }

    public long ModularMultiplicativeInverse(long a, long mod)
    {
        long b = a % mod;
        for (int x = 1; x < mod; x++)
        {
            if ((b * x) % mod == 1)
            {
                return x;
            }
        }
        return 1;
    }
}
