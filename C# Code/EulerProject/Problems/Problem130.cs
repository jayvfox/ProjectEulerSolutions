using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;


namespace EulerProject
{
    public class Problem130
    {
        public static long limit = 25;
        public static long Solution()
        {
            long solution = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var previousTime = 0.0;
            var currentTime = 0.0;

            var count = 0;
            var solutions = new List<long>();
            for (int i = 7; count < limit; i++ )
            {
                var order = UtilityFunctions.MultiplicativeOrder(10, 9 * i);
                if ( order > 0 && (i-1) % order == 0 && !UtilityFunctions.IsPrime(i))
                {
                    count += 1;
                    solution += i;
                    solutions.Add(i);
                    Console.Write($"{ i}, ");
                }
            }
            currentTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Solution took {currentTime - previousTime} seconds.");
            Console.WriteLine(solutions.ToArray());
            return solution;
        }
    }
}