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
                    var pTimesQ = p * q;
                    long r = 1;
                    while (r <= limit / (p * q))
                    {
                        var ham = pTimesQ * r;
                        var hamPrime = ham + 1;
                        solution = (solution + ham) % modBase;
                        if (ham <= limit / 7)
                            hammingNumbers.Add(ham);

                        if (ham <= squareRootOfLimit)
                        {
                            smallHammingNumbers.Add(ham);
                            if (PrimeTools.IsPrime(hamPrime))
                            {
                                smallHammingPrimes.Add(hamPrime);
                                if (hamPrime > 6)
                                {
                                    var currentEligibleDivisors = eligibleDivisors.ToArray();
                                    foreach (var divisor in currentEligibleDivisors)
                                    {
                                        long newNumber = divisor * hamPrime;
                                        if (newNumber <= limit)
                                            eligibleDivisors.Add(newNumber);
                                    }
                                }
                            }
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
            
            eligibleDivisors.Remove(1);
            eligibleDivisors.Sort();
            largeHammingPrimes.Sort();

            var divisorsSansLargePrimes = eligibleDivisors.ToArray();
            foreach (var largePrime in largeHammingPrimes)
            {
                if (largePrime > limit / eligibleDivisors[0])
                    break;
                foreach (var divisor in divisorsSansLargePrimes)
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
            
            foreach (var h in smallHammingNumbers) 
            {
                foreach (var hp in largeHammingPrimes)
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