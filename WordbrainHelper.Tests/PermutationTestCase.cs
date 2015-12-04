namespace WordbrainHelper.Tests
{
    public class PermutationTestCase
    {
        public PermutationTestCase(string input, string[] expected)
        {
            Input = input;
            Expected = expected;
        }

        public string[] Expected { get; private set; }
        public string Input { get; private set; }
    }
}