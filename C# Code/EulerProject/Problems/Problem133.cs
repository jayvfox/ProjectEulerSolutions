using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem133
    {
        public static long limit = 100000;
        public static long Solution()
        {
            long solution = 0;

            var primes = UtilityFunctions.Primes(limit);
            foreach (var p in primes)
            {
                var ord = UtilityFunctions.MultiplicativeOrder(10, 9 * p);
                if (ord == 0)
                {
                    solution += p;
                    continue;
                }
                while (ord % 2 == 0 || ord % 5 == 0)
                {
                    if (ord % 2 == 0) ord /= 2;
                    if (ord % 5 == 0) ord /= 5;
                }
                if (ord > 1)
                    solution += p;
                else
                    Console.WriteLine(p);
            }

            return solution;
        }
    }
}