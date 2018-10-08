using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeStuff();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var answer = Problem585.Solution();
            stopwatch.Stop();
            Console.WriteLine(answer);
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }


        static void TimeStuff()
        {
            var limit = 5000;
            var primes = UtilityFunctions.Primes(limit);
            List<long> nonSquareFree;
            var squareFree = UtilityFunctions.SquareFreeIntegers(limit, out nonSquareFree);
            var squares = new List<long>();
            for (long i = 1; i < Math.Sqrt(limit); i++)
                squares.Add(i * i);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var answer = UtilityFunctions.Partition(limit, squareFree.ToArray(), squares.ToArray(),squareFree.Count-1, 4);
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }

    }
}
