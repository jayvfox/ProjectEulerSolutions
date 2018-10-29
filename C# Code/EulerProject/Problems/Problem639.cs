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
        public static long limit = UtilityFunctions.IntegralPower(10,8);
        public static int exponent = 3;
        public static long modBase = 1000000007;

        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            var squareFreePart = new long[limit];
           
           
            stopwatch.Start();
            for (long i = 1; i <= limit; i++)
            {
                squareFreePart[i - 1] = i;
            }
            stopwatch.Stop();
            Console.WriteLine($"Initialising list took {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Restart();
           
            foreach (var p in primes)
            {
                var primePower = p * p;
                while (primePower <= limit)
                {
                    for (long i = 1; i <= limit / primePower; i++)
                    {
                        squareFreePart[i * primePower - 1] /= p;
                    }
                    primePower *= p;
                }
            }
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

            // var count = limit / modBase;
            // var residue = limit % modBase;
            // for (long i = 1; i < Math.Min(modBase, limit); i++)
            // {
            //     var thisCount = i < residue ? count + 1 : count;
            //
            //     long inverse, sum;
            //     UtilityFunctions.Gcd(i, modBase, out inverse, out long dummy);
            //     if (inverse == 0)
            //         sum = exponent;
            //     else
            //     {
            //         var modPower = UtilityFunctions.ModPower(i + 1, exponent, modBase);
            //         sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * (i + 1)) % modBase;
            //     }
            //     solution = (solution + (sum * thisCount) % modBase) % modBase;
            // }

            return solution;
        }


        public static long Solution2()
        {
            var chunks = Math.Max(limit/100000000,1);
            

            var stopwatch = new Stopwatch();
            long solution = 0;
            var primes = UtilityFunctions.Primes((int)Math.Sqrt(limit));
            

            for (int batch = 0; batch < chunks; batch++)
            {
                Console.WriteLine($"Processing batch {batch + 1}.");
                var lowerLimit = batch * (limit / chunks);
                var upperLimit = lowerLimit + limit / chunks;
                var nonSquareFree = new Dictionary<long, long>();
                stopwatch.Restart();
                //Parallel.ForEach(primes, (p) =>
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
                                nonSquareFree.Add(newNumber, newNumber/p);
                        }
                        primePower *= p;
                    }
                }
                stopwatch.Stop();
                Console.WriteLine($"Square-free part list took {stopwatch.ElapsedMilliseconds}.");

               // stopwatch.Restart();
               // for (long i = 1; i <= limit; i++)
               // {
               //
               //     var reduced = nonSquareFree.ContainsKey(i) ? nonSquareFree[i] % modBase : i % modBase;
               //     squareFreePart[reduced]++;
               // }
               // stopwatch.Stop();
               // Console.WriteLine($"Reducing square-free list took {stopwatch.ElapsedMilliseconds}.");

                stopwatch.Restart();
               // for (long i = lowerLimit+1; i <= upperLimit; i++)
               // {
               //
               //     var number = nonSquareFree.ContainsKey(i)? nonSquareFree[i] : i;
               //
               //     long sum;
               //     UtilityFunctions.Gcd(number - 1, modBase, out long inverse, out long dummy);
               //     if (inverse == 0)
               //         sum = exponent;
               //     else
               //     {
               //         var modPower = UtilityFunctions.ModPower(number, exponent, modBase);
               //         sum = ((((modPower - 1) * ((inverse + modBase) % modBase)) % modBase) * number) % modBase;
               //     }
               //     solution = (solution + (sum) % modBase) % modBase;
               // }
               
                stopwatch.Stop();
                Console.WriteLine($"Processing took {stopwatch.ElapsedMilliseconds}.");
            }
            return solution;
        }

        
    }
}