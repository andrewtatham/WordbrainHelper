using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class WordbrainHelperTests
    {
        public static readonly SolveTestCase[] SolveTestCases =
        {
            new SolveTestCase("TS,LA", new[] {4}, new[] {"SALT"}),
            new SolveTestCase("CN,HI", new[] {4}, new[] {"CHIN"}),
            new SolveTestCase("LSE,LID,LOD", new[] {5, 4}, new[] {"SLIDE", "DOLL"}),
            new SolveTestCase("ENRD,LOCO,HBAT,RTRE", new[] {5, 6, 5}, null),
            new SolveTestCase("PIRC,KATH,NIID,NOSW", new[] {6, 5, 5}, null)
        };

        [TestCase]
        [TestCaseSource("ApplySplitTestCases")]
        public void ApplySplit(ApplySplitTestCase applySplitTestCase)
        {
            var actual = WordbrainHelper.ApplySplit(applySplitTestCase.Input, applySplitTestCase.Words);


            CollectionAssert.AreEqual(applySplitTestCase.Expected, actual.ToArray());
        }

        public static readonly ApplySplitTestCase[] ApplySplitTestCases =
        {
            new ApplySplitTestCase("ABCD", new[] {4}, new[] {"ABCD"}),
            new ApplySplitTestCase("ABCDEFGH", new[] {4, 4}, new[] {"ABCD", "EFGH"}),
            new ApplySplitTestCase("ABCDEFGH", new[] {2, 3, 3}, new[] {"AB", "CDE", "FGH"})
        };

        private static bool ExactMatch(SolveTestCase solveTestCase, string[] candidate)
        {
            return solveTestCase.Expected.OrderBy(x => x).SequenceEqual(candidate.OrderBy(x => x));
        }

        private static bool SubsetMatch(SolveTestCase solveTestCase, string[] candidate)
        {
            return new HashSet<string>(solveTestCase.Expected).IsSubsetOf(new HashSet<string>(candidate));
        }

        [Test]
        [TestCaseSource("SolveTestCases")]
        public void Solve(SolveTestCase solveTestCase)
        {
            Console.WriteLine("Grid:");

            foreach (var row in solveTestCase.Input.Input.Split(','))
            {
                Console.WriteLine("\t{0}", row);
            }


            var actual = WordbrainHelper.Solve(solveTestCase.Input);

            Assert.IsNotEmpty(actual.Candidates);


            Console.WriteLine("Found:");
            foreach (var candidate in actual.Candidates)
            {
                Console.WriteLine("{{ {0} }}", candidate.Aggregate((c1, c2) => c1 + ", " + c2));
            }


            if (solveTestCase.Expected != null)
            {
                // Any subset candidate
                Assert.IsTrue(actual.Candidates.Any(candidate => SubsetMatch(solveTestCase, candidate)));

                // Any exact candidate
                //Assert.IsTrue(actual.Candidates.Any(candidate => ExactMatch(solveTestCase, candidate)));
            }
        }
    }
}