using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    /// <summary>
    /// In an a x b grid, the number of rectangles along the normal gridlines simple equals (a+1 choose 2)*(b+1 choose 2). 
    /// Along the diagonals, we set up a recurrence relation: assume b >= a; and let f(b,a) be the number of diagonal rectangles. 
    /// Then f(b+1,a) = f(b,a) + the number of diagonal rectangles with topmost vertex in the top row. 
    /// Enumerate these according to size to obtain sum_{i=1}{a}(sum_{j=1}{i} j = a(2a-1)(2a+1)/3. 
    /// </summary>

    public class Problem147
    {
        public static long aLimit = 43;
        public static long bLimit = 47;
        public static long Solution()
        {
            long solution = 0;
            long[][] diagonalRectangles = new long[bLimit][];
            long[][] allRectangles = new long[bLimit][];
            
            for (int i=0; i<bLimit; i++)
            {
                diagonalRectangles[i] = new long[Math.Min(i+1,aLimit)];
                allRectangles[i] = new long[Math.Min(i + 1, aLimit)];
                diagonalRectangles[i][0] = i;
            }

            for (int a = 1; a <= aLimit; a++)
            {
                var increment = a * (2 * a + 1) * (2 * a - 1) / 3;
                if (a > 1)
                {
                    diagonalRectangles[a-1][a-1] = diagonalRectangles[a-1][a - 2] + increment - a;
                }
                allRectangles[a-1][a-1] = diagonalRectangles[a-1][a-1] + a * a * (a + 1) * (a + 1) / 4;
                solution += allRectangles[a-1][a-1];
                for (int b = a+1; b <= bLimit; b++)
                {
                    diagonalRectangles[b-1][a-1] = diagonalRectangles[b-2][a-1] + increment;
                    allRectangles[b-1][a-1] = diagonalRectangles[b-1][a-1] + a * b * (a + 1) * (b + 1) / 4;
                    solution += allRectangles[b-1][a-1];
                    solution += (b <= aLimit) ? allRectangles[b - 1][a - 1] : 0;
                }
            }

            return solution;
        }
    }
}