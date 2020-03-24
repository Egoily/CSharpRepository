using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ee.Core.Framework.Caching
{
    public class ValueCache<TKey, TValue> where TValue : struct
    {
        private static readonly Dictionary<TKey, TValue> Cache = new Dictionary<TKey, TValue>();

        public Func<TKey, TValue> Converter { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TValue Convert(TKey key)
        {
            return Converter?.Invoke(key) ?? default;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (Cache.TryGetValue(key, out var value)) return value;
                value = Convert(key);
                Cache.Add(key, value);
                return value;
            }
        }
    }
}
