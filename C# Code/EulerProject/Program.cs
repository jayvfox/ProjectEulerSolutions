using System;
using System.Diagnostics;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeStuff();
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var answer = UtilityFunctions.PartitionIntoPerfectSquares(500);
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }

    }
}
