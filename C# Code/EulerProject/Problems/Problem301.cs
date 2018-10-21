using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem301
    {
        public static long limit = 30;
        public static long Solution()
        {
            long solution = 0;

            for (int i = 0; i <= limit; i++)
                solution += UtilityFunctions.Choose(limit - i + 1, i);
            return solution;
        }
    }
}