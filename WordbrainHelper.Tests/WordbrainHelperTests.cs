using System;
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
            new SolveTestCase("LSE,LID,LOD", new[] {5, 4}, new[] {"SLIDE", "DOLL"})
            //new SolveTestCase("ENRD,LOCO,HBAT,RTRE", new int[]{5, 6, 5}, null),
            //new SolveTestCase("PIRC,KATH,NIID,NOSW", new int[]{6, 5, 5}, null)
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

        [Test]
        [TestCaseSource("SolveTestCases")]
        public void Solve(SolveTestCase solveTestCase)
        {
            var actual = WordbrainHelper.Solve(solveTestCase.Input);

            Assert.IsNotEmpty(actual.Candidates);
            if (solveTestCase.Expected != null)
            {
                Assert.IsTrue(
                    actual.Candidates.Any(candidate =>
                        solveTestCase.Expected.OrderBy(x => x).SequenceEqual(candidate.OrderBy(x => x))));
            }
            else
            {
                foreach (var candidate in actual.Candidates)
                {
                    Console.WriteLine(candidate);
                }
            }
        }
    }
}