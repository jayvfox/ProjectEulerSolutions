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
        
        public static long Solution0()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var squareFreePart = new long[modBase];
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
            for (long i = 1; i <= limit; i++)
            {
                var item = nonSquareFree.ContainsKey(i)? nonSquareFree[i] % modBase : i % modBase;
                if (item < i + 1)
                {
                    squareFreePart[item - 1]++;
                    squareFreePart[i] = 0;
                }
                else
                    squareFreePart[i] = 1;
            }
            stopwatch.Stop();
            Console.WriteLine($"Reducing square-free list took {stopwatch.ElapsedMilliseconds}.");

            stopwatch.Restart();
            for (long i = 0; i < Math.Min(modBase, limit); i++)
            {
                var count = squareFreePart[i];
                if (count == 0)
                    continue;

                long inverse, sum;
                UtilityFunctions.Gcd(i, modBase, out inverse, out long dummy);
                if (inverse == 0)
                    sum = exponent;
                else
                {
                    var modPower = UtilityFunctions.ModPower(i + 1, exponent, modBase);
                    sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * (i + 1)) % modBase;
                }
                solution = (solution + (sum * count) % modBase) % modBase;
            }
            stopwatch.Stop();
            Console.WriteLine($"Processing took {stopwatch.ElapsedMilliseconds}.");

            return solution;
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

        public static long Solution()
        {
            var chunks = Math.Max(limit/1000000,1);
            

            var stopwatch = new Stopwatch();
            var solution = new long[chunks];
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            int[] progress = new[] { 0 };

            Parallel.For(0, chunks, batch =>
            {
                var lowerLimit = batch * (limit / chunks);
                var upperLimit = lowerLimit + limit / chunks;
                var nonSquareFree = new Dictionary<long, long>();
                foreach (var p in primes)
                {

                    var primePower = p * p;
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
                
                // stopwatch.Restart();
                // for (long i = 1; i <= limit; i++)
                // {
                // 
                //     var reduced = nonSquareFree.ContainsKey(i) ? nonSquareFree[i] % modBase : i % modBase;
                //     squareFreePart[reduced]++;
                // }
                // stopwatch.Stop();
                // Console.WriteLine($"Reducing square-free list took {stopwatch.ElapsedMilliseconds}.");

                for (long i = lowerLimit + 1; i <= upperLimit; i++)
                {

                    var number = nonSquareFree.ContainsKey(i) ? nonSquareFree[i] : i;

                    long sum;
                    UtilityFunctions.Gcd(number - 1, modBase, out long inverse, out long dummy);
                    if (inverse == 0)
                        sum = exponent;
                    else
                    {
                        var modPower = UtilityFunctions.ModPower(number, exponent, modBase);
                        sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * number) % modBase;
                    }
                    solution[batch] = (solution[batch] + (sum) % modBase) % modBase;
                }
                nonSquareFree = null;
                GC.Collect();      
                lock (progress)
                {
                    progress[0]++;
                }
                Console.WriteLine($"{Math.Round(progress[0] * 100.0 / chunks,3)}% done.");
            });
            long finalSolution = 0;
            foreach (var s in solution)
                finalSolution = (finalSolution + s) % modBase;

            return finalSolution;
        }

        
    }
}