using System;
using System.Collections.Concurrent;

namespace Hattem.Api.Collections
{
    internal sealed class ObjectCache<TKey, TValue>
        where TKey : notnull
    {
        private readonly int _maxCapacity;
        private readonly ConcurrentDictionary<int, (TKey Key, TValue Value)> _values;

        public ObjectCache(int maxCapacity)
        {
            if (maxCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCapacity), "Must be positive");
            }

            _maxCapacity = maxCapacity;

            _values = new ConcurrentDictionary<int, (TKey Key, TValue Value)>(
                Environment.ProcessorCount,
                _maxCapacity);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            var index = key.GetHashCode() % _maxCapacity;
            var (cacheKey, cacheValue) = _values.GetOrAdd(index, _ => (key, valueFactory(key)));

            if (key.Equals(cacheKey))
            {
                return cacheValue;
            }

            var value = valueFactory(key);

            _values.AddOrUpdate(index, (key, value), (_, _) => (key, value));

            return value;
        }
    }
}
