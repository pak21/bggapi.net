using System;
using System.Collections.Generic;

namespace WantToPlay
{
    static class DictionaryExtensions
    {
        internal static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> factory)
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = factory();
                dict.Add(key, value);
            }

            return value;
        }
    }
}
