using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordbrainHelper.Grid;

namespace WordbrainHelper
{
    public class CandidateSolution : List<FoundWord>
    {
        public CandidateSolution()
        {
            
        }
        public CandidateSolution(IEnumerable<FoundWord> collection) : base(collection)
        {
        }

        public override bool Equals(object obj)
        {
            var other = obj as CandidateSolution;
            return this.Zip(other, (thisWord, otherWord) => new { ThisWord = thisWord, OtherWord = otherWord })
                .All(words => words.ThisWord.Equals(words.OtherWord));
        }

        public override int GetHashCode()
        {
            return this.Select(foundWord => foundWord.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
        }
        public override string ToString()
        {
            return this.Select(foundWord => foundWord.ToString()).Aggregate((h1, h2) => h1 + " " + h2);
        }
    }
}
