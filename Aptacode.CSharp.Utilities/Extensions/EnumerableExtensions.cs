using System;
using System.Collections.Generic;
using System.Linq;

namespace Aptacode.CSharp.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rng = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = Rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
        {
            return source.Skip(Math.Max(0, source.Count() - count));
        }
    }
}