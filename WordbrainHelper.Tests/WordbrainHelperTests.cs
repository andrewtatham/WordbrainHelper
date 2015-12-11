using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Config;
using NUnit.Framework;
using WordbrainHelper.TestData;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class WordbrainHelperTests
    {






        private static bool ExactMatch(SolveTestCase solveTestCase, string[] candidate)
        {
            return solveTestCase.Expected.OrderBy(x => x).SequenceEqual(candidate.OrderBy(x => x));
        }

        private static bool SubsetMatch(SolveTestCase solveTestCase, string[] candidate)
        {
            return new HashSet<string>(solveTestCase.Expected).IsSubsetOf(new HashSet<string>(candidate));
        }

        [Test]
        [TestCaseSource(sourceType:typeof(WordbrainTestCases), sourceName: nameof(WordbrainTestCases.SolveTestCases))]
        public void Solve(SolveTestCase solveTestCase)
        {
            Console.WriteLine("Grid:");

            foreach (var row in solveTestCase.Input.Grid.Split(','))
            {
                Console.WriteLine("\t{0}", row);
            }


            var actual = WordbrainHelper.Solve(solveTestCase.Input);

            Assert.IsNotEmpty(actual.Candidates);


            Console.WriteLine("Found:");
            foreach (var candidate in actual.Candidates)
            {
                Console.WriteLine("{{ \"{0}\" }}", candidate.FoundWords.Select(word => word.Word).Aggregate((c1, c2) => c1 + "\", \"" + c2));
            }


            if (solveTestCase.Expected != null)
            {
                // Any subset candidate
                Assert.IsTrue(actual.Candidates.Any(candidate => SubsetMatch(solveTestCase, candidate.FoundWords.Select(word => word.Word).ToArray())));

                // Any exact candidate
                Assert.IsTrue(actual.Candidates.Any(candidate => ExactMatch(solveTestCase, candidate.FoundWords.Select(word => word.Word).ToArray())));
            }
            else
            {
                Assert.Inconclusive();
            }
        }
    }
}