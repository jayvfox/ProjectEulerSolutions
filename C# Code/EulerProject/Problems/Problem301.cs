using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    /// <summary>
    /// X(n, 2n, 3n) =0 iff n, 2n and 3n XOR to 0, iff the sum of n and 2n in binary have no carries, 
    /// iff n does not contain any consecutive ones in binary. 
    /// </summary>
    public class Problem301
    {
        public static long limit = 30;
        public static long Solution()
        {
            long solution = 0;

            for (int i = 0; i <= limit; i++)
                solution += Combinatorics.Choose(limit - i + 1, i);
            return solution;
        }
    }
}