namespace WordbrainHelper.Tests
{
    public class ApplySplitTestCase
    {
        public ApplySplitTestCase(string input, int[] words, string[] expected)
        {
            Input = input;
            Words = words;
            Expected = expected;
        }

        public string Input { get; private set; }
        public int[] Words { get; private set; }
        public string[] Expected { get; private set; }
    }
}