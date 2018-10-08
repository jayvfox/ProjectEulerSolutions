using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem136
    {
        // This needs optimising. 

        public static int limit = (int)5e7;
        public static int divisorLimit = 1;
        public static long Solution()
        {
            var exactlyOneSolution = 0;
            var primes = UtilityFunctions.Primes(limit);
            var index = 0;
            var prime = primes[++index];


            foreach (int p in primes)
            {
                if (p % 4 == 3)
                    exactlyOneSolution += 1;
                if (16 * p < limit)
                {
                    exactlyOneSolution += 2;
                }
                else if (4 * p < limit)
                    exactlyOneSolution += 1;
            }

            return exactlyOneSolution;
        }
    }
}