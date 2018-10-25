using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ProjectEuler
{
    public class Problem639
    {
        public static long limit = UtilityFunctions.IntegralPower(10,12);
        public static int exponent = 50;
        public static long modBase = 1000000007;
        public static long Solution()
        {
            long solution = 0;
            var primes = UtilityFunctions.Primes((long)Math.Sqrt(limit));
            primes.Add((long)Math.Sqrt(long.MaxValue));
            
            for (var n = 1; n <= limit; n++)
             {
                 int index = 0;
                 long m = n;
                 long primeSquared;
                 while ((primeSquared = primes[index]*primes[index]) <= m)
                 {
                    while (m % primeSquared == 0)
                    {
                        
                        m /= primes[index];
                        
                    }
                    index++;
                 }
                 m %= modBase;
                 long sum = m;
                 for (int i = 1; i < exponent; i++)
                     sum = ((sum + 1) * m) % modBase;
                 solution = (solution + sum) % modBase;
             }
            return solution;
        }
    }
}