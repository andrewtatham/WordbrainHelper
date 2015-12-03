using System.Collections.Generic;
using System.Linq;

namespace WordbrainHelper
{
    public static class PermutationHelper
    {
        public static IEnumerable<string> GeneratePermutations(string input)
        {
            return Permutations(input.ToCharArray())
                .Select(arr => new string(arr.ToArray()));
        }

        private static IEnumerable<IList<T>> Permutations<T>(IList<T> list)
        {
            var perms = new List<IList<T>>();
            if (list.Count == 0)
                return perms; // Empty list.
            var factorial = 1;
            for (var i = 2; i <= list.Count; i++)
                factorial *= i;
            for (var v = 0; v < factorial; v++)
            {
                var s = new List<T>(list);
                var k = v;
                for (var j = 2; j <= list.Count; j++)
                {
                    var other = k%j;
                    var temp = s[j - 1];
                    s[j - 1] = s[other];
                    s[other] = temp;
                    k = k/j;
                }
                perms.Add(s);
            }
            return perms;
        }
    }
}