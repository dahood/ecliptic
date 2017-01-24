using System;
using System.Collections.Generic;

namespace EclipticLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var eachItem in items)
            {
                action(eachItem);
            }
        }
    }
}
