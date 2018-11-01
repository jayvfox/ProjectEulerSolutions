using ProjectEuler.HelperClasses;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler
{
    public class Problem429
    {
        public static long limit = NumberTheory.IntegralPower(10,8);
        public static long modBase = NumberTheory.IntegralPower(10, 9)+9;

        public static long Solution()
        {
            long halfLimit = limit / 2;
            long solution = 1;
            bool[] isComposite = new bool[limit + 1];
            for (int i = 2; i < isComposite.Length; i++)
                if (!isComposite[i])
                {
                    long n = 1;
                    if (i <= halfLimit)
                        n = NumberTheory.LargestPowerDividingFactorial(limit, i);
                    solution = (solution * (NumberTheory.ModPower(i, 2 * n, modBase) + 1)) % modBase;
                    for (int j = 2 * i; j < isComposite.Length; j += i)
                        isComposite[j] = true;
                }

            return solution;
        }

        public static long Solution1()
        {
            long solution = 1;
            var primes = PrimeTools.Primes(limit);
            foreach (var p in primes)
            {
                long largestPower = 1;
                if (2*p <= limit)
                    largestPower = NumberTheory.LargestPowerDividingFactorial(limit, p);
                var thisFactor = NumberTheory.ModPower(p, 2*largestPower, modBase);
                solution = (solution * (1 + thisFactor)) % modBase;
            }
            return solution;
        }
    }
}