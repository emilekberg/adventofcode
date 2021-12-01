using AdventOfCode.Common;

namespace AdventOfCode.Year2021
{
    /// <Summary>
    /// https://adventofcode.com/2021/day/1
    /// </Summary>
    public class Day01 : BaseDay<string[], int>, IDay
    {
        public override int Part1(string[] input)
        {
            var convertedInput = input.Select(int.Parse).ToArray();
            var result = GetNumberOfIncreases(convertedInput);
            return result;
        }

        public override int Part2(string[] input)
        {
            var convertedInput = input.Select(int.Parse).ToArray();
            var summed = convertedInput
                .Zip(convertedInput.Skip(1), convertedInput.Skip(2))
                .Select(values => values.First + values.Second + values.Third)
                .ToArray();
            var result = GetNumberOfIncreases(summed);
            return result;
        }

        public static int GetNumberOfIncreases(int[] values) => values
            .Zip(values.Skip(1))
            .Count(values => values.Second > values.First);
    }
}
