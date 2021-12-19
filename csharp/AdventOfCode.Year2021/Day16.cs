using AdventOfCode.Common;
using System.Buffers.Binary;
using System.Numerics;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/16
/// </Summary> 
public class Day16 : BaseDay<string, long>, IDay
{
	public static ReadOnlySpan<byte> Parse(string input)
	{
		var hexToBitString = (char c) => Convert.ToString(Convert.ToInt16(c.ToString(), 16), 2).PadLeft(4, '0');
		var data = input.ToCharArray()
			.Select(c => hexToBitString(c))
			.SelectMany(x => x)
			.Select(c => (byte)char.GetNumericValue(c))
			.ToArray();

		var span = new Span<byte>(data);
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
		var version = packet.ReadNextInt(3);
		var typeId = packet.ReadNextEnum<PacketType>(3);
		(long version, long value) data = typeId switch
		{
			PacketType.Literal => ProcessLiteralPacket(ref packet),
			_ => ProcessOperatorPacket(ref packet, typeId),
		};
		return data with { version = data.version + version };
	}
	public static (long version, long value) ProcessOperatorPacket(ref BitPacket packet, PacketType type)
	{
		var lengthTypeId = packet.ReadNextInt(1);
		var data = new List<(long version, long value)>();
		switch(lengthTypeId)
		{
			case 0:
				var totalLengthAsBits = packet.ReadNextInt(15);
				var target = packet.GetOffset() + totalLengthAsBits;
				while(packet.GetOffset() < target)
				{
					data.Add(ProcessPacket(ref packet));
				}
				break;
			case 1:
				var numberOfSubPackets = packet.ReadNextInt(11);
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
			PacketType.Sum => data.Sum(),
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
		int keepReading;
		long result = 0;
		do
		{
			keepReading = packet.ReadNextInt(1);
			var bits = packet.GetNext(4);
			foreach (var bit in bits)
			{
				result <<= 1;
				result += bit;
			}
		}
		while (keepReading == 1);
		return (0, result);
	}
}
public enum LengthTypeId
{
	Length = 0,
	Count = 1
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
	private readonly ReadOnlySpan<byte> _span;
	private int _offset = 0;
	public BitPacket(ReadOnlySpan<byte> span)
	{
		_span = span;
	}

	public ReadOnlySpan<byte> GetNext(int size)
	{
		var result = _span.Slice(_offset, size);
		_offset += size;
		return result;
	}

	public int GetOffset() => _offset;

	public int ReadNextInt(int size)
	{
		var bits = _span.Slice(_offset, size);
		_offset += size;

		int result = 0;
		foreach(var bit in bits)
		{
			result <<= 1;
			result += bit;
		}
		return result;
	}
	public T ReadNextEnum<T>(int size) where T : struct, Enum
	{
		if(!typeof(T).IsEnum)
		{
			throw new ArgumentException("Must be an enum", nameof(T));
		}
		var result = ReadNextInt(size);
		return (T)(object)result;
	}
}


