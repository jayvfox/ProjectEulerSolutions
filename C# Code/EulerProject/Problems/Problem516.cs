using ProjectEuler.HelperClasses;
using System;
using System.Collections.Generic;

namespace ProjectEuler
{
    public class Problem516
    {
        public static long limit = NumberTheory.IntegralPower(10,12);
        public static long modBase = NumberTheory.IntegralPower(2, 32);
        public static long Solution()
        {
            var smallHammingPrimes = new List<long>();
            var largeHammingPrimes = new List<long>();
            var smallHammingNumbers = new List<long>();
            var eligibleDivisors = new List<long> { 1 };
            var hammingNumbers = new List<long>();
            long solution = 0;
            long p = 1;
            var squareRootOfLimit = (long)Math.Sqrt(limit);
            while (p <= limit)
            {
                long q = 1;
                while (q <= limit / p)
                {
                    long r = 1;
                    while (r <= limit / (p * q))
                    {
                        var ham = p * q * r;
                        var hamPrime = ham + 1;
                        solution = (solution + ham) % modBase;
                        hammingNumbers.Add(ham);

                        if (ham <= squareRootOfLimit)
                        {
                            smallHammingNumbers.Add(ham);
                            if (PrimeTools.IsPrime(hamPrime))
                                smallHammingPrimes.Add(hamPrime);
                        }
                        else
                        {
                            if (PrimeTools.IsPrime(hamPrime) && hamPrime <= limit)
                                largeHammingPrimes.Add(hamPrime);
                        }
                        r *= 5;
                    }
                    q *= 3;
                }
                p *= 2;
            }

            foreach (var hamPrime in smallHammingPrimes)
            {
                if (hamPrime < 6)
                    continue;
                var currentEligibleDivisors = eligibleDivisors.ToArray();
                foreach (var divisor in currentEligibleDivisors)
                {
                    long newNumber = divisor * hamPrime;
                    if (newNumber <= limit)
                        eligibleDivisors.Add(newNumber);
                }
            }

            eligibleDivisors.Remove(1);
            eligibleDivisors.Sort();
            smallHammingNumbers.Sort();

            foreach (var largePrime in largeHammingPrimes)
            {
                var currentEligibleDivisors = eligibleDivisors.ToArray();
                foreach (var divisor in currentEligibleDivisors)
                {
                    var newNumber = divisor * largePrime;
                    if (newNumber > limit)
                        break;
                    eligibleDivisors.Add(newNumber);
                }
            }

            eligibleDivisors.Sort();

            foreach (var ham in hammingNumbers)
            {
                foreach (var divisor in eligibleDivisors)
                {
                    long newNumber = divisor * ham;
                    if (newNumber > limit)
                        break;
                    solution = (solution + newNumber) % modBase;
                }
            }

            foreach (var hp in largeHammingPrimes)
            {
                foreach(var h in smallHammingNumbers)
                {
                    long newNumber = hp * h;
                    if (newNumber > limit)
                        break;
                    solution = (solution + newNumber) % modBase;
                }
            }


            return solution;
        }
    }
}