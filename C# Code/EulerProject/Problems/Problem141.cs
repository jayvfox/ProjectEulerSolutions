using System;
using System.Collections.Generic;
using System.Linq;




namespace EulerProject
{
    public class Problem141
    {
        public static long limit = (long)1e12;
        public static long Solution()
        {
            long solution = 0;
            long mLimit = (long)1e4;
            
            HashSet<long> squares = new HashSet<long>();
            for (long m = 2; m < mLimit; m++)
            {
                long kLimit = (long)Math.Sqrt(limit/(m*m*m)+1);
                var step = 2 - (m % 2);
                for (long r = 1; r < m; r += step)
                {
                    if (UtilityFunctions.Gcd(m, r) != 1)
                        continue;
                    if (m * m * m * r + r * r >= limit)
                        break;
                    for (long k = 1; k <= kLimit; k++)
                    {
                        long n = k * k * m * m * m * r + k * r * r;
                        if (n >= limit)
                            break;
                        if (UtilityFunctions.IsPerfectSquare(n))
                        {
                            if (!squares.Contains(n))
                            {
                                solution += n;
                                squares.Add(n);
                            }
                        }
                    }
                }
            }
            return solution;
        }
    }
}