using System.Linq;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class PermutationHelperTests
    {
        public PermutationTestCase[] TestCases =
        {
            new PermutationTestCase("A", new[]
            {
                "A"
            }),
            new PermutationTestCase("AB", new[]
            {
                "AB",
                "BA"
            }),
            new PermutationTestCase("ABC", new[]
            {
                "ABC", "ACB",
                "BAC", "BCA",
                "CAB", "CBA"
            }),
            new PermutationTestCase("ABCD", new[]
            {
                "ABCD", "ABDC", "ACBD", "ACDB", "ADBC", "ADCB",
                "BACD", "BADC", "BCAD", "BCDA", "BDAC", "BDCA",
                "CABD", "CADB", "CBAD", "CBDA", "CDAB", "CDBA",
                "DABC", "DACB", "DBAC", "DBCA", "DCAB", "DCBA"
            }),
            new PermutationTestCase("AA", new[]
            {
                "AA", "AA"
            })
        };

        [Test]
        [TestCaseSource("TestCases")]
        public void GeneratePermutations(PermutationTestCase testCase)
        {
            var permutations = PermutationHelper.GeneratePermutations(testCase.Input).ToArray();

            CollectionAssert.IsNotEmpty(permutations);
            CollectionAssert.AreEquivalent(testCase.Expected, permutations);
        }
    }
}