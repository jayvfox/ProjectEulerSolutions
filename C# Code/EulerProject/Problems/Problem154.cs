using System;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem154
    {
        /// <summary>
        /// The coefficient of x^ay^bz^c equals n!/(a!b!c!). We calculate the largest power of 10 dividing the numerator and denominator, and check when they differ by at least 12. 
        /// </summary>
        public static long limit = 200000;
        public static long Solution()
        {
            long solution = 0;
            for (long a = (long)Math.Ceiling(limit/3.0); a < limit; a++)
                for (long b = (long)Math.Ceiling((limit-a)/2.0); b <= a; b++)
                {
                    var c = limit - a - b;
                    var powerOfFive = NumberTheory.LargestPowerDividingFactorial(limit, 5) 
                        - NumberTheory.LargestPowerDividingFactorial(a, 5) 
                        - NumberTheory.LargestPowerDividingFactorial(b, 5) 
                        - NumberTheory.LargestPowerDividingFactorial(c, 5);
                    var powerOfTwo = NumberTheory.LargestPowerDividingFactorial(limit, 2)
                       - NumberTheory.LargestPowerDividingFactorial(a, 2)
                       - NumberTheory.LargestPowerDividingFactorial(b, 2)
                       - NumberTheory.LargestPowerDividingFactorial(c, 2);

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