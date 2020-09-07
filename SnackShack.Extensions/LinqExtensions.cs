using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    /// <summary>
    /// Adds extensions for Linq.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Gets items from the sequence until the item matches the predicate.
        /// </summary>
        /// <typeparam name="T">The type contained in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="predicate">The predicate used to determine when to stop the sequence.</param>
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _(); IEnumerable<T> _()
            {
                foreach (var item in source)
                {
                    yield return item;
                    if (predicate(item))
                        yield break;
                }
            }
        }
    }
}
