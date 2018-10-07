using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace EulerProject
{

    public class Problem131
    {

        //Since n^3 + pn^2 is a cube, it quickly implies that n and n+p are consecutive cubes, i.e. p = 3n^2+3n+1. Increment n and find these primes!

        public static long limit = 1000000;

        public static long Solution()
        {
            long solution = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var previousTime = 0.0;
            var currentTime = 0.0;

            for (int i = 1; true; i++)
            {
                var p = 3 * i * i + 3 * i + 1;
                if (p > limit)
                    break;
                if (UtilityFunctions.IsPrime(p))
                {
                    solution += 1;
                   // Console.WriteLine($"{i} -> {p}");
                }
            }
            currentTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Solution took {currentTime - previousTime} milliseconds.");

            return solution;
        }
    }
}