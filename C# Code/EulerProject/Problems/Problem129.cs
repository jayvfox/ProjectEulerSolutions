using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EulerProject
{
    public class Problem129
    {
        public static int upperLimit = 1000000;
        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            for (int i = upperLimit + 1; true; i += 2)
            {

                var order = UtilityFunctions.MultiplicativeOrder(10, 9 * i);

                if (order > upperLimit)
                {
                    Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
                    return i;
                }
            }
        }

        
    }
}