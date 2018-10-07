using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem142
    {
        public static long limit = 100;
        public static long Solution()
        {
            long solution = 0;
            for (long sum = 6; ; sum++)
            {
                for (long z = 1; z < sum / 3.0; z++)
                    for (long y = z + 1; y < (sum - z) / 2.0; y++)
                    {
                        long x = sum - y - z;
                        if (UtilityFunctions.IsPerfectSquare(x - y) && UtilityFunctions.IsPerfectSquare(x + y) && UtilityFunctions.IsPerfectSquare(y - z) && UtilityFunctions.IsPerfectSquare(y + z) && UtilityFunctions.IsPerfectSquare(x - z) && UtilityFunctions.IsPerfectSquare(x + z))
                        {
                            Console.WriteLine($"{x} {y} {z}");
                            return sum;
                        }
                    }
                Console.WriteLine(sum);
            }
        }
    }
}