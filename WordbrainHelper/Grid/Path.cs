using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordbrainHelper.Grid
{
    public class Path : List<Cell>
    {
        public Path()
        {
        }
        public Path(IEnumerable<Cell> collection) : base(collection)
        {
        }

        public override string ToString()
        {
            return this.Select(cell => cell.ToString()).Aggregate((c1,c2) => c1 + c2);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Path;
            return this.Zip(other, (thisCell, otherCell) => new {ThisCell = thisCell, OtherCell = otherCell}).All(cells => cells.ThisCell.Equals(cells.OtherCell));
        }

        public override int GetHashCode()
        {
            return this.Select(cell => cell.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
        }
    }
}
