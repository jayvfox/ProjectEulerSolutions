﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem357
    {
        public static long limit = 100000000;
        public static long Solution()
        {
            long solution = 3;
            var primes = new HashSet<long>(UtilityFunctions.Primes(limit));
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