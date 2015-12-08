using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace WordbrainHelper.Grid
{
    public class Paths : List<Path>
    {
        public override string ToString()
        {
            if (this.Any())
            {
                return this.Select(path => path.ToString()).Aggregate((p1, p2) => p1 + p2);
            }
            else
            {
                return "<Empty>";

            }
           
        }
    }
}
