namespace WordbrainHelper.Tests
{
    public class SolveTestCase
    {
        public SolveTestCase(string input, int[] words, string[] expected)
        {
            Input = new WordbrainInput(input, words);
            Expected = expected;
        }

        public WordbrainInput Input { get; private set; }
        public string[] Expected { get; private set; }
    }
}