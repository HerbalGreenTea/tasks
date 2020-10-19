using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var actualCount = GetCountByPrefix(phrases, prefix);

            if (count > actualCount)
                count = actualCount;
            var array = new string[count];

            for (int i = left + 1, j = 0; i < count; i++, j++)
                array[j] = phrases[i];
            return array;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            int left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

            int count = right - left - 1;
            return count;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            // ...
            //CollectionAssert.IsEmpty(actualTopWords);
        }

        // ...

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            // ...
            //Assert.AreEqual(expectedCount, actualCount);
        }

        // ...
    }
}
