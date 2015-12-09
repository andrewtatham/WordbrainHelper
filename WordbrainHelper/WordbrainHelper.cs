using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using log4net;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public static class WordbrainHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(WordbrainHelper));


        public static WordbrainOutput Solve(WordbrainInput input)
        {
            return new WordbrainOutput(TrySolve(input.Input, input.WordLengths));
        }

        private static List<CandidateSolution> TrySolve(string input, List<int> wordLengths, CandidateSolution cs = null)
        {
            var candidateSolutions = new List<CandidateSolution>();

            var grid = new GridNavigator(input);
            var letters = grid.GetRemainingLetters();
            foreach (var wordLength in wordLengths.Distinct())
            {
                var newWordLengths = new List<int>(wordLengths);
                newWordLengths.Remove(wordLength);

                var possibleWords = DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, letters)
                    .SelectMany(word => grid.TryFindWord(word))
                    .Where(word => word.Found);

                foreach (var possibleWord in possibleWords)
                {              
                    if(cs == null) cs = new CandidateSolution();
                    cs.Add(possibleWord);
                    candidateSolutions.Add(cs);

                    var newgrid = new GridNavigator(grid.GetGridState());
                    newgrid.RemoveCells(possibleWord.Path);

                    if (newWordLengths.Any())
                    {
                        var state = newgrid.GetGridState();
                        Log.InfoFormat("Grid State: {0}", state);
                        candidateSolutions.AddRange(TrySolve(state, newWordLengths, cs));
                    }
              
                    
                }
            }
            if (candidateSolutions.Any())
            {
                Log.InfoFormat("returning {0}", candidateSolutions.Select(c => c.ToString()).Aggregate((cs1,cs2) => cs1 + Environment.NewLine + cs2));
            }
            return candidateSolutions;
        }
    }
}