using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordbrainHelper.Grid
{
    public class FoundWord
    {
        public FoundWord(string word, bool found = false, Path path = null)
        {
            Word = word;
            Found = found;
            Path = path ?? new Path();
        }

        public string Word { get; set; }

        public bool Found { get; set; }

        public Path Path { get; set; }

        public override string ToString()
        {
            return $"{Word} {Path}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as FoundWord;
            return Word == other.Word && Path.Equals(other.Path);
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode() ^ Path.GetHashCode();
        }
    }
}
