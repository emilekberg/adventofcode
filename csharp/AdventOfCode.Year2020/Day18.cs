using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day18 : BaseDay<string[], long>, IDay
{
    public enum Operation
    {
        Empty,
        Number,
        Add,
        Multiply,
        ParenthesisStart,
        ParenthesisEnd
    };
    public enum OrderOfExecution
    {
        LeftToRight,
        AddFirst
    }
    public record MathOperation(Operation Operation, long? Number);

    public override long Part1(string[] input)
    {
        var sum = input
            .Select(str => str.Replace(" ", string.Empty))
            .Select(x => MapToOperation(x))
            .Select(x => SolveParenthesis(x, 0, x.Count, OrderOfExecution.LeftToRight))
            .Sum();
        return sum;
    }
    public override long Part2(string[] input)
    {
        var sum = input
            .Select(str => str.Replace(" ", string.Empty))
            .Select(x => MapToOperation(x))
            .Select(x => SolveParenthesis(x, 0, x.Count, OrderOfExecution.AddFirst))
            .Sum();
        return sum;
    }
    /// <summary>
    /// Maps a string input to a list of mathmatical operations.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<MathOperation> MapToOperation(string input)
    {
        var regex = new Regex(@"(\*|\+|\-|\/|\(|\))");
        var a = regex
            .Split(input)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(str =>
            {
                switch (str)
                {
                    case "*": return new MathOperation(Operation.Multiply, null);
                    case "+": return new MathOperation(Operation.Add, null);
                    case "(": return new MathOperation(Operation.ParenthesisStart, null);
                    case ")": return new MathOperation(Operation.ParenthesisEnd, null);
                    default: return new MathOperation(Operation.Number, long.Parse(str));
                }
            })
            .ToList();
        return a;
    }

    /// <summary>
    /// Solves the equations specified with in the list.
    /// </summary>
    /// <param name="operations">List of operations to solve.</param>
    /// <param name="toSolve">Only solve the specified operation. Empty to solve from left to right.</param>
    /// <returns></returns>
    public List<MathOperation> SolveOperation(List<MathOperation> operations, Operation toSolve)
    {
        var result = operations.ToList();
        int i = 1;
        if (result.Count == 1)
        {
            return result;
        }
        do
        {
            var left = result[i - 1];
            var sign = result[i];
            var right = result[i + 1];
            var didOperation = false;
            if (toSolve == Operation.Empty || sign.Operation == toSolve)
            {
                long answer = ProcessOperation(left.Number.Value, sign.Operation, right.Number.Value);
                var op = new MathOperation(Operation.Number, answer);
                result.Insert(i + 2, op);
                result.RemoveRange(i - 1, 3);
                didOperation = true;
            }
            if (!didOperation)
            {
                i += 2;
            }

        }
        while (i < result.Count);
        return result;
    }

    public long ProcessOperation(long a, Operation operation, long b)
    {
        return operation switch
        {
            Operation.Add => a + b,
            Operation.Multiply => a * b,
            _ => throw new ArgumentException($"Unknown operation {operation}")
        };
    }

    /// <summary>
    /// Returns a list of operations that exists within the perenthesis starting at the specified index.
    /// </summary>
    /// <param name="operations"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public List<MathOperation> GetOperationsInParenthesis(List<MathOperation> operations, int startIndex)
    {
        int level = 0;
        for (int i = startIndex; i < operations.Count; i++)
        {
            var m = operations[i];
            switch (m.Operation)
            {
                case Operation.ParenthesisStart:
                    level++;
                    break;
                case Operation.ParenthesisEnd:
                    level--;
                    break;
            }
            if (level == 0)
            {
                var result = operations.Skip(startIndex + 1).Take(i - startIndex - 1).ToList();
                return result;
            }
        }
        return new List<MathOperation>();
    }

    /// <summary>
    /// Solve the equations within the perenthesis. If there are none, it solves the math problem.
    /// </summary>
    /// <param name="operations"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public long SolveParenthesis(List<MathOperation> operations, int startIndex, int endIndex, OrderOfExecution order)
    {
        var searchList = operations.Skip(startIndex).Take(endIndex - startIndex).ToList();
        if (searchList.Find(x => x.Operation == Operation.ParenthesisStart) == null)
        {
            var sum = 0L;
            switch (order)
            {
                case OrderOfExecution.LeftToRight:
                    sum = SolveOperation(searchList, Operation.Empty).Single().Number.Value;
                    break;
                case OrderOfExecution.AddFirst:
                    var a = SolveOperation(searchList, Operation.Add);
                    var b = SolveOperation(a, Operation.Multiply);
                    sum = b.Single().Number.Value;
                    break;
            }

            return sum;
        }
        var result = new List<MathOperation>();
        for (int i = startIndex; i < operations.Count; i++)
        {
            var m = operations[i];
            switch (m.Operation)
            {
                case Operation.ParenthesisStart:
                    var subOp = GetOperationsInParenthesis(operations, i);
                    var l = SolveParenthesis(subOp, 0, subOp.Count, order);
                    result.Add(new MathOperation(Operation.Number, l));
                    i += subOp.Count + 1;
                    break;
                default:
                    result.Add(m);
                    break;
            }
        }
        return SolveParenthesis(result, 0, result.Count, order);
    }
}
