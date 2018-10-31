using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem622
    {
        public static long limit = 100;
        public static int order = 60;
        public static long Solution()
        {
            long solution = 0;
            long power = UtilityFunctions.IntegralPower(2, order) - 1;
            var primes = UtilityFunctions.Primes(UtilityFunctions.IntegralPower(2,order/2));
            var primeFactors = new List<long>();
            var ordersModP = new List<long>();
            var powersOfTwo = new Dictionary<long,long>();
            bool keyAdded = false;
            var factors = new Dictionary<long, int>();
            
            for (int i= 2; i <= Math.Sqrt(order); i++)
            {
                if (order % i == 0)
                {
                    powersOfTwo.Add(i,UtilityFunctions.IntegralPower(2,i)-1);
                    if (i*i != order)
                        powersOfTwo.Add(order/i,UtilityFunctions.IntegralPower(2, order/i) - 1);
                }
            }

            var keys = powersOfTwo.Keys.OrderBy(t => t).ToArray();

            foreach (var p in primes)
            {
                if (power < p * p)
                {
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
                    primeFactors.Add(p);
                    foreach (var key in keys)
                    {
                        keyAdded = false;
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

            }


            return solution;
        }
    }
}