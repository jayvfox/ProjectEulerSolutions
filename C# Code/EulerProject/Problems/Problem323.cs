using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem323
    {
        public static long limit = 32;
        public static double Solution()
        {
            var expectedRounds = new double[limit+1];
            expectedRounds[0] = 0;
            for (int i = 1; i <= limit; i++)
            {
                var denominator = (NumberTheory.IntegralPower(2, i) - 1);
                expectedRounds[i] = 1.0/denominator;
                for (int j = 1; j <= i; j++)
                    expectedRounds[i] += (Combinatorics.Choose(i, j) * (1 + expectedRounds[i - j])) / denominator;
            }

            return Math.Round(expectedRounds[limit], 10);
        }
    }
}