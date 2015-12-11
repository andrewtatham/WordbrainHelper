using System.Collections.Generic;

namespace WordbrainHelper
{
    public class WordbrainInput
    {
        public WordbrainInput(string grid, List<int> wordLengths)
        {
            Grid = grid;

            WordLengths = wordLengths;
        }

        public string Grid { get; private set; }
        public List<int> WordLengths { get; private set; }
        public override string ToString()
        {
            return Grid;
        }
    }
}