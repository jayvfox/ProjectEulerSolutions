using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem132
    {
        public static long limit = 1000000000;
        public static long Solution()
        {
            long solution = 0;
            var counter = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var previousTime = 0.0;
            var currentTime = 0.0;

            var primes = PrimeTools.Primes(1000000);
            currentTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Settig up took {currentTime - previousTime} milliseconds.");
            previousTime = currentTime;

            foreach (var p in primes)
            {
                var order = NumberTheory.MultiplicativeOrder(10, 9 * p);
                if (order == 0) continue;
                if (limit % order == 0)
                {
                    solution += p;
                    counter += 1;
                    Console.WriteLine(p);
                    if (counter >= 40) return solution;
                }
            }
            return solution;
        }
    }
}