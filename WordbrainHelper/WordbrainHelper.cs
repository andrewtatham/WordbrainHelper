using System.Collections.Generic;
using System.Linq;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public static class WordbrainHelper
    {
        public static WordbrainOutput Solve(WordbrainInput input)
        {

            var grid = new GridNavigator(input.Input);

            var letters = grid.GetRemainingLetters();

            var possibleWords = input.WordLengths.Distinct()
                .SelectMany(
                    wordLength => DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, letters))
                    .OrderByDescending(word => word.Length)
                .ToList();


            var foundWords = possibleWords
                .SelectMany(word => grid.TryFindWord(word))
                .Where(word => word.Found)
                .ToArray();

            var candidateSolutions = new List<CandidateSolution>();

            foreach (var foundWord in foundWords)
            {
                var candidateSolution = new CandidateSolution();

                var wordLenths2 = new List<int>(input.WordLengths);
                wordLenths2.Remove(foundWord.Word.Length);


                // TODO new grid
                grid.RemoveCells(foundWord.Path);
                var letters2 = grid.GetRemainingLetters();



                var possibleWords2 = input.WordLengths.Distinct()
                    .SelectMany(
                        wordLength => DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, letters2))
                        .OrderByDescending(word => word.Length)
                    .ToList();


                var foundWords2 = possibleWords2
                    .SelectMany(word => grid.TryFindWord(word))
                    .Where(word => word.Found)
                    .ToArray();

                foreach (var foundWord2 in foundWords2)
                {
                    candidateSolution.Add(foundWord2);

                }

                candidateSolutions.Add(candidateSolution);

            }


            return new WordbrainOutput(candidateSolutions);
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