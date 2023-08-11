using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class ListExtension
    {
        public static bool NotAllofNullOrZero(this IEnumerable<double?> values)
        {
            foreach (double? value in values)
            {
                if (value.HasValue && value != 0.0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAllNull(this IEnumerable<double?> values)
        {
            foreach (double? value in values)
            {
                if (value.HasValue)
                {
                    return false;
                }
            }

            return true;
        }

        public static int RemoveAll<T>(this List<T> destinationCollection, Func<T, bool> condition)
        {
            List<T> list = destinationCollection.Where(condition).ToList();
            foreach (T item in list)
            {
                destinationCollection.Remove(item);
            }

            return list.Count;
        }

        public static void AddRange<T>(this List<T> destinationCollection, IEnumerable<T> sourceCollection)
        {
            if (sourceCollection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (T item in sourceCollection)
            {
                destinationCollection.Add(item);
            }
        }

        public static int Remove<T>(this List<T> coll, Func<T, bool> condition)
        {
            List<T> list = coll.Where(condition).ToList();
            foreach (T item in list)
            {
                coll.Remove(item);
            }

            return list.Count;
        }

        public static int Replace<T>(this IList<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var index = source.IndexOf(oldValue);
            if (index != -1)
                source[index] = newValue;
            return index;
        }
    }

}
