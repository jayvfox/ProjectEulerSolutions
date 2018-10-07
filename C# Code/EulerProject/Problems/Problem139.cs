using System;
using System.Collections.Generic;
using System.Linq;


namespace EulerProject
{
    public class Problem139
    {

        // a^2+b^2=c^2 and a-b | c. We may assume a primitive triangle, since they simply scale. As before, a = 2xy, b=x^2-y^2. c=x^2+y^2 and x^2-2xy-y^2 | x^2+y^2. However, 
        // if prime p|b-a, then p| (x^2+y^2) + (x^2-2xy-y^2) = 2x(x-y). b-a is odd, so p|x or p|x-y. If the latter, then p| x^2-y^2 and hence p|xy, i.e. it divides one of x and y, 
        // and hence the other, contradiciton. Hence no such p exists, and we find that x^2-2xy-y^2 = +-1.  (x,y) --> (2x+y,x);


        public static long limit = 100000000 - 1;
        public static long Solution()
        {
            long solution = 0;
            long perimiter = 0;
            long x = 1;
            long y = 0;

            while (true)
            {
                var temp = x;
                x = 2*x + y;
                y = temp;
                perimiter = 2 * x * (x + y);
                if (perimiter > limit)
                    break;
                solution += limit / perimiter;               
            }

            return solution;
        }
    }
}