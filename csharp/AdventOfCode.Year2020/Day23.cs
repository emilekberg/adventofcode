using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

/// <summary>
/// custom linked list implementation with hashing functionality.
/// Can easily lookup the node of a specific value.
/// </summary>
public class LL
{
    public LL(IEnumerable<uint> values)
    {
        LLNode<uint> prev = null;
        var count = values.Count();
        Lookup = Enumerable.Range(0, count + 1).Select(x => (LLNode<uint>)null).ToList();
        foreach (var val in values)
        {
            var node = new LLNode<uint>(val);
            if (Head == null)
            {
                Head = node;
            }
            else if (prev != null)
            {
                prev.Next = node;
            }
            Lookup[(int)val] = node;
            prev = node;
        }
    }

    public LLNode<uint> Head { get; set; } = null;
    public List<LLNode<uint>> Lookup { get; set; }
    public void AddAfterNode(LLNode<uint> parent, LLNode<uint> node)
    {
        var parentNext = parent.Next;
        parent.Next = node;
        node.Next = parentNext;
        Lookup[(int)node.Value] = node;
    }
    public LLNode<uint> RemoveNext(LLNode<uint> node)
    {
        var removed = node.Next;
        node.Next = node.Next.Next;
        removed.Next = null;
        Lookup[(int)removed.Value] = null;
        return removed;
    }
    public LLNode<uint> RemoveHead()
    {
        var removed = Head;
        Head = Head.Next;
        removed.Next = null;
        Lookup[(int)removed.Value] = null;
        return removed;
    }
}
/// <summary>
/// Custom node for LL Implementation.
/// </summary>
/// <typeparam name="T"></typeparam>
public class LLNode<T>
{
    public LLNode(T value)
    {
        Value = value;
    }
    public LLNode<T> Next { get; set; }
    public T Value { get; set; }
}
public class Day23 : BaseDay<string, ulong>, IDay
{
    public override ulong Part1(string input) => Part1(input, 100);
    public ulong Part1(string input, ulong numMoves = 100)
    {
        var numbers = input.ToCharArray()
            .Select(x => x.ToString())
            .Select(uint.Parse)
            .ToList();

        var result = CrabGameCustomLinkedList(numbers, numMoves);

        var resultString = string.Empty;
        var startIndex = result.IndexOf(1) + 1;
        for (int i = 0; i < result.Count - 1; i++)
        {
            var index = (startIndex + i) % result.Count;
            resultString += result[index].ToString();
        }
        var resultLong = ulong.Parse(resultString);
        return resultLong;
    }
    public override ulong Part2(string input) => Part2(input, 10_000_000);
    public ulong Part2(string input, ulong numMoves = 10_000_000)
    {
        var numbers = input.ToCharArray()
            .Select(x => x.ToString())
            .Select(uint.Parse)
            .ToList();

        Console.WriteLine($"adding a bunch of numbers");
        for (uint i = numbers.Max() + 1; i <= 1_000_000UL; i++)
        {
            numbers.Add(i);
        }
        Console.WriteLine($"done! starting game");
        var result = CrabGameCustomLinkedList(numbers, numMoves);

        var indexOf1 = result.IndexOf(1);
        Console.WriteLine($"found {1} at {indexOf1}");
        var resultLong = 1UL;
        for (int i = 1; i <= 2; i++)
        {
            var index = (indexOf1 + i) % numbers.Count;
            var value = result[index];
            resultLong *= value;
        }
        return resultLong;
    }

    public List<uint> CrabGameCustomLinkedList(List<uint> numbers, ulong numMoves)
    {
        LL cups = new LL(numbers);
        var currentNode = cups.Head;
        var smallestNumber = numbers.Min();
        for (ulong move = 0; move < numMoves; move++)
        {
            // remove the following 3 values after the current node.
            var pickupNodes = new List<LLNode<uint>>(3);
            for (int i = 0; i < 3; ++i)
            {
                // remove head
                if (currentNode.Next == null)
                    pickupNodes.Add(cups.RemoveHead());
                else
                    pickupNodes.Add(cups.RemoveNext(currentNode));
            }

            // find the destination node, nearest smallest node from the current value.
            LLNode<uint> destinationNode = null;
            for (uint destinationValue = currentNode.Value - 1; destinationValue >= smallestNumber; destinationValue--)
            {
                var lookupNode = cups.Lookup[(int)destinationValue];
                if (lookupNode != null)
                {
                    destinationNode = lookupNode;
                    break;
                }
            }

            // if the above algorithm does not find a value, select the greatest value.
            destinationNode ??= cups.Lookup.Last(x => x != null);

            // Add the nodes after the destination node.
            for (int i = 0; i < 3; i++)
            {
                var nodeToAdd = pickupNodes[i];
                cups.AddAfterNode(destinationNode, nodeToAdd);
                destinationNode = nodeToAdd;
            }
            currentNode = currentNode.Next ?? cups.Head;
        }

        // create a list of values from the linkedlist, with the retained order.
        var node = cups.Head;
        var result = new List<uint>();
        do
        {
            result.Add(node.Value);
            node = node.Next;
        }
        while (node != null);
        return result;
    }
}
