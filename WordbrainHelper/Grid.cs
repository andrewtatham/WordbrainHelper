using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordbrainHelper
{
    public class Grid
    {
        private readonly int _n;
        private readonly Cell[,] _cells;


        public Grid(string letters)
        {
            var rows = letters.Split(',');
            _n = rows.Length;
            _cells = new Cell[_n, _n];
            for (var y = 0; y < _n; y++)
            {
                for (var x = 0; x < _n; x++)
                {
                    _cells[x, y] = new Cell(letters[y*(_n + 1) + x]);
                }
            }
        }


        internal IEnumerable<string> GeneratePermutations()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < _n; y++)
            {
                for (var x = 0; x < _n; x++)
                {
                    sb.Append(_cells[x, y].Letter);
                }
            }


            return PermutationHelper.GeneratePermutations(sb.ToString());
        }
    }
}