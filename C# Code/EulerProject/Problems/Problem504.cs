using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem504
    {
        public static long limit = 100;
        public static long Solution()
        {
            long solution = 0;

            for (int a = 1; a <= limit; a++)
            {
                for (int c = 1; c <= a; c++)
                {
                    for (int b = 1; b <= limit; b++)
                    {
                        for (int d = 1; d <= b; d++)
                        {
                            var internalPoints =
                                ((a + c) * (b + d)
                                - UtilityFunctions.Gcd(a, b)
                                - UtilityFunctions.Gcd(b, c)
                                - UtilityFunctions.Gcd(c, d)
                                - UtilityFunctions.Gcd(d, a)) / 2 + 1;
                            if (UtilityFunctions.IsPerfectSquare(internalPoints))
                            {
                                solution += 4;
                                if (a==c || b==d)
                                {
                                    solution -= 2;
                                    if (a==c && b == d)
                                    {
                                        solution -= 1;
                                    }
                                        
                                }
                            }
                        }
                    }
                }
            }


            return solution;
        }
    }
}