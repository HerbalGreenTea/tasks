using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right - 1 == left) return left;
            var m = (left + right) / 2;
            if (string.Compare(phrases[m], prefix, StringComparison.OrdinalIgnoreCase) < 0)
                return GetLeftBorderIndex(phrases, prefix, m, right);
            else
                return GetLeftBorderIndex(phrases, prefix, left, m);
        }
    }
}