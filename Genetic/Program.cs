using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Mutation rate? - ");
            int mutationRate = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Generation size? - ");
            int generationSize = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Generations count? - ");
            int generationsCount = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Clear();
            Console.WriteLine("Running...");
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();

            Solver solver = new Solver(mutationRate, generationSize, generationsCount)
            {
                NoMutations = false
            };

            List<Gene> alfas = solver.Solve();

            Console.WriteLine("Mutation rate: " + mutationRate);
            Console.WriteLine("Generation size: " + generationSize);
            Console.WriteLine("Generations count: " + generationsCount);
            Console.WriteLine("\n\t----Top 3 genes----\n");

            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine(alfas[j].ToString());
                Console.WriteLine();
            }

            Console.WriteLine("Elapsed: " + Math.Round(sw.Elapsed.TotalSeconds, 2) + "s");
            Console.ReadKey();
        }
    }
}
