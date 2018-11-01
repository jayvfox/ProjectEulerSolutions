using System;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem129
    {
        public static int upperLimit = 1000000;
        public static long Solution()
        {
            for (int i = upperLimit + 1; true; i += 2)
            {
                var order = NumberTheory.MultiplicativeOrder(10, 9 * i);

                if (order > upperLimit)
                    return i;
            }
        }

        
    }
}