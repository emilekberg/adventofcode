using AdventOfCode.Common;
using AdventOfCode.Year2020.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

/// <summary>
/// https://adventofcode.com/2020/day/8
/// </summary>
public class Day08 : BaseDay<string[], int>, IDay
{
    public List<Command> FormatInput(string[] input)
    {
        return input
            .Select(row => row.Split(' '))
            .Select(x =>
            {
                if (!int.TryParse(x[1], out var argument))
                {
                    throw new ArgumentException($"Could not parse {x[1]} to an int.");
                }
                return new Command
                {
                    Instruction = x[0],
                    Argument = argument
                };
            })
            .ToList();
    }
    public override int Part1(string[] input)
    {
        var program = FormatInput(input);

        var computer = new Computer();
        computer.HasInfiniteLoop(program);
        return computer.Accumulator;
    }
    public override int Part2(string[] input)
    {
        var originalProgram = FormatInput(input);
        var computer = new Computer();
        var lastChangedInstruction = 0;
        bool exitSuccessfully;
        do
        {
            computer.ResetAccumulator();
            var program = originalProgram.ToList();
            var commandToChange = program[++lastChangedInstruction];
            switch (commandToChange.Instruction)
            {
                case "jmp":
                    program[lastChangedInstruction] = new Command
                    {
                        Instruction = "nop",
                        Argument = commandToChange.Argument
                    };
                    break;
                case "nop":
                    program[lastChangedInstruction] = new Command
                    {
                        Instruction = "jmp",
                        Argument = commandToChange.Argument
                    };
                    break;
            }

            exitSuccessfully = computer.HasInfiniteLoop(program);
        }
        while (exitSuccessfully || lastChangedInstruction == input.Length);
        return computer.Accumulator;
    }
}
