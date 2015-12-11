using System.Collections.Generic;

namespace WordbrainHelper
{
    public class WordbrainOutput
    {
        public WordbrainOutput(IEnumerable<CandidateSolution> candidates)
        {
            Candidates = candidates;
        }

        public IEnumerable<CandidateSolution> Candidates { get; private set; }
    }
}