﻿using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace WordbrainHelper
{
    public class GridNavigator
    {
        private readonly List<Cell> _cells;
        private readonly Dictionary<char, Cell[]> _cellsByLetter;
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
                    var cell = new Cell(grid[y*(_n + 1) + x], x, y);
                    _cells.Add(cell);
                    _cellsXY[x, y] = cell;
                }
            }

            _cellsByLetter = _cells
                .GroupBy(cell => cell.Letter)
                .ToDictionary(k => k.Key, v => v.ToArray());
        }


        public bool TryFindWord(string word, int n = 0, Cell startFrom = null)
        {
            bool success = false;


            if (startFrom == null)
            {
                _cells.ForEach(cell => cell.Visited = false);

                // first letter
                if (_cellsByLetter.ContainsKey(word[0]))
                {
                    foreach (var cell in _cellsByLetter[word[0]])
                    {
                        
                        Log.DebugFormat("Trying to find {0} starting from cell {1}", word, cell);
                        bool successTemp = TryFindWord(word, n + 1, cell);
                        if (successTemp)
                        {
                            cell.Visited = true;
                        }
                        Log.DebugFormat("{0} finding {1} starting from cell {2}", successTemp ? "Success":"Fail", word, cell);
                        success |= successTemp;
                    }
                    
                }

                return success;
            }
            if (n < word.Length)
            {
                // Find next letter
                var letter = word[n];
                Log.DebugFormat("Trying to find {0} in {1}", letter, word);

                foreach (Direction direction in Enum.GetValues(typeof (Direction)))
                {
                    var peekLetter = PeekCellLetter(startFrom, direction);
                    if (peekLetter.HasValue && peekLetter.Value == letter)
                    {
                        startFrom.Visited = true;
                        Log.DebugFormat("Found {0} by looking {1}", letter, direction);
                        var nextCell = GetNextCell(startFrom, direction);                        
                        var successTemp = TryFindWord(word, n + 1, nextCell);
                        success |= successTemp;
                    }
                }
                return success;
            }
            // word found
            return true;
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
    }
}