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
            Console.WriteLine(Problem549.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }

        static void TimeStuff()
        {
            var stopwatch = new Stopwatch();
            while (true)
            {
                var limit = 10000000;
                for (int i = 1; i < limit; i++)
                {
                    stopwatch.Start();
                    UtilityFunctions.DigitSum(limit);
                    stopwatch.Stop();
                }
                Console.WriteLine($"New digitSum took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                stopwatch.Reset();
                for (int i = 1; i < limit; i++)
                {
                    stopwatch.Start();
                    var sum = 0;
                    foreach (var d in UtilityFunctions.Digits(limit))
                        sum += d;
                    stopwatch.Stop();
                }
                Console.WriteLine($"Old digitSum took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                Console.ReadKey();
            }
        }
    }
}
