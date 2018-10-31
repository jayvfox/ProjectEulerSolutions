using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ProjectEuler
{
    public class Problem549
    {
        public static long limit = UtilityFunctions.IntegralPower(10,8);
        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes(limit);
            var minima = new long[limit+1];
            minima[0] = 1;

            foreach (var p in primes)
            {
                long primePower = p;
                long exponent = 1;
                while (primePower <= limit)
                {
                    long upper = exponent;
                    long lower = 1;
                    while (upper - lower > 1)
                    {
                        long mid = (upper + lower) / 2;
                        if (UtilityFunctions.LargestPowerDividingFactorial(p * mid, p) < exponent)
                            lower = mid;
                        else
                            upper = mid;
                    }
                    
                    minima[primePower] = (upper) * p;
                    stopwatch.Start();
                    for (long i = 1; i <= limit / primePower; i++)
                    {
                        minima[i * primePower] = Math.Max(minima[i], minima[primePower]);
                    }
                    stopwatch.Stop();
                    primePower *= p;
                    exponent += 1;
                }
            }
            Console.WriteLine($"Populating table took {stopwatch.ElapsedMilliseconds}.");

            for (long i = 1; i < limit; i++)
                solution += minima[i+1];
            return solution;
        }
    }
}