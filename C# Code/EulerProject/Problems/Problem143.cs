using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem143
    {
        public static long limit = 120000;

        public static long Solution()
        {
            long answer = 0;
            var validPairs = new Dictionary<long, HashSet<long>>();
            var validSums = new HashSet<long>();
            for (long p = 2; p < limit; p++)
            {
                for (long q = 1; q < p; q++)
                {
                    if (p + q > limit)
                        break;
                    if (p % 2 == 1)
                    {
                        if ((q % 2==1) && (p * q) % 4 != 3)
                            continue;
                        if (q % 4 == 2)
                            continue;
                    }
                    else
                    {
                        if ((p % 4 == 2) && (q % 2 == 1))
                            continue;
                    }
        
                    var aSquare = (p * p + q * q + p*q);
                    if (!UtilityFunctions.IsPerfectSquare(aSquare))
                        continue;
                        if (validPairs.ContainsKey(p))
                        {
                            validPairs[p].Add(q);
                            if (validPairs.ContainsKey(q))
                            {
                                long sum = long.MaxValue;
                                foreach (var r in validPairs[q])
                                {
                                    if (validPairs[p].Contains(r))
                                    {
                                        var bSquare = p * p + r * r + p * r;
                                        var cSquare = r * r + q * q + r * q;
                                        if (bSquare + cSquare < aSquare)
                                        {
                                            var bc = Math.Sqrt(bSquare * cSquare);
                                            if (bSquare + cSquare + bc < aSquare)
                                                break;
                                        }
                                        sum = p + q + r;
                                        if (sum < limit)
                                        {
                                            validSums.Add(sum);
                                            Console.WriteLine($"{p} {q} {r} {sum}");
                                        }
                                    }
                                }
                            }
                        }
                        else
                            validPairs.Add(p, new HashSet<long> { q });
                }
            }
            foreach (var sum in validSums)
                answer += sum;
            return answer;
        }
    }
}