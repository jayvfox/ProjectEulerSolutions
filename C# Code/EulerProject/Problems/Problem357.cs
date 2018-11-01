using System;
using System.Collections.Generic;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem357
    {
        /// <summary>
        /// It's clear than n must be square-free and even. If n = 2k, then both p = k+2 and 2k+1 = 2p-3 must be prime. Furthermore, if 
        /// n is divisble by 3 and n = 6k, then all of 6k+1, 3k+2, 2k+3 and k+6 must be prime. We filter the n to choose based on these
        /// conditions, and directly test those. 
        /// </summary>
        public static long limit = 100000000;
        public static long Solution()
        {
            long solution = 3;
            var primes = new HashSet<long>(PrimeTools.Primes(limit));
            var eligibleN = new HashSet<long>();

            foreach (var p in primes)
            {
                if (2 * p - 4 > limit)
                    break;
                if (primes.Contains(2 * p - 3))
                {
                    if (p % 6 == 1)
                        eligibleN.Add(2 * p - 4);
                    else if (primes.Contains((p + 16) / 3) && primes.Contains((2 * p + 5) / 3))
                        eligibleN.Add(2 * p - 4);
                }

            }

            foreach (var n in eligibleN)
            {
                bool allPrime = true;
                for (var j = 5; j < Math.Sqrt(n); j++)
                {
                    if (n % j == 0)
                    {
                        var p = j + n / j;
                        if (!primes.Contains(p))
                        {
                            allPrime = false;
                            break;
                        }
                    }
                }
                if (allPrime)
                {
                    solution += n;                 
                }
               
            }

            return solution;
        }
    }
}