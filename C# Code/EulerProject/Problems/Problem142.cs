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
            var validPairs = new Dictionary<long, HashSet<long>>();
            for (long a = 3; ; a++)
            {
                for (long b = 2 - (a % 2); b < a; b += 2)
                {
                    var x = (a * a + b * b) / 2;
                    var y = (a * a - b * b) / 2;
                    if (validPairs.ContainsKey(x))
                    {
                        validPairs[x].Add(y);
                        if (validPairs.ContainsKey(y))
                        {
                            long sum = long.MaxValue;
                            foreach (var z in validPairs[y])
                            {
                                if (validPairs[x].Contains(z))
                                {
                                    sum = Math.Min(sum, x + y + z);
                                }
                            }
                            if (sum < long.MaxValue)
                                return sum;
                        }
                    }
                    else
                        validPairs.Add(x, new HashSet<long> { y });
                }
            }
        }
    }
}