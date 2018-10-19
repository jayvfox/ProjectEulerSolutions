using System;
using System.Collections.Generic;
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
            Console.WriteLine(Problem149.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }

        static void TimeStuff()
        {
            var stopwatch = new Stopwatch();
            while (true)
            {
                var limit = 100000000;
                for (int i = 2; i <= limit; i++)
                {
                    stopwatch.Start();
                    var temp = limit % i;
                    stopwatch.Stop();
                }
                Console.WriteLine($"Modding took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                Console.ReadKey();
            }
        }
    }
}
