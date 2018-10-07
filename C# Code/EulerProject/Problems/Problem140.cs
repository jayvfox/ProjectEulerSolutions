using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem140
    {
        public static long limit = 30;
        public static long Solution()
        {
            long solution = 0;
            long a1 = 0, b1 = 1, a11 = -1, b11 = 3;

            var nList = new List<long>();
            while (true)
            {
                var n1 = GenerateNextSolution(a1, b1, 1, out a1, out b1);
                if (n1 < 0)
                    break;
                nList.Add(n1);
            }
            while (true)
            {
                var n11 = GenerateNextSolution(a11, b11, 11, out a11, out b11);
                if (n11 < 0)
                    break;
                nList.Add(n11);
            }

            nList.Sort();
            for (int i = 0; i < limit; i++)
            {
                solution += nList[i];
            }
            return solution;

        }

        internal static long GenerateNextSolution(long a, long b, int k, out long nextA, out long nextB)
        {
            nextA = a + b;
            nextB = a + 2 * b;
            var n = (3 * nextA + nextB) * nextA / k;
            return n;
        }
    }
}