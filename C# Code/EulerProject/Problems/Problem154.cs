using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem154
    {
        public static long limit = 200000;
        public static long Solution()
        {
            long solution = 0;
            for (long a = (long)Math.Ceiling(limit/3.0); a < limit; a++)
                for (long b = (long)Math.Ceiling((limit-a)/2.0); b <= a; b++)
                {
                    var c = limit - a - b;
                    var powerOfFive = UtilityFunctions.LargestPowerDividingFactorial(limit, 5) 
                        - UtilityFunctions.LargestPowerDividingFactorial(a, 5) 
                        - UtilityFunctions.LargestPowerDividingFactorial(b, 5) 
                        - UtilityFunctions.LargestPowerDividingFactorial(c, 5);
                    var powerOfTwo = UtilityFunctions.LargestPowerDividingFactorial(limit, 2)
                       - UtilityFunctions.LargestPowerDividingFactorial(a, 2)
                       - UtilityFunctions.LargestPowerDividingFactorial(b, 2)
                       - UtilityFunctions.LargestPowerDividingFactorial(c, 2);

                    if (powerOfFive >=12 && powerOfTwo >= 12)
                    {
                        solution += 6;
                        if (a == b || b == c)
                        {
                            solution -= 3;
                            if (a == b && b == c)
                                solution -= 2;
                        }
                    }
                }

            return solution;
        }
    }
}