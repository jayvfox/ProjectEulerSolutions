using System;
using System.Diagnostics;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(Problem145.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadLine();
        }
    }
}
