using System.Collections.Generic;
using System.IO;

namespace WordbrainHelper
{
    public static class DictionaryHelper
    {
        private static readonly HashSet<string> Words;

        static DictionaryHelper()
        {
            Words = new HashSet<string>(File.ReadAllLines(@"wordsEn.txt"));
        }


        public static bool IsAWord(string permutation)
        {
            return Words.Contains(permutation.ToLowerInvariant());
        }
    }
}