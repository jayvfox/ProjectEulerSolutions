using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ProjectEuler
{
    public class Problem145
    {
        public static long limit = (long)1e6;
        public static long Solution()
        {
            long solution = 0;
            var stopwatch = new Stopwatch();

            for (long i = 1; i < limit; i++)
            {
                if (i % 10 == 0)
                    continue;
                stopwatch.Start();
                var reverseI = long.Parse(UtilityFunctions.ReverseString(i.ToString()));
                stopwatch.Stop();

                var sum = i + reverseI;
                if (HasEvenDigit(sum))
                    continue;
                solution++;
            }
            Console.WriteLine($"Reversing strings took {stopwatch.ElapsedMilliseconds}");
            
            return solution;
        }

        public static bool HasEvenDigit(long number)
        {
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = number % 10;
                if (thisDigit % 2 == 0)
                    return true;
                number = number / 10;
            }
            return false;
        }
    }
}