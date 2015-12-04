using System.Collections.Generic;
using System.Linq;

namespace WordbrainHelper
{
    public static class WordbrainHelper
    {
        public static WordbrainOutput Solve(WordbrainInput input)
        {
            var lettersOnly = input.Input.Replace(",", string.Empty);

            var grid = new GridNavigator(input.Input);


            var possibleWords = input.WordLengths.Distinct()
                .SelectMany(
                    wordLength => DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, lettersOnly))
                .ToList();


            var foundWords = possibleWords
                .Where(word => grid.TryFindWord(word))
                .ToArray();


            return new WordbrainOutput(new[] {foundWords});
        }

        public static IEnumerable<string> ApplySplit(string permutation, int[] wordLengths)
        {
            var word = new string(permutation.Take(wordLengths[0]).ToArray());

            if (wordLengths.Length == 1)
            {
                return new[] {word};
            }
            return new[]
            {
                word
            }.Union(ApplySplit(
                new string(permutation.Skip(wordLengths[0]).ToArray()),
                wordLengths.Skip(1).ToArray())
                );
        }
    }
}