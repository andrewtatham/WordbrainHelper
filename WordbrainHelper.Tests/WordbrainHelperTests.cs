using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Config;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{
    [TestFixture]
    public class WordbrainHelperTests
    {




        public static readonly SolveTestCase[] SolveTestCases =
        {
            new SolveTestCase("TS,LA", new List<int>() {4}, new[] {"SALT"}),
            new SolveTestCase("CN,HI", new List<int>() {4}, new[] {"CHIN"}),
            new SolveTestCase("LSE,LID,LOD", new List<int>() {5, 4}, new[] {"SLIDE", "DOLL"}),
            new SolveTestCase("ENRD,LOCO,HBAT,RTRE", new List<int>() {5, 6, 5}, new [] {"TABLE", "NORTH", "RECORD"}),
            new SolveTestCase("PIRC,KATH,NIID,NOSW", new List<int>() {6, 5, 5}, new [] {"SWITCH", "PIANO", "DRINK"}),
            new SolveTestCase("TENE,RLTO,ICRB,CYMO", new List<int>() {8,8}, new [] {"TRICYCLE", "TROMBONE"}),
            new SolveTestCase("DYAE,RPSI,ACKT,CREN", new List<int>() {4,7,5}, new [] {"CARD", "NECKTIE", "SPRAY"}),
            new SolveTestCase("FHFS,LSIK,ARCE,GATN", new List<int>() {4,4,8}, new [] { "NECK", "FLAG", "STARFISH" }),

            new SolveTestCase("WENF,HEAW,OGRS,RCNI", new List<int>() {3,3,4,6}, new [] { "SAW", "HEN", "CROW", "FINGER" }),
            // new SolveTestCase("", new List<int>() {}, null),
            // new SolveTestCase("", new List<int>() {}, null),
            // new SolveTestCase("", new List<int>() {}, null),
            // new SolveTestCase("", new List<int>() {}, null),
            // new SolveTestCase("", new List<int>() {}, null),
            
            // new SolveTestCase("", new List<int>() {}, null),
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
        [TestCaseSource(nameof(SolveTestCases))]
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
                Console.WriteLine("{{ \"{0}\" }}", candidate.Select(word => word.Word).Aggregate((c1, c2) => c1 + "\", \"" + c2));
            }


            if (solveTestCase.Expected != null)
            {
                // Any subset candidate
                Assert.IsTrue(actual.Candidates.Any(candidate => SubsetMatch(solveTestCase, candidate.Select(word => word.Word).ToArray())));

                // Any exact candidate
                Assert.IsTrue(actual.Candidates.Any(candidate => ExactMatch(solveTestCase, candidate.Select(word => word.Word).ToArray())));
            }
            else
            {
                Assert.Inconclusive();
            }
        }
    }
}