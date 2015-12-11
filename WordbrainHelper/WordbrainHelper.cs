using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public static class WordbrainHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(WordbrainHelper));


        public static WordbrainOutput Solve(WordbrainInput input)
        {
            var cs = new CandidateSolution(input);


            var output = new WordbrainOutput(TrySolve(cs));

            Log.InfoFormat("returning {0}", output.Candidates.Select(c => c.ToString()).Aggregate((cs1, cs2) => cs1 + Environment.NewLine + cs2));


            return output;
        }

        private static IEnumerable<CandidateSolution> TrySolve(CandidateSolution cs)
        {

            var grid = new GridNavigator(cs.GridState);
            Log.DebugFormat("Grid: {0} ", cs.GridState);

            var letters = grid.GetRemainingLetters();
            Log.DebugFormat("letters: {0} ", letters);

            var possibleWords = cs.WordLengths.Distinct().OrderByDescending(wordLength => wordLength)
                .SelectMany(wordLength => DictionaryHelper.GetWordsByLengthContainingOnlyLetters(wordLength, letters))
                .SelectMany(word => grid.TryFindWord(word))
                .Where(word => word.Found)
                .ToList();

            var candidateSolutions = new List<CandidateSolution>();

            foreach (var possibleWord in possibleWords)
                Log.InfoFormat("Current: {0} Found: {1}", cs, possibleWord);

            foreach (var possibleWord in possibleWords)
            {
                CandidateSolution newCs = new CandidateSolution(cs);

                newCs.AddWord(possibleWord);
                newCs.RemoveCells(possibleWord);
                newCs.RemoveWordLength(possibleWord);

                if (newCs.WordLengths.Any())
                {
                    candidateSolutions.AddRange(TrySolve(newCs));
                }
                else
                {
                    candidateSolutions.Add(newCs);
                }
            }

            candidateSolutions = candidateSolutions.Distinct().ToList();

            if (candidateSolutions.Any())
            {
                Log.DebugFormat("returning {0}", candidateSolutions.Select(c => c.ToString()).Aggregate((cs1,cs2) => cs1 + Environment.NewLine + cs2));
            }
            return candidateSolutions;
        }
    }
}