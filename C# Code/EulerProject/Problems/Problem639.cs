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
        public static long limit = UtilityFunctions.IntegralPower(10,7);
        public static int exponent = 3;
        public static long modBase = 1000000007;
        
        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var nonSquareFree = new Dictionary<long, long>();

            stopwatch.Restart();
           
            foreach (var p in primes)
            {
                var primePower = p * p;
                while (primePower <= limit)
                {
                    for (long i = 1; i <= limit / primePower; i++)
                    {
                        var newNumber = i * primePower;
                        if (nonSquareFree.ContainsKey(newNumber))
                        {
                            nonSquareFree[newNumber] /= p;
                        }
                        else
                            nonSquareFree.Add(newNumber, newNumber / p);
                    }
                    primePower *= p;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Square-free part list took {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Restart();
            foreach (var key in nonSquareFree.Keys.ToArray())
            {
                var value = key % modBase;
                var newKey = nonSquareFree[key];
                nonSquareFree.Remove(key);
                if (nonSquareFree.ContainsKey(newKey))
                    nonSquareFree[newKey]++;
                else
                    nonSquareFree.Add(newKey, 1);
                
                if (nonSquareFree.ContainsKey(value))
                    nonSquareFree[value]--;
                else
                    nonSquareFree.Add(value, -1);
            }
            stopwatch.Stop();
            Console.WriteLine($"Reducing list took {stopwatch.ElapsedMilliseconds}.");

            stopwatch.Restart();

            solution = PowerSum(1, limit, exponent, modBase);

            foreach (var element in nonSquareFree)
            {
                var count = element.Value;
                var number = element.Key;
                long inverse, sum;
                UtilityFunctions.Gcd(number-1, modBase, out inverse, out long dummy);
                if (inverse == 0)
                    sum = exponent;
                else
                {
                    var modPower = UtilityFunctions.ModPower(number, exponent, modBase);
                    sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * number) % modBase;
                }
                solution = (solution + sum * count) % modBase;
            }
            stopwatch.Stop();
            Console.WriteLine($"Processing took {stopwatch.ElapsedMilliseconds}.");

            return (solution + modBase) % modBase;
        }

        public static long Solution1()
        {
            long[] solution = new long[] { 0 };
            var primes = UtilityFunctions.Primes((long)Math.Sqrt(limit));
            primes.Add((long)Math.Sqrt(long.MaxValue));

            Parallel.For(1, limit + 1, n =>
            {
                if (n % 1000000 == 0)
                    Console.WriteLine($"Working on {n}.");
                int index = 0;
                long m = n;
                long primeSquared;
                while ((primeSquared = primes[index] * primes[index]) <= m)
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
                lock (solution)
                {
                    solution[0] = (solution[0] + sum) % modBase;
                }
            });
            return solution[0];
        }

        public static long Solution0()
        {
            var chunks = Math.Max(limit/1000,1);
            

            var stopwatch = new Stopwatch();
            var solution = new long[1];
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var squareFreePart = new long[modBase];
            int[] progress = new[] { 0 };

            Parallel.For(0, chunks, batch =>
            {
                var lowerLimit = batch * (limit / chunks);
                var upperLimit = lowerLimit + limit / chunks;
                var nonSquareFree = new Dictionary<long, long>();
                foreach (var p in primes)
                {

                    var primePower = p * p;
                    if (primePower > upperLimit)
                        break;
                    while (primePower <= upperLimit)
                    {
                        for (long i = lowerLimit / primePower + 1; i <= upperLimit / primePower; i++)
                        {
                            var newNumber = i * primePower;
                            if (nonSquareFree.ContainsKey(newNumber))
                            {
                                nonSquareFree[newNumber] /= p;
                            }
                            else
                                nonSquareFree.Add(newNumber, newNumber / p);
                        }
                        primePower *= p;
                    }
                }
                lock (squareFreePart)
                {
                    for (long i = lowerLimit + 1; i <= upperLimit; i++)
                    {
                        var reduced = nonSquareFree.ContainsKey(i) ? nonSquareFree[i] % modBase : i % modBase;
                        squareFreePart[reduced]++;
                    }
                }
               progress[0]++;
               if (progress[0]%10000==0)
                   Console.WriteLine($"{Math.Round(progress[0] * 100.0 / chunks,3)}% done.");
            });
            Parallel.For(1, modBase, i =>
           {
               var count = squareFreePart[i];
               if (count > 0)
               {
                   var number = i;

                   long sum;
                   UtilityFunctions.Gcd(number - 1, modBase, out long inverse, out long dummy);
                   if (inverse == 0)
                       sum = exponent;
                   else
                   {
                       var modPower = UtilityFunctions.ModPower(number, exponent, modBase);
                       sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * number) % modBase;
                   }
                   lock (solution)
                   {
                       solution[0] = (solution[0] + (sum * count) % modBase) % modBase;
                   }
               }
           });

            return solution[0];
        }


        public static long PowerSum(long startNumber, long endNumber, long exponent, long modBase)
        {
            startNumber = startNumber % modBase;
            endNumber = endNumber % modBase;
            var sums = new long[exponent + 1];
            sums[0] = endNumber - startNumber + 1;
            for (long i = 1; i <= exponent; i++)
            {
                long newSum = (UtilityFunctions.ModPower(endNumber + 1, i + 1, modBase) - UtilityFunctions.ModPower(startNumber, i + 1, modBase)) % modBase;
                for (long r = 0; r < i; r++)
                {
                    newSum -= ((UtilityFunctions.Choose(i + 1, r) % modBase) * sums[r]) % modBase;
                }
                UtilityFunctions.Gcd(i + 1, modBase, out long inverse, out var dummy);
                if (inverse == 0)
                    sums[i] = sums[0];
                else
                    sums[i] = (newSum * inverse + modBase) % modBase;
            }
            long sum = 0;
            for (int i = 1; i <= exponent; i++)
                sum = (sum + sums[i] + modBase) % modBase;
            return sum;
        }


    }
}