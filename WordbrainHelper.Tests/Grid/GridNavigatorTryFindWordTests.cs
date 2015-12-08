using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using WordbrainHelper.Grid;

namespace WordbrainHelper.Tests.Grid
{
    [TestFixture]
    public class GridNavigatorTryFindWordTests
    {
        // ABC
        // DEF
        // GHI
        private readonly GridNavigator _grid = new GridNavigator("DOG,O  ,G  ");


        [Test]
        public void TryFindWord()
        {
            var actual = _grid.TryFindWord("DOG");
            var expected = new List<FoundWord>()
            {
                new FoundWord("DOG",true,new Path()
                {
                    new Cell('D', 0, 0),
                    new Cell('O', 0, 1),
                    new Cell('G', 0, 2),
                }),
                new FoundWord("DOG",true,new Path()
                {
                    new Cell('D', 0, 0),
                    new Cell('O', 1, 0),
                    new Cell('G', 2, 0),
                }),

            };
            CollectionAssert.AreEqual(expected, actual);
        }


    }
}
