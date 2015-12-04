namespace WordbrainHelper
{
    public class Cell
    {
        public Cell(char letter, int x, int y)
        {
            Letter = letter;
            X = x;
            Y = y;
        }

        public char Letter { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Visited { get; set; }
    }
}