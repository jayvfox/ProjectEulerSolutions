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
        public static long limit = UtilityFunctions.IntegralPower(10,4);
        public static int exponent = 3;
        public static long modBase = 1000000007;

        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var nonSquareFree = new Dictionary<long,long>();
          
            stopwatch.Restart();
            Parallel.ForEach(primes, (p) =>
            {
                var primePower = p * p;
                
                for (long i = 1; i <= limit / primePower; i++)
                {
                    lock (nonSquareFree)
                    {
                        var newNumber = i * primePower;
                        if (nonSquareFree.ContainsKey(newNumber))
                        {
                            nonSquareFree[newNumber] *= p;
                        }
                        else
                            nonSquareFree.Add(newNumber,p);
                    }
                }
            });
            stopwatch.Stop();
            Console.WriteLine($"Square-free part list took {stopwatch.ElapsedMilliseconds}.");
            primes = null;
            GC.Collect();
            var squareFreePart = new int[modBase/2];

            stopwatch.Restart();
            for (long i = 1; i <= limit; i++)
            {
                
                var reduced = nonSquareFree.ContainsKey(i)? nonSquareFree[i] % modBase : i% modBase;
                    squareFreePart[reduced]++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Reducing square-free list took {stopwatch.ElapsedMilliseconds}.");

            stopwatch.Restart();
            for (long i = 0; i < Math.Min(modBase, limit); i++)
            {

                var count = squareFreePart[i];
                var number = i+1;
                if (count == 0)
                    continue;

                long sum;
                UtilityFunctions.Gcd(number - 1, modBase, out long inverse, out long dummy);
                if (inverse == 0)
                    sum = exponent;
                else
                {
                    var modPower = UtilityFunctions.ModPower(number, exponent, modBase);
                    sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * number) % modBase;
                }
                solution = (solution + (sum * count) % modBase) % modBase;
            }
            stopwatch.Stop();
            Console.WriteLine($"Processing took {stopwatch.ElapsedMilliseconds}.");
            return solution;
        }

        public static long Solution2()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var squareFreePart = new long[limit];
           stopwatch.Start();
           for (long i = 1; i<=limit; i++)
           {
               squareFreePart[i - 1] = i;
           }
           stopwatch.Stop();
           Console.WriteLine($"Initialising list took {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Restart();
            Parallel.ForEach(primes, (p) =>
           {
               var primePower = p * p;
               while (primePower <= limit)
               {
                   for (long i = 1; i <= limit / primePower; i++)
                   {
                      lock(squareFreePart) 
                      {
                          squareFreePart[i * primePower - 1] /= p;
                      }
                   } 
                   primePower *= p;
               }
           });
            stopwatch.Stop();
            Console.WriteLine($"Square-free part list took {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Restart();
            for (long i = 0; i < limit; i++)
            {
                var item = squareFreePart[i] % modBase;
                if (item < i + 1)
                {
                    squareFreePart[item - 1]++;
                    squareFreePart[i] = 0;
                    continue;
                }
                else
                    squareFreePart[i] = 1;
            }
            stopwatch.Stop();
            Console.WriteLine($"Reducing square-free list took {stopwatch.ElapsedMilliseconds}.");

            stopwatch.Restart();
            for (long i = 0; i < Math.Min(modBase,limit); i++)
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
                   var modPower = UtilityFunctions.ModPower(i+1, exponent, modBase);
                   sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * (i+1)) % modBase;
               }
               solution = (solution + (sum*count) % modBase) % modBase;
           }
            stopwatch.Stop();
            Console.WriteLine($"Processing took {stopwatch.ElapsedMilliseconds}.");
            return solution;
        }
    }
}