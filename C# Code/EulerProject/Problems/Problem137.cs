using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem137
    {
        // By expanding each term of the Fibonacci series in A(x), one solves the equation A(x) = x / (1 - x - x^2). 
        // Setting x = a/b, this leads to A(x) = 2ab/(b^2-ab-a^2). Since this is in lowest terms, A(x) is a positive integer 
        // iff b^2 - ab - a^2 = 1. This has integer solution iff 5a^2+4 = y^2 for some y, i.e. y+sqrt(5)x has mod 4 in Z[sqrt(5)]
        // It turns out that a and b are consecutive fibonacci numbers. 

        public static long Solution()
        {   
            long a = 1;
            long b = 2;
            for (int i = 1; i<15; i++)
            {
                a = a + b;
                b = a + b;
            }

            return a*b;
        }
    }
}