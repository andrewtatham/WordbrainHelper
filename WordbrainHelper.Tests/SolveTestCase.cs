using System.Collections.Generic;

namespace WordbrainHelper.Tests
{
    public class SolveTestCase
    {
        public SolveTestCase(string input, List<int> words, string[] expected)
        {
            Input = new WordbrainInput(input, words);
            Expected = expected;
        }

        public WordbrainInput Input { get; private set; }
        public string[] Expected { get; private set; }

        public override string ToString()
        {
            return Input.ToString();
        }
    }
}