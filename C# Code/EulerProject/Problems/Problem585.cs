using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem585
    {
        public static long limit = 100;
        public static long Solution()
        {
            long solution = 0;

            var primes = UtilityFunctions.Primes(limit);
            List<long> nonSquareFree;
            var squareFree = UtilityFunctions.SquareFreeIntegers(limit, out nonSquareFree);
            var squares = new List<long>();
            for (long i = 1; i < Math.Sqrt(limit); i++)
                squares.Add(i * i);

            for (int n = 1; n <= limit; n++)
            {
                var partitions = UtilityFunctions.Partition(n,squareFree.ToArray(),squares.ToArray());
                foreach (var part in partitions)
                {
                    var eligibleCandidate = true;
                    var nonzeroCoeffs = part.TakeWhile(p => p != 0).ToList();
                    if (nonzeroCoeffs.Distinct().Count() != nonzeroCoeffs.Count())
                        break;
                    foreach (var i in nonzeroCoeffs)
                    {
                        if (nonSquareFree.Contains(i))
                        {
                            eligibleCandidate = false;
                            break;
                        }
                            
                    }
                    if (!eligibleCandidate)
                        break;
                    var numberSquaredArray = new long[squareFree.Count];
                    for (long i = 1; i < part.Length; i++)
                    {
                        var aI = part[i];
                        if (aI == 0)
                            continue;
                        for (long j = 0; j < i; j++)
                        {
                            var aJ = part[j];
                            if (aJ == 0)
                                continue;
                            var indexI = squareFree.IndexOf(aI);
                            var indexJ = squareFree.IndexOf(aJ);
                            var gcd = UtilityFunctions.Gcd(aI, aJ);
                            var aProduct = (aI / gcd) * (aJ / gcd);
                            var indexProduct = squareFree.IndexOf(aProduct);
                            numberSquaredArray[indexProduct] += 2 * (i + 1) * (j + 1) * gcd;
                        }
                     }
                    int positiveCount = 0;
                    for(long i = 1; i < numberSquaredArray.Length; i++)
                    {
                        var coefficient = numberSquaredArray[i];
                        if (coefficient < 0)
                        {
                            positiveCount = 0;
                            break;
                        }
                        else if (coefficient > 0)
                            positiveCount += 0;
                    }
                    if (positiveCount == 1 || positiveCount == 2)
                        Console.WriteLine($"{n} : {part}");
                }

            }



            return solution;
        }
    }
}
