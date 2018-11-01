using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem347
    {
        public static long limit = NumberTheory.IntegralPower(10,7);
        public static long Solution()
        {
            long solution = 0;
            var primes = PrimeTools.Primes(limit/2);

            for (int i = 0; i < primes.Count; i++)
            {
                var p = primes[i];
                List<long> powersOfP = new List<long>();
                var n = p;
                while (n < limit/p)
                {
                    powersOfP.Add(n);
                    n *= p;
                }
                for (int j = i+1; j < primes.Count; j++)
                {
                    var q = primes[j];
                    if (p * q > limit)
                    {
                        break;
                    }
                    var powersOfPTimesQ = new List<long>(powersOfP);
                    long max = 0;
                    while (true)
                    {
                        powersOfPTimesQ = powersOfPTimesQ.Where(t => t * q <= limit).Select(t => t * q).ToList();
                        if (powersOfPTimesQ.Count == 0)
                            break;
                        max = Math.Max(powersOfPTimesQ.Max(), max);
                    }
                    solution += max;
                }
            }



            return solution;
        }
    }
}