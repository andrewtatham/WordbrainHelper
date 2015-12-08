using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;

namespace WordbrainHelper
{
    public static class DictionaryHelper
    {
        private static readonly HashSet<string> Words;
        private static readonly Dictionary<int, List<string>> WordsByLength;

        static DictionaryHelper()
        {
            


            var words = DictionaryEn.wordsEn.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToUpperInvariant())
                .ToList();
            Words = new HashSet<string>(words);
            WordsByLength = words
                .GroupBy(k => k.Length)
                .ToDictionary(k => k.Key, v => new List<string>(v));
        }


        public static bool IsAWord(string permutation)
        {
            return Words.Contains(permutation);
        }

        public static string[] GetWordsByLengthContainingOnlyLetters(int length, string letters)
        {
            var chars = letters.ToCharArray();
            return WordsByLength[length]
                .Where(word => new HashSet<char>(word).IsSubsetOf(chars))
                .ToArray();
        }
    }
}