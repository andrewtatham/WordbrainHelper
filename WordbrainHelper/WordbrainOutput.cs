namespace WordbrainHelper
{
    public class WordbrainOutput
    {
        public WordbrainOutput(string[][] candidates)
        {
            Candidates = candidates;
        }

        public string[][] Candidates { get; private set; }
    }
}