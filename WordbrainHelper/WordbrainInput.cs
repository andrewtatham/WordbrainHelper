namespace WordbrainHelper
{
    public class WordbrainInput
    {
        public WordbrainInput(string input, int[] wordLengths)
        {
            Input = input;

            WordLengths = wordLengths;
        }

        public string Input { get; private set; }
        public int[] WordLengths { get; private set; }
        public override string ToString()
        {
            return Input;
        }
    }
}