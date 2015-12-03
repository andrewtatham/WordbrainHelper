using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WordbrainHelper
{
    public class GridNavigator
    {
        private int _n;
        private readonly Cell[,] _cellsXY;
        private readonly Dictionary<Char,Cell[]> _cellsByLetter;

        public GridNavigator(string grid)
        {
            var rows = grid.Split(',');
            _n = rows.Length;

            var cells = new List<Cell>();

            _cellsXY = new Cell[_n, _n];
            for (var y = 0; y < _n; y++)
            {
                for (var x = 0; x < _n; x++)
                {
                    var cell = new Cell(grid[y * (_n + 1) + x], x, y);
                    cells.Add(cell);
                    _cellsXY[x, y] = cell;
                }
            }

            _cellsByLetter = cells
                .GroupBy(cell => cell.Letter)
                .ToDictionary(k => k.Key, v => v.ToArray());

        }


        public bool CanIFind(string word, int n = 0, Cell startFrom = null)
        {
            if (startFrom == null)
            {
                // first letter
                if (_cellsByLetter.ContainsKey(word[0]))                    
                    return _cellsByLetter[word[0]].Any(cell => CanIFind(word, n + 1, cell));
                else return false;
            }
            else if (n < word.Length)
            {
                // Find next letter

                char letter = word[n];

                

                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    char? peekLetter = PeekCellLetter(startFrom, direction);
     

                    if (peekLetter.HasValue && peekLetter.Value == letter)
                    {
                        var nextCell = GetNextCell(startFrom, direction);
                        return CanIFind(word, n + 1, nextCell);
                    }
                }
                return false;

            }
            else
            {
                // word found
                return true;
            }


           
                
        }

        public Cell GetNextCell(Cell startFrom, Direction direction)
        {
            int x = startFrom.X;
            int y = startFrom.Y;



            switch (direction)
            {
                case Direction.Up:
                    y = y - 1;
                    break;
                case Direction.Down:
                    y = y + 1;
                    break;
                case Direction.Left:
                    x = x - 1;
                    break;
                case Direction.Right:
                    x = x + 1;
                    break;
                case Direction.UpLeft:
                    x = x - 1;
                    y = y - 1;
                    break;
                case Direction.UpRight:
                    y = y - 1;
                    x = x + 1;
                    break;
                case Direction.DownLeft:
                    x = x - 1;
                    y = y + 1;
                    break;
                case Direction.DownRight:
                    y = y + 1;
                    x = x + 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            if (0 <= x && x < _n && 0 <= y && y < _n)
            {
                return _cellsXY[x, y];
            }
            else
                return null;
        }

        public char? PeekCellLetter(Cell startFrom, Direction direction)
        {
            var nextCell = GetNextCell(startFrom, direction);
            if (nextCell != null)
                return nextCell.Letter;
            else
            {
                return null;
            }
        }
    }
}
