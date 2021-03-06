﻿namespace WordbrainHelper
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
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }

        public override string ToString()
        {
            return $"[{X},{Y}:{Letter}]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            return Letter == other.Letter && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return Letter.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}