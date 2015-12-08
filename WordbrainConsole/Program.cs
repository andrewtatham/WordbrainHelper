using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Config;
using WordbrainHelper;

namespace WordbrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            Console.WriteLine("Enter the grid");
            int? n = null;
            List<string> rows = new List<string>();
            while (!n.HasValue || rows.Count() < n.Value)
            {
                var line = Console.ReadLine().Trim().ToUpperInvariant();
                if (!n.HasValue)
                {
                    n = line.Length;
                }
                
                rows.Add(line);
            }

            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }
            var wordLengths = new List<int>();
            Console.WriteLine("Enter word lengths");
            while (wordLengths.Sum() < n*n)
            {
                int x;
                var key = Console.ReadKey();

                if (int.TryParse(key.KeyChar.ToString(), out x))
                {
                    wordLengths.Add(x);
                }

            }
            Console.WriteLine("Solving");


            var grid = rows.Aggregate((r1, r2) => r1 + "," + r2);

            var input = new WordbrainInput(grid, wordLengths.ToArray());

            var output = global::WordbrainHelper.WordbrainHelper.Solve(input);

            foreach (var candidate in output.Candidates)
            {
                if (candidate.Any())
                {
                    Console.WriteLine("{0}", candidate.Select(word => word.Word).Aggregate((r1, r2) => r1 + " " + r2));
                }

            }
            Console.WriteLine("done");
            Console.ReadKey();

        }



    }
}
