using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Keane.CH.Framework.Core.ExtensionMethods
{
    /// <summary>
    /// Encapsulates IEnumerable extensions.
    /// </summary>
    public static class IEnumerableExtensionMethods
    {
        /// <summary>
        /// Determines whetehr all the items with in the collection are of the same type.
        /// </summary>
        /// <param name="enumerable">The enumerable being tested.</param>
        /// <typeparam name="T">A type.</typeparam>
        /// <returns>True if all items in the collection can be cast to the generic type.</returns>
        public static bool AreAllItemsOfSameType<T>(
            this IEnumerable enumerable)
        {
            bool result = false;
            if (enumerable != null)
            {
                foreach (object item in enumerable)
                {
                    if (item is T)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        break;                    
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Returns the number of items in a n IEnuerable collection.
        /// </summary>
        /// <param name="enumerable">The enumerable being tested.</param>
        /// <returns>The count of items within the collection.</returns>
        public static int Count(
            this IEnumerable enumerable)
        {
            int result = 0;
            if (enumerable != null)
            {
                foreach (object item in enumerable)
                {
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a customised collection from an enumeration of items.
        /// </summary>
        /// <typeparam name="C">The type of custom collection to return.</typeparam>
        /// <typeparam name="I">The type of collection item to be iterated.</typeparam>
        /// <param name="enumerable">An enumerable collection of items.</param>
        /// <returns>A custom collection populated from the passed enumerable.</returns>
        public static C AsCustomCollection<C, I>(this IEnumerable<I> enumerable)
            where C : ICollection<I>, new()
        {
            C result = new C();
            enumerable.ToList().ForEach(i => result.Add(i));
            return result;
        }
    }
}
