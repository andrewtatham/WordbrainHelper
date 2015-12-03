namespace WordbrainHelper
{
    public class WordbrainInput
    {
        public WordbrainInput(string input, int[] words)
        {
            Input = input;

            Words = words;
        }

        public string Input { get; private set; }
        public int[] Words { get; private set; }
    }
}