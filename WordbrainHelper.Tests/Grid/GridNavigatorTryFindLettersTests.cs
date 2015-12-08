using System.Linq;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class GridNavigatorTryFindLettersTests
    {
        // ABC
        // DEF
        // GHI
        private readonly GridNavigator _grid = new GridNavigator("ABC,DEF,GHI");


        [Test]
        [TestCase("A", true)]
        [TestCase("Z", false)]
        [TestCase("AB", true)]
        [TestCase("BC", true)]


        // Right To Left
        [TestCase("ABC", true)]
        [TestCase("DEF", true)]
        [TestCase("GHI", true)]
        // Left To Right
        [TestCase("CBA", true)]
        [TestCase("FED", true)]
        [TestCase("IHG", true)]
        // Top to bottom
        [TestCase("ADG", true)]
        [TestCase("BEH", true)]
        [TestCase("CFI", true)]
        // Bottom to Top
        [TestCase("GDA", true)]
        [TestCase("HEB", true)]
        [TestCase("IFC", true)]
        // Diagnols
        [TestCase("AEI", true)]
        [TestCase("IEA", true)]
        [TestCase("CEG", true)]
        [TestCase("GEC", true)]


        // invalid
        [TestCase("ABA", false)]
        [TestCase("ACI", false)]
        [TestCase("GBE", false)]
        public void TryFindLetters(string word, bool expected)
        {
            var actual = _grid.TryFindLetters(word);

            Assert.AreEqual(expected, actual.SingleOrDefault() != null, word);
        }


    }
}