using System;
using System.Collections.Generic;

namespace Common.Extensions;

public static class DictionaryExtensions
{
	public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
		Action<KeyValuePair<TKey, TValue>> action) where TKey : notnull
	{
		foreach (var keyValuePair in dictionary)
		{
			action(keyValuePair!);
		}
	}
}