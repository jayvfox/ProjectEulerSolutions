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
            for (long sum = 6; ; sum++)
            {
                var bLimit = Math.Sqrt(2 * sum - 7);
               for (long b = (long)Math.Sqrt(2*sum/3)+1; b <= bLimit; b++)
                {
                    var dLimit = Math.Sqrt(2 * sum - b - 1);
                    for (long d = (long)Math.Sqrt((2*sum - b)/2)+1; d <= dLimit; d++)
                    {
                        if (!UtilityFunctions.IsPerfectSquare(2 * sum - b - d))
                            break;
                        long fSquared= 2 * sum - b - d;

                        if (fSquared <= 4 || fSquared >= d * d || d >= b) 
                            break;
                        for (long c = 2 - (d % 2); c < d; c += 2) 
                        {
                            var eLimit = Math.Min(c, Math.Sqrt(fSquared));
                            for (long e = 12 + (fSquared % 2); e < eLimit; e++)
                            {
                                var cSquared = c * c;
                                var dSquared = d * d;
                                var eSsuared = e * e;
                                if (dSquared - cSquared != fSquared - eSsuared)
                                    break;
                                for (long a = 2 - (b % 2); a < c; a += 2) 
                                {
                                    var aSquared = a * a;
                                    var bSquared = b * b;
                                    
                                    var x = aSquared + bSquared;
                                    var y = bSquared - aSquared;
                                    var z = dSquared - cSquared;
                                    //Console.WriteLine($"{sum} : {b} {d} {f} {e} | {c} {a} | {e}");
                                    if ((x == cSquared + dSquared) && (y == eSsuared + fSquared) && (z == fSquared - eSsuared))
                                    {
                                        Console.WriteLine($"Solution: {x / 2} {y / 2} {z / 2} {sum / 2}");
                                        return sum / 2;
                                    }
                                }
                            }
                        }
                    }
                }
                if (sum % 1000 == 0)
                    Console.WriteLine(sum);
            }
        }
    }
}