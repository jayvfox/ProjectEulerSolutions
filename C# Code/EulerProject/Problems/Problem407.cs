using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem407
    {
        public static long limit = NumberTheory.IntegralPower(10,7);

        public static long Solution()
        {
            long solution = 0;
            var primes = PrimeTools.Primes((int)Math.Sqrt(limit));
            var idempotents = new List<long>[limit-1];
            idempotents[2-2] = new List<long> { 0, 1 };

            for (int i = 3; i <= limit; i++)
            {
                idempotents[i - 2] = new List<long> { 0, 1 };
                
                foreach (var p in primes)
                {
                    if (i % p == 0)
                    {
                        var n1 = i / p;
                        var n2 = p;
                        while (n1 % p == 0)
                        {
                            n1 /= p;
                            n2 *= p;
                        }
                        if (n1 == 1)
                        {
                            break;
                        }
                        var inverse = NumberTheory.ModularInverse(n1, n2);
                        foreach (var a1 in idempotents[n1 - 2])
                        {
                            foreach (var a2 in idempotents[n2 - 2])
                            {
                                if (a1 != a2 || a1 > 2)
                                {
                                    var temp = ((a2 - a1) * inverse * n1 + a1) % i;
                                    idempotents[i - 2].Add((temp < 0 ? i + temp : temp));
                                }
                            }
                        }
                        break;
                    }
                }

            }

            foreach (var idems in idempotents)
                solution += idems.Max();
            return solution;
        }
    }
}