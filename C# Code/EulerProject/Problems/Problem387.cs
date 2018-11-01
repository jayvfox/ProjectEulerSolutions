using System.Collections.Generic;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem387
    {
        public static long limit = NumberTheory.IntegralPower(10,14);
        public static long Solution()
        {
            long solution = 0;
            HashSet<long> strongTruncatableHarshads;

            var rightTruncatableHarshads = RightTruncatableHarshads(limit, out strongTruncatableHarshads);

            foreach (var s in strongTruncatableHarshads)
            {
                foreach (var i in new[] { 1, 3, 7, 9 })
                {
                    var n = 10 * s + i;
                    if (PrimeTools.IsPrime(n))
                    {
                        solution += n;
                    }
                }
            } 

            return solution;
        }

        private static bool IsHarshad(long n)
        {
            var digitSum = UtilityFunctions.DigitSum(n);
            return (n % digitSum == 0);
        }

        private static bool IsStrongHarshad(long n)
        {
            var digitSum = UtilityFunctions.DigitSum(n);
            if (n % digitSum != 0)
                return false;
            if (!PrimeTools.IsPrime(n / digitSum))
                return false;
            return true;
        }

        private static HashSet<long> RightTruncatableHarshads(long limit, out HashSet<long> strongTruncatableHarshads)
        {
            var truncatableHarshads = new HashSet<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var previousHarshads = new HashSet<long>(truncatableHarshads);
            var currentHarshads = new HashSet<long>();

            strongTruncatableHarshads = new HashSet<long>();
            int length = 2;
            while (length < 14)
            {

                foreach (var h in previousHarshads)
                {
                    for (long i = 0; i < 10; i++)
                    {
                        var n = 10 * h + i;
                        if (IsHarshad(n))
                        {
                            currentHarshads.Add(n);
                            truncatableHarshads.Add(n);
                            if (IsStrongHarshad(n))
                                strongTruncatableHarshads.Add(n);
                        }
                    }
                }
                previousHarshads = new HashSet<long>(currentHarshads);
                currentHarshads = new HashSet<long>();
                length += 1;
            }
            return truncatableHarshads;
        }
    }
}