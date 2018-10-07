using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem138

        // Let b = 2a. Then L^2 = b^2 + (2b +- 1)^2. Since b and 2b+-1 are relatively prime, it follows that b = 2xy, 2b+-1 = x^2-y^2 for some integers x, y (Pythagorean triple generation). 
        // This leads to x^2 - 4xy - y^2 = +-1 => (x-2y)^2 - 5y^2 = +-1. Hence (x-2y) - y\sqrt(5) is a unit in Z[5]. 2+\sqrt(5) is a fundamental unit, so all units X_k + Y_k\sqrt(5) are given by 
        // (2 + \sqrt(5))^k. After some work, this leads to if (x,y) is a solution of the original equation, then so is (y,4x+y), with L = x^2+y^2. The first solution (x,y) = (1,4). 
    {
        public static int limit = 12;
        public static long Solution()
        {
            long solution = 0;

            long x = 0;
            long y = 1;

            for (int i = 0; i < limit; i++)
            {
                long temp = x;
                x = y;
                y = 4 * y + temp;
                solution += x * x + y * y;
            }

            return solution;
        }
    }
}