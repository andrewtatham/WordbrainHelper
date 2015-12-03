namespace WordbrainHelper
{
    public class Cell
    {
        public Cell(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; private set; }
    }
}