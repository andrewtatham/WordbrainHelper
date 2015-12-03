using System.Collections.Generic;
using System.Linq;

namespace WordbrainHelper
{
    public static class WordbrainHelper
    {
        public static WordbrainOutput Solve(WordbrainInput input)
        {

            string lettersOnly = input.Input.Replace(",", string.Empty);

            var grid = new GridNavigator(input.Input);



            var possibleWords = input.WordLengths.Distinct()
                .SelectMany(
                    wordLength => DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, lettersOnly))
                    .ToList();


            var candidates = possibleWords
                .Where(word => grid.CanIFind(word))
                .ToArray();


            //var permutations = grid.GeneratePermutations().ToList();

            //var splitPermutations = permutations
            //    .Select(permutation => ApplySplit(permutation, input.WordLengths).ToArray())
            //    .ToList();

            //var candidates = splitPermutations.Where(words => words.Any(DictionaryHelper.IsAWord)).ToArray();

            return new WordbrainOutput(new []{candidates});
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