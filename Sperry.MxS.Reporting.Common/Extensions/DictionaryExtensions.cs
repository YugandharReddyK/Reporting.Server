using System;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class DictionaryExtensions
    {
		public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> source, bool overwrite)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source), "Source Collection cannot be null");
			}
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target), "Target Collection cannot be null");
			}

			foreach (var keyValuePair in source)
			{
				if (!target.ContainsKey(keyValuePair.Key))
				{
					target.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (overwrite)
				{
					target[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}
	}
}
