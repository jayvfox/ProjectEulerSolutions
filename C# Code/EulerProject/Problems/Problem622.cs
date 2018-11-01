using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem622
    {
        /// <summary>
        /// We make use of the fact that if F(n) = sum_{ord(2,d)=n}(d+1), then 
        /// G(n) = sum_{d|2^n-1} (d+1) = sum_{m|n} F(m) (since every divisor d appears in each sum exactly once)
        /// and so using Moebius Inversion, we get that 
        /// F(n) = sum_m|n G(m). Now G(m) = {sum of divisors of m} + {number of divisors of m}.
        /// </summary>
        public static int order = 60;

        public static long Solution()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            long solution = 0;

            var divisorsOfOrder = NumberTheory.Divisors(order);
            var primes = PrimeTools.Primes((int)Math.Sqrt(NumberTheory.IntegralPower(2, order/2) + 1));
            
            var primeFactors = NumberTheory.PrimeFactors(NumberTheory.IntegralPower(2, order) - 1,primes);
            var reducedPrimes = primeFactors.Select(t => t.Item1).ToList();

            foreach (var d in divisorsOfOrder)
            {
                var m = NumberTheory.Moebius(d,null, primes);
                if (m != 0)
                {
                    var divisorsOfPowerOfTwo = NumberTheory.Divisors(NumberTheory.IntegralPower(2, order / d) - 1, null, reducedPrimes);
                    solution += (divisorsOfPowerOfTwo.Sum() + divisorsOfPowerOfTwo.Count()) * m;
                }
            }
            
            return solution;
        }

        public static long Solution1()
        {
            long solution = 0;
            long power = NumberTheory.IntegralPower(2, order) - 1;
            var primes = PrimeTools.Primes(NumberTheory.IntegralPower(2,order/4));
            var primeFactors = new List<long>();
            var ordersModP = new List<long>();
            var powersOfTwo = new Dictionary<long,long>();
            bool keyAdded = false;
            var factors = new Dictionary<long, long>();
            
            for (int i= 2; i <= Math.Sqrt(order); i++)
            {
                if (order % i == 0)
                {
                    powersOfTwo.Add(i,NumberTheory.IntegralPower(2,i)-1);
                    if (i*i != order)
                        powersOfTwo.Add(order/i,NumberTheory.IntegralPower(2, order/i) - 1);
                }
            }

            var keys = powersOfTwo.Keys.OrderBy(t => t).ToArray();

            foreach (var p in primes)
            {
                if (power < p * p)
                {
                    keyAdded = false;
                    primeFactors.Add(power);
                    foreach(var key in keys)
                    {
                        if (powersOfTwo[key] % power == 0)
                        {
                            keyAdded = true;
                            ordersModP.Add(key);
                            break;
                        }
                    }
                    if (!keyAdded)
                        ordersModP.Add(order);
                    break;
                }
                var primePower = p;
                while (power % p == 0)
                {
                    keyAdded = false;
                    primeFactors.Add(p);
                    foreach (var key in keys)
                    {
                        if (powersOfTwo[key] % primePower == 0)
                        {
                            keyAdded = true;
                            ordersModP.Add(key);
                            break;
                        }
                    }
                    if (!keyAdded)
                        ordersModP.Add(order);
                    power /= p;
                    primePower *= p;
                }
            }

            factors.Add(1, 1);

            for (int i = 0; i < primeFactors.Count; i++)
            {
                keys = factors.Keys.ToArray();
                foreach(var key in keys)
                {
                    var newKey = key * primeFactors[i];
                    if (!factors.ContainsKey(newKey))
                        factors.Add(newKey, NumberTheory.Lcm(factors[key], ordersModP[i]));
                }
            }

            foreach (var factor in factors)
            {
                if (factor.Value == order)
                    solution += factor.Key+1;
            }


            return solution;
        }
    }
}