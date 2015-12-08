using System.Collections.Generic;

namespace WordbrainHelper
{
    public class WordbrainOutput
    {
        public WordbrainOutput(List<CandidateSolution> candidates)
        {
            Candidates = candidates;
        }

        public List<CandidateSolution> Candidates { get; private set; }
    }
}