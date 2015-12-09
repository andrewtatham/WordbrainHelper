using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using log4net.Core;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public class GridNavigator
    {
        private readonly List<Cell> _cells;
        private readonly Dictionary<char, List<Cell>> _cellsByLetter;
        private readonly Cell[,] _cellsXY;
        private readonly int _n;

        private static readonly ILog Log = LogManager.GetLogger(nameof(GridNavigator));



        public GridNavigator(string grid)
        {



            var rows = grid.Split(',');
            _n = rows.Length;

            _cells = new List<Cell>();

            _cellsXY = new Cell[_n, _n];
            for (var y = 0; y < _n; y++)
            {
                for (var x = 0; x < _n; x++)
                {
                    var letter = grid[y*(_n + 1) + x];
                    if (char.IsLetter(letter))
                    {
                        var cell = new Cell(letter, x, y);
                        _cells.Add(cell);
                        _cellsXY[x, y] = cell;
                    }         
                }
            }

            _cellsByLetter = _cells
                .GroupBy(cell => cell.Letter)
                .ToDictionary(k => k.Key, v => v.ToList());
        }

        public List<FoundWord> TryFindWord(string word)
        {
            var paths = TryFindLetters(word);

            if (paths.Any())
            {
                return paths.Select(path => new FoundWord(word, true, path)).ToList();
            }
            else
            {
                return new List<FoundWord>() { new FoundWord(word, false, null)}; 
            }

        }



        public Paths TryFindLetters(string word, int n = 0, Path path = null)
        {
            var paths = new Paths();

            if (path == null)
            {
                _cells.ForEach(cell => cell.Visited = false);

                // first letter
                if (_cellsByLetter.ContainsKey(word[0]))
                {
                    foreach (var cell in _cellsByLetter[word[0]])
                    {
                    
                        paths.AddRange(TryFindLetters(word, n + 1, new Path() {cell}));
                    }
                }
                if (paths.Any())
                {
                    Log.DebugFormat("Returning paths 1 {0}", paths);                    
                }
                return paths;
            }

            if (n < word.Length)
            {
                // Find next letter
                var letter = word[n];
                var startFrom = path.Last();

                Log.DebugFormat("Trying to find {0} in {1} starting from {2}", letter, word, startFrom);

                foreach (Direction direction in Enum.GetValues(typeof (Direction)))
                {
                    var peekLetter = PeekCellLetter(startFrom, direction);
                    if (peekLetter.HasValue && peekLetter.Value == letter)
                    {
                        startFrom.Visited = true;
                        Log.DebugFormat("Found {0} by looking {1}", letter, direction);
                        var nextCell = GetNextCell(startFrom, direction);

                        var newPath = new Path(path);
                        newPath.Add(nextCell);
                        paths.AddRange(TryFindLetters(word, n + 1, newPath));
                    }
                }
                if (paths.Any())
                {
                    Log.DebugFormat("Returning paths 2 {0}", paths);
                }
                return paths;
            }
            // word found

            paths.Add(path);
            Log.DebugFormat("Returning paths 3 {0}", paths);
            return paths;
        }


        public Cell GetNextCell(Cell startFrom, Direction direction)
        {
            var x = startFrom.X;
            var y = startFrom.Y;


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
            return null;
        }

        public char? PeekCellLetter(Cell startFrom, Direction direction)
        {
            var nextCell = GetNextCell(startFrom, direction);
            if (nextCell != null && !nextCell.Visited)
                return nextCell.Letter;
            return null;
        }

        public string GetRemainingLetters()
        {
            return new string(_cells.Select(cell => cell.Letter).ToArray());
        }

        public void RemoveCells(Path foundWordPath)
        {
            foreach (var foundWordCell in foundWordPath)
            {
                _cells.Remove(foundWordCell);
                _cellsXY[foundWordCell.X, foundWordCell.Y] = null;
                _cellsByLetter[foundWordCell.Letter].Remove(foundWordCell);

                
            }

            // drop cells

            for (int x = 0; x < _n; x++)
            {
                for (int y = _n-1; y > 0; y--)
                {
                    var cellabove = _cellsXY[x, y - 1];
                    var cell = _cellsXY[x, y];
                    if (cell == null && cellabove != null)
                    {
                        _cellsXY[x, y - 1] = null;
                        cell = cellabove;
                        cell.X = x;
                        cell.Y = y;
                        _cellsXY[x, y] = cell;
                    }


                }

            }


        }

        public string GetGridState()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < _n; y++)
            {
                if (y!=0)
                {
                    sb.Append(',');
                }
                for (int x = 0; x < _n; x++)
                {
                    var cell = _cellsXY[x, y];
                    if (cell == null)
                    {

                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(cell.Letter);
                    }

                }
                 
            }
            return sb.ToString();
        }
    }
}