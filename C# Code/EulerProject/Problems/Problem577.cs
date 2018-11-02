using ProjectEuler.HelperClasses;
using System;

namespace ProjectEuler
{
    /// <summary>
    /// Closed form solution: S(n)=( n(n+3)(n+6)(3n(n+1)−16)+40{0,3n+4,−4}[n%3])/3240
    /// </summary>
    public class Problem577
    {
        public static long limit = 12345;
        public static long Solution()
        {
            long solution = 0;
            var H = new long[limit+1];
            long sum = 0;
            long squareSums = 0;
            long cubeSums = 0;
            H[0] = 0; H[1] = 0;

            for (long n=3; n <= limit; n++)
            {
                if (n % 3 == 0)
                {
                    var newK = n / 3;
                    sum += newK;
                    squareSums += newK * newK;
                    cubeSums += newK * newK * newK;
                }
                var thisSum = 3*((3 * cubeSums -  (2 * n + 3) * squareSums)/2) + ((n + 1) * (n + 2) * sum) / 2;
                H[n] = thisSum;
                solution += H[n];
            }
            

            return solution;
        }
    }
}