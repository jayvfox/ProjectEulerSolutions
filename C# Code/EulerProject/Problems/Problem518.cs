using ProjectEuler.HelperClasses;
using System;

namespace ProjectEuler
{
    public class Problem518
    {
        public static long limit = NumberTheory.IntegralPower(10,2);
        public static long Solution()
        {
            long solution = 0;
            var primes = PrimeTools.PrimeSieve(limit);

            for (long m = 2; m < Math.Sqrt(limit); m++)
            {
                long c;
                for (long g = 1; (c = g * m * m - 1) < limit; g += g<4?1:2) 
                {
                    var stepsize = 2 - m % 2;
                    if (!primes[c])
                        continue;
                    for (long n = 1; n<m; n+=stepsize)
                    {
                        if (NumberTheory.Gcd(m, n) > 1)
                            continue;
                        var b = m * n * g - 1;
                        var a = n * n * g - 1;
                        if (primes[b] && primes[a])
                        {
                            solution += a + b + c;
                        }
                    }
                }
            }
            return solution;
        }
    }
}