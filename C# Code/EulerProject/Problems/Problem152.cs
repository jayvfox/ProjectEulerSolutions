using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem152
    {
        //TODO: This is not the way to do it - find a more efficient way. 
        public static long limit = 80;
        public static List<long> primes = UtilityFunctions.Primes(limit);
        public static long Solution()
        {
            long solution = 0;

            solution = NumberOfWays(1, 2, 2, 35);
            
            return solution;
        }

        private static long NumberOfWays(long numerator, long denominator,long lowerLimit, long upperLimit)
        {
            if (upperLimit == lowerLimit)
                return (numerator*lowerLimit*lowerLimit == denominator ? 1 : 0);

            var newLowerLimit = (long)Math.Sqrt(denominator / (double)numerator) + 1;
            lowerLimit = Math.Max(lowerLimit, newLowerLimit + 1);

            if (primes.Contains(upperLimit))
                upperLimit -= 1;
            if (primes.Contains(lowerLimit))
                lowerLimit += 1;

            long newNumerator = numerator * lowerLimit * lowerLimit - denominator;
            long newDenominator = denominator * lowerLimit * lowerLimit;
            var gcd = UtilityFunctions.Gcd(newNumerator, newDenominator);

            long containsLowerLimit = NumberOfWays(newNumerator / gcd, newDenominator / gcd, lowerLimit + 1, upperLimit);
            long doesNotContainLowerLimit = NumberOfWays(numerator, denominator, lowerLimit + 1, upperLimit);
            return containsLowerLimit + doesNotContainLowerLimit;
        }
    }
}