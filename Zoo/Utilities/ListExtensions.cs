using System.Collections.Generic;

namespace Zoo.Utilities
{
    public static class ListExtensions
    {
        /// <summary>
        /// Adds an object to the end of the <see cref="List{T}"/> if the conditional expression returns true.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="List{T}"/>. The value can be null for reference types.</param>
        /// <param name="condition">The conditional expression to evaluate.</param>
        public static void AddWithCondition<T>(this List<T> list, T item, bool condition)
        {
            if (condition)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Adds multiple objects to the end of the <see cref="List{T}"/>.
        /// </summary>
        /// <param name="items">The objects to be added to the end of the <see cref="List{T}"/>. The values can be null for reference types.</param>
        public static void AddMultiple<T>(this List<T> list, params T[] items)
        {
            foreach (T item in items)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collections to the end of the <see cref="List{T}"/>.
        /// </summary>
        /// <param name="collections">The collections whose elements should be added to the end of the <see cref="List{T}"/>. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public static void AddRanges<T>(this List<T> list, params IEnumerable<T>[] collections)
        {
            foreach (List<T> collection in collections)
            {
                list.AddRange(collection);
            }
        }
    }
}
