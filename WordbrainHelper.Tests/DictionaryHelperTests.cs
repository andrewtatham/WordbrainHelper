using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class DictionaryHelperTests
    {
        [Test]
        [TestCase(3, "KYS", new[] {"SKY"})]
        public void GetWordsByLengthContainingOnlyLetters(int length, string letters, string[] expected)
        {
            CollectionAssert.AreEquivalent(expected,
                DictionaryHelper.GetWordsByLengthContainingOnlyLetters(length, letters));
        }


        [Test]
        [TestCase("PYTHAGORAS", true)]
        [TestCase("XYZ", false)]
        public void IsAWord(string word, bool expected)
        {
            Assert.AreEqual(expected, DictionaryHelper.IsAWord(word));
        }
    }
}