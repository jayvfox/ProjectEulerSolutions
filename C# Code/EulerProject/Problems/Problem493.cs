using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    /// <summary>
    /// The expected value of the number of different colours is the sum of the probabilities that a particular colour is drawn. 
    /// </summary>
    public class Problem493
    {
        public static long limit = 20;
        public static double Solution()
        {
            return 7 * (1 - UtilityFunctions.Choose(60, 20) / (double)UtilityFunctions.Choose(70, 20)); 
            //return ExpectedBalls(0,0);
        }

        private static double ExpectedBalls(int step, int colours)
        {
            if (step == limit)
                return colours;
            return ((70 - 10 * colours) / (double)(70 - step)) * ExpectedBalls(step + 1, colours + 1) + (10 * colours - step) / (double)(70 - step) * ExpectedBalls(step + 1, colours);
        }
    }
}