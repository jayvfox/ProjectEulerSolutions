using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem206
    {
        public static long limit = 1020304050607080900;
        public static long Solution()
        {
            long testNumber = (((int)Math.Sqrt(limit)) / 100)*100+30;
            while (true)
            {
                var n = testNumber * testNumber;
                if (CheckSolution(n))
                    return testNumber;
                if (testNumber % 100 == 30)
                    testNumber += 40;
                else
                    testNumber += 60;
                
            }
        }

        private static bool CheckSolution(long n)
        {
            for (int i = 1; i<10; i++)
            {
                n = n / 100;
                if (n % 10 != 10 - i)
                    return false;
            }
            return true;
        }
    }
}