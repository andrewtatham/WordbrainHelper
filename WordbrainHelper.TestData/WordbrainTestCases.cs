using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordbrainHelper.Tests;

namespace WordbrainHelper.TestData
{
    public static class WordbrainTestCases
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


            // Christmas
            new SolveTestCase("BLA,REM,DAL", new List<int>() {5,4}, new[] { "DREAM", "BALL"}),
            new SolveTestCase("COOH,APTT,TNOR,DSOR", new List<int>() {5,4,4,3}, new[] { "NORTH", "SPOT", "DOOR", "CAT"}),
            new SolveTestCase("TEESS,PCGCS,KERRA,DEAEL,JALWG", new List<int>() {5,5,4,5,6}, new []{"GLASS", "LARGE", "DEEP", "SCREW", "DEEP", "JACKET"}),
            new SolveTestCase("NLESRK,AOUUIE,LIGUTA,DENPSY,PPNOFO,LITTWL", new List<int>() {8,4,4,4,4,6,3,3}, new []{ "UPSTAIRS", "WOLF", "PIPE",  "TONGUE", "NUT", "DOLL", "NAIL",  "KEY"}),
            new SolveTestCase("CHCEBNG,ERAENTK,AILIWIN,HCESOLI,RTKBCAS,UTESARA,BMINSWB", new List<int>() {5,6,4,4,5,3,6,7,5,4}, null),


        };
    }
}
