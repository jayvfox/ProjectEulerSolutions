using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem134
    {
        public static long limit = 1000000;
        public static long Solution()
        {
            long solution = 0;

            var primes = UtilityFunctions.Sieve(limit+100);

            for (int i=3; true; i++)
            {
                var p1 = primes[i - 1];
                var p2 = primes[i];

                if (p1 > limit)
                    break;
                long powerOf10 = (long)Math.Pow(10, p1.ToString().Length);
                var x = (-p1 * UtilityFunctions.ModularInverse(powerOf10, p2) % p2 + p2) % p2;
                solution += powerOf10*x + p1;
            }

            return solution;
        }
    }
}