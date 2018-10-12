using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem148
    {
        /// <summary>
        /// Observe that (n choose k) is divisible by p if and only if the digits 
        /// of n in base p are greater than the corresponding digits of k in base p. 
        /// </summary>

        public static long million = 1000000;
        public static long limit = 1000*million;
        public static long Solution()
        {
            int prime = 7;
            long solution = 0;

            var limits = UtilityFunctions.Digits(limit-1, prime);
            var numberOfDigits = limits.Count;

            var defaultLimit = prime * (prime + 1) / 2;
            for (int index = 0; index <= numberOfDigits; index++) 
            {
                long thisFactor = 1;
                for (int j = numberOfDigits - 1; j >= 0; j--)
                {
                    if (j < index)
                        thisFactor *= limits[numberOfDigits - j - 1] + 1;
                    else if (j == index)
                        thisFactor *= limits[numberOfDigits - j - 1] * (limits[numberOfDigits - j - 1] + 1) / 2;
                    else
                        thisFactor *= defaultLimit;
                }

                solution += thisFactor;
            }

            return solution;
        }
    }
}