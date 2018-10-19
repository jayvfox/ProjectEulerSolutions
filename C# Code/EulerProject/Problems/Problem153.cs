using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ProjectEuler
{
    public class Problem153
    {
        public static long limit = 100000000;
        public static long Solution()
        {
            long solution = 0;
            var stopwatch = new Stopwatch();
            var gcdStopwatch = new Stopwatch();
            for (long a = 1; a <= Math.Sqrt(limit); a++)
            {
                var stepSize = 2 - (a % 2);
                for (long b = 1; b < a; b += stepSize)
                {
                    gcdStopwatch.Start();
                    var g = UtilityFunctions.Gcd(a, b);
                    gcdStopwatch.Stop();
                    if (g > 1)
                        continue;
                    var n = a * a + b * b;
                    if (n > limit)
                        break;
                    var floor = limit / n;
                    stopwatch.Start();
                    for (long k = 1; k <= floor; k++)
                    {
                        solution += 2*(a+b)*k*(limit/(k*n));
                    }
                    stopwatch.Stop();
                }
            }
            Console.WriteLine($"Time spent calculating gcd: {gcdStopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"Time spent in first loop: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Restart();
            for (long i = 1; i <= limit; i++)
            {
                solution -= (limit % i)*(i%2==0? 2:1);
            }
            stopwatch.Stop();
            solution += limit * limit + limit*(limit/2);
            Console.WriteLine($"Time spent in second loop: {stopwatch.ElapsedMilliseconds}");
            return solution;
        }
    }
}