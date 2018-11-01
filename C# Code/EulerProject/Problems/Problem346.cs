using System;
using System.Collections.Generic;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem346
    {
        public static long limit = NumberTheory.IntegralPower(10,12);
        public static long Solution()
        {
            long solution = 1;
            HashSet<long> strongRepunits = new HashSet<long>();
            for (long a = 2; a < Math.Sqrt(limit); a++)
            {

                var n = a * a + a + 1;
                if (n >= limit)
                    break;
                while (n < limit)
                {
                    if (!strongRepunits.Contains(n))
                    {
                        solution += n;
                        strongRepunits.Add(n);
                    }
                    n = n * a + 1;
                }
            }
            
            return solution;
        }
    }
}