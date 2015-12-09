using System.Collections.Generic;

namespace WordbrainHelper
{
    public class WordbrainInput
    {
        public WordbrainInput(string input, List<int> wordLengths)
        {
            Input = input;

            WordLengths = wordLengths;
        }

        public string Input { get; private set; }
        public List<int> WordLengths { get; private set; }
        public override string ToString()
        {
            return Input;
        }
    }
}