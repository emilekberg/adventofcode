using AdventOfCode.Common;
using System.Numerics;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/16
/// </Summary> 
public class Day16 : BaseDay<string, long>, IDay
{
	public static ReadOnlySpan<char> Parse(string input)
	{
		var hexToBitString = (char c) => Convert.ToString(Convert.ToInt16(c.ToString(), 16), 2).PadLeft(4, '0');
		var a = input.ToCharArray()
			.Select(c => hexToBitString(c))
			.SelectMany(x => x)
			.ToArray();

		var span = new Span<char>(a);
		return span;
	}
	public override long Part1(string input)
	{
		var span = Parse(input);
		var packet = new BitPacket(span);
		var (version, _) = ProcessPacket(ref packet);
		return version;
	}
	public override long Part2(string input)
	{
		var span = Parse(input);
		var packet = new BitPacket(span);
		var (_, value) = ProcessPacket(ref packet);
		return value;
	}
	public static (long version, long value) ProcessPacket(ref BitPacket packet)
	{
		var version = packet.GetNextInt(3);
		var typeId = packet.GetNextEnum<PacketType>(3);
		(long version, long value) data = typeId switch
		{
			PacketType.Literal => ProcessLiteralPacket(ref packet),
			_ => ProcessOperatorPacket(ref packet, typeId),
		};
		return data with { version = data.version + version };
	}
	public static (long version, long value) ProcessOperatorPacket(ref BitPacket packet, PacketType type)
	{
		var lengthTypeId = packet.GetNextInt(1);
		var data = new List<(long version, long value)>();
		switch(lengthTypeId)
		{
			case 0:
				var totalLengthAsBits = packet.GetNextInt(15);
				var target = packet.GetOffset() + totalLengthAsBits;
				while(packet.GetOffset() < target)
				{
					data.Add(ProcessPacket(ref packet));
				}
				break;
			case 1:
				var numberOfSubPackets = packet.GetNextInt(11);
				for(int i = 0; i < numberOfSubPackets; i++)
				{
					data.Add(ProcessPacket(ref packet));
				}
				break;
		}
		var version = data.Aggregate(0L, (acc, next) => acc + next.version);
		var value = CalculateOperator(data.Select(x => x.value).ToList(), type);
		return (version, value);
	}
	public static long CalculateOperator(List<long> data, PacketType type)
	{
		return type switch
		{
			PacketType.Sum => data.Aggregate(0L, (acc, next) => acc + next),
			PacketType.Product => data.Aggregate(1L, (acc, next) => acc * next),
			PacketType.Minimum => data.Min(),
			PacketType.Maximum => data.Max(),
			PacketType.GreaterThan => data[0] > data[1] ? 1 : 0,
			PacketType.LessThan => data[0] < data[1] ? 1 : 0,
			PacketType.Equals => data[0] == data[1] ? 1 : 0,
			_ => throw new ArgumentException("unknown argument" + type.ToString(), nameof(type))
		};
	}
	public static (long version, long value) ProcessLiteralPacket(ref BitPacket packet)
	{
		var data = new List<char>();
		int keepReadingBit;
		do
		{
			keepReadingBit = packet.GetNextInt(1);
			var readData = packet.GetNext(4);
			data.AddRange(readData.ToArray());
		}
		while (keepReadingBit == 1);
		var s = string.Join("", data);
		long i = Convert.ToInt64(s, 2);
		return (0, i);
	}
}
public enum PacketType
{
	Sum = 0,
	Product = 1,
	Minimum = 2,
	Maximum = 3,
	Literal = 4,
	GreaterThan = 5,
	LessThan = 6,
	Equals = 7
}
public ref struct BitPacket
{
	private readonly ReadOnlySpan<char> _span;
	private int _offset = 0;
	public BitPacket(ReadOnlySpan<char> span)
	{
		_span = span;
	}

	public ReadOnlySpan<char> GetNext(int size)
	{
		var result = _span.Slice(_offset, size);
		_offset += size;
		return result;
	}

	public int GetOffset() => _offset;
	public ReadOnlySpan<char> GetSpanCopy() => _span;

	public int GetNextInt(int size)
	{
		var result = _span.Slice(_offset, size);
		_offset += size;

		var digit = Convert.ToInt16(string.Join("", result.ToArray()), 2);
		return digit;
	}
	public T GetNextEnum<T>(int size) where T : struct, Enum
	{
		if(!typeof(T).IsEnum)
		{
			throw new ArgumentException("Must be an enum", nameof(T));
		}
		var result = GetNextInt(3);
		return (T)(object)result;
	}
}


