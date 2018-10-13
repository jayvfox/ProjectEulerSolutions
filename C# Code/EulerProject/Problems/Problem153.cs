using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ProjectEuler
{
    public class Problem153
    {
        public static long limit = 100000;
        public static long Solution()
        {
            var stopwatchSolver = new Stopwatch();
            var stopwatchKeyList = new Stopwatch();
            var stopwatchPopulateDivSums = new Stopwatch();

            long solution = 0;
            var primes = UtilityFunctions.Primes(limit);
            var divSums = new SortedList<long,long>();
            divSums.Add(1,1);
            foreach (var p in primes)
            {
                long pSum;
                if (p == 2)
                    pSum =  3;
                else if (p % 4 == 3)
                    pSum = 1;
                else
                {
                    stopwatchSolver.Start();
                    var temp = UtilityFunctions.SumOfSquares(p);
                    if (temp.Count > 2)
                        Console.WriteLine(p);
                    pSum = 1 + 2 * temp.Sum();
                    stopwatchSolver.Stop();
                }
                stopwatchKeyList.Start();
                var keyList = divSums.Keys.ToList();
                stopwatchKeyList.Stop();

                stopwatchPopulateDivSums.Start();
                foreach (var key in keyList)
                {
                    if (p * key > limit)
                        break;
                    long primePower = p;
                    while (primePower*key <= limit)
                    {
                        divSums.Add(primePower * key, divSums[key] * ((primePower - 1) / (p - 1) * pSum  + primePower));
                        primePower *= p;
                    }
                }
                stopwatchPopulateDivSums.Stop();
            }
            solution = divSums.Sum(x => x.Value);
            Console.WriteLine($"Solving for a^2+b^2=p took {stopwatchSolver.ElapsedMilliseconds} milliseconds.");
            Console.WriteLine($"Getting the keys took {stopwatchKeyList.ElapsedMilliseconds} milliseconds.");
            Console.WriteLine($"populating table took {stopwatchPopulateDivSums.ElapsedMilliseconds} milliseconds.");
            var tempp = divSums.OrderBy(p => p.Key);
            return solution;
        }
    }
}