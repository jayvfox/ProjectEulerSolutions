using System;
using System.Collections.Generic;
using System.Linq;


namespace EulerProject
{
    public class Problem135
    {

        //n = (n+2d)^2 - (n+d)^2 - n^2 leads to n = (3k-d)(k+d), with the condition that to ensure positivity, 
        // k+d > sqrt(n/3) and to ensure k, d integers, the sum of the two factors are a multiple of 4. 
        // If quickly follows that n = -1,0 (mod 4). Simply do a search for integers with exactly 10 divisor pairs 
        // satisfying these conditions. 

        public static int limit = (int)1e6;
        public static int divisorLimit = 10;
        public static long Solution()
        {
            var exactlyTenSolutions = 0;
            
            for (int n = 3; n<limit; n++)
            {
                if (n % 4 == 2 || n % 4 == 1)
                    continue;
                var solutionCount = 0;
                var F = (int)Math.Sqrt(n / 3.0);
                var sqrtN = Math.Sqrt(n);

                for (int d = 1; d < sqrtN; d++)
                {

                    if (n % d != 0)
                        continue;
                    if ((n / d + d) % 4 != 0)
                        continue;
                    solutionCount += 1;
                    if (d > F)
                        solutionCount += 1;
                    if (solutionCount > divisorLimit)
                        break;
                }
                if ((int)sqrtN * (int)sqrtN == n)
                    solutionCount += 1;

                if (solutionCount == divisorLimit)
                {
                    exactlyTenSolutions += 1;
                    Console.WriteLine(n);
                }
            }

            return exactlyTenSolutions;
        }
    }
}