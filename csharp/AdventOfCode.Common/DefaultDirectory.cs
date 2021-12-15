using System.Collections.Generic;

namespace AdventOfCode.Common;
public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
	private readonly TValue _value;
	public DefaultDictionary(TValue value)
	{
		_value = value;
	}

	public new TValue this[TKey key]
	{
		get => TryGetValue(key, out var value) ? value : _value;
		set => base[key] = value;
	}
}
