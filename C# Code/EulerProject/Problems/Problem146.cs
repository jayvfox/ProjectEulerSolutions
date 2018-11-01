using System;
using System.Collections.Generic;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem146
    {
        public static long million = 1000000;
        public static long limit = 150*million;
        public static long Solution()
        {
            long solution = 0;
            var primes = PrimeTools.Primes(8000);
            var residues = new List<long> { 1, 3, 7, 9, 13, 27 };
            for (long num = 10; num < limit; num += 10)
            {
                if (num % 3 == 0 || (num % 7 != 3 &&  num % 7 !=4)|| num % 13==0)
                    continue;
                bool skipNum = false;
                for (int i=3; i < primes.Count; i++)
                {
                    long p = primes[i];
                    if (p > num * num)
                        break;
                    var r = num % p;
                    foreach (var s in residues)
                    {
                        if ((r * r + s) % p == 0) 
                        {
                            skipNum = true;
                            break;
                        }
                    }
                    if (skipNum)
                        break;
                }
                if (skipNum)
                    continue;
                var nSquared = num * num;
                if (PrimeTools.IsPrime(nSquared + 1)
                    && PrimeTools.IsPrime(nSquared + 3)
                    && PrimeTools.IsPrime(nSquared + 7)
                    && PrimeTools.IsPrime(nSquared + 9)
                    && PrimeTools.IsPrime(nSquared + 13)
                    && PrimeTools.IsPrime(nSquared + 27)
                    && !PrimeTools.IsPrime(nSquared + 19)
                    && !PrimeTools.IsPrime(nSquared + 21))
                {
                    solution += num;
                    Console.WriteLine(num);
                }
            }

            return solution;
        }
    }
}