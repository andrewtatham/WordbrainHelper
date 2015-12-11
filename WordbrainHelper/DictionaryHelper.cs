using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Xml.Schema;

namespace WordbrainHelper
{
    public static class DictionaryHelper
    {
        private static readonly Dictionary<int, List<MyWord>> WordsByLength;

        static DictionaryHelper()
        {
            var words = DictionaryEn.wordsEn
                .Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToUpperInvariant())
                .Select(word => new MyWord(word))
                .ToList();
                       
            WordsByLength = words.GroupBy(k => k.WordLength).ToDictionary(k => k.Key, v => v.ToList());
        }

        public static IEnumerable<string> GetWordsByLengthContainingOnlyLetters(int length, string letters)
        {
            var chars = letters.ToCharArray();
            return WordsByLength[length]
                .Where(word => word.Letters.IsSubsetOf(chars))
                .Select(word => word.Word);
        }
    }

    public class MyWord
    {
        public MyWord(string word)
        {
            Word = word;
            WordLength = word.Length;
            Letters = new HashSet<char>(word);


        }

        public HashSet<char> Letters { get; set; }

        public int WordLength { get; set; }

        public string Word { get; set; }
    }
}