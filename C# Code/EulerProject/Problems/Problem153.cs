﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ProjectEuler
{
    public class Problem153
    {
        public static long limit = 100000000;
        public static long Solution()
        {
            long solution = 0;

            var primes = UtilityFunctions.Primes(limit, (long)Math.Sqrt(limit));

            for (long a = 1; a <= Math.Sqrt(limit); a++)
            {
                for (long b = 1; b < a; b++)
                {
                    var g = UtilityFunctions.Gcd(a, b);
                    if (g > 1)
                        continue;
                    var n = a * a + b * b;
                    if (n > limit)
                        break;
                    var floor = limit / n;
                    for (long k = 1; k <= floor;k++)
                        solution += 2 * (a + b) * k * (limit/(k*n));
                }
            }

            for (long i = 1; i <= limit; i++)
            {
                solution -= (limit % i)*(i%2==0? 2:1);
            }
            solution += limit * limit + limit*(limit/2);

            return solution;
        }
    }
}