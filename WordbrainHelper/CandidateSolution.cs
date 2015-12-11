using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public class CandidateSolution
    {
        public List<int> WordLengths { get; private set; } 
        public string GridState { get; private set; }

        public List<FoundWord> FoundWords { get; private set; } = new List<FoundWord>();

        public CandidateSolution(WordbrainInput input)
        {
            this.GridState = input.Grid;
            this.WordLengths = new List<int>(input.WordLengths);
        }
        public CandidateSolution(CandidateSolution other)
        {
            this.FoundWords = new List<FoundWord>(other.FoundWords);
            this.GridState = other.GridState;
            this.WordLengths = new List<int>(other.WordLengths);
        }


        public override bool Equals(object obj)
        {
            var other = obj as CandidateSolution;
            return WordLengths.Equals(other.WordLengths)
                && GridState == other.GridState
                && FoundWords.Zip(other.FoundWords, (thisWord, otherWord) => new { ThisWord = thisWord, OtherWord = otherWord })
                .All(words => words.ThisWord.Equals(words.OtherWord));
        }

        public override int GetHashCode()
        {
            return GridState.GetHashCode() 
                ^ (WordLengths.Any() ?  WordLengths.Select(wl => wl.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2) : 0)
                ^ (FoundWords.Any() ? FoundWords.Select(foundWord => foundWord.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2) : 0);
        }
        public override string ToString()
        {
            return FoundWords.Any() ? FoundWords.Select(foundWord => foundWord.ToString()).Aggregate((h1, h2) => h1 + " " + h2) : "";
        }

        public void AddWord(FoundWord possibleWord)
        {
            FoundWords.Add(possibleWord);
        }

        public void RemoveCells(FoundWord possibleWord)
        {
            var newgrid = new GridNavigator(GridState);
            newgrid.RemoveCells(possibleWord.Path);
            GridState = newgrid.GetGridState();
       
        }

        public void RemoveWordLength(FoundWord possibleWord)
        {
            WordLengths.Remove(possibleWord.Word.Length);
        }

    }
}
