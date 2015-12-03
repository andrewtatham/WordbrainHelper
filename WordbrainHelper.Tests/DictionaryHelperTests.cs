using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class DictionaryHelperTests
    {


        [Test]
        [TestCase("PYTHAGORAS", true)]
        [TestCase("XYZ", false)]
        public void IsAWord(string word, bool expected)
        {
            Assert.AreEqual(expected, DictionaryHelper.IsAWord(word));
            
        }
        [Test]
        [TestCase(3, "KYS", new []{"SKY"})]
        public void GetWordsByLengthContainingOnlyLetters(int length, string letters, string[] expected)
        {
            CollectionAssert.AreEquivalent(expected, DictionaryHelper.GetWordsByLengthContainingOnlyLetters(length, letters));

        }
    }
}
