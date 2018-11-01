using System;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem549
    {
        public static long limit = NumberTheory.IntegralPower(10,8);
        public static long Solution()
        {
            long solution = 0;
            var primes = PrimeTools.Primes(limit);
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
                        if (NumberTheory.LargestPowerDividingFactorial(p * mid, p) < exponent)
                            lower = mid;
                        else
                            upper = mid;
                    }
                    
                    minima[primePower] = (upper) * p;
                    for (long i = 1; i <= limit / primePower; i++)
                    {
                        minima[i * primePower] = Math.Max(minima[i], minima[primePower]);
                    }
                    primePower *= p;
                    exponent += 1;
                }
            }

            for (long i = 1; i < limit; i++)
                solution += minima[i+1];
            return solution;
        }
    }
}