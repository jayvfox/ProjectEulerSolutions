using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace ProjectEuler
{
    public class Problem585
    {
        public static long limit = 100;
        public static long Solution()
        {
            var stopwatch = new Stopwatch();
            long totalTime = 0;

            long solution = 0;
            var validSolutions = new List<long[]>();

            var primes = UtilityFunctions.Primes(limit);
            List<long> nonSquareFree;
            var squareFree = UtilityFunctions.SquareFreeIntegers(limit * limit, out nonSquareFree);
            var squares = new List<long>();
            for (long i = 1; i < Math.Sqrt(limit); i++)
                squares.Add(i * i);

            for (int n = 1; n <= limit; n++)
            {
                long maxIndex = 0;
                for (int i = 1; ; i++ )
                {
                    if (squareFree[i] > n)
                        break;
                    maxIndex = i;
                }
                stopwatch.Restart();
                var partitions = UtilityFunctions.Partition(n, squareFree.ToArray(), squares.ToArray(), maxIndex,4);
                stopwatch.Stop();
                Console.WriteLine($"Time spent on partitioning {n}: {stopwatch.ElapsedMilliseconds}");
                totalTime += stopwatch.ElapsedMilliseconds;
                foreach (var part in partitions)
                {
                    var nonzeroCoeffIndices = new List<int>();
                    for (int i = 0; i < part.Length; i++)
                    {
                        if (part[i] != 0)
                            nonzeroCoeffIndices.Add(i);
                    }
                   //if (nonzeroCoeffIndices.Count != 2 && nonzeroCoeffIndices.Count != 4)
                   //    continue;
                    var masks = new bool[1 << nonzeroCoeffIndices.Count-1][];
                    for (int i = 0; i < masks.Length; i++)
                    {
                        masks[i] = new bool[part.Length];
                        for (int j=0; j < nonzeroCoeffIndices.Count-1; j++)
                        {
                            if ((i & (1 << j)) != 0)
                                masks[i][nonzeroCoeffIndices[j]] = true;
                        }
                    }

                    foreach (var signMask in masks)
                    {
                        var numberSquaredArray = new long[squareFree.Count];
                        for (int i = 1; i < part.Length; i++)
                        {
                            numberSquaredArray[0] = n;
                            if (part[i] == 0)
                                continue;
                            var aI = squareFree[i];
                            for (int j = 0; j < i; j++)
                            {
                                if (part[j] == 0)
                                    continue;
                                var aJ = squareFree[j];
                                var gcd = UtilityFunctions.Gcd(aI, aJ);
                                var aProduct = (aI / gcd) * (aJ / gcd);
                                var indexProduct = squareFree.IndexOf(aProduct);
                                long temp = (long)Math.Sqrt(part[i] * part[j]);
                                numberSquaredArray[indexProduct] += 2 * temp * gcd * ( signMask[i] != signMask[j]? -1:1);
                            }
                        }
                        int positiveCount = 0;
                        for (long i = 1; i < numberSquaredArray.Length; i++)
                        {
                            var coefficient = numberSquaredArray[i];
                            if (coefficient < 0)
                            {
                                positiveCount = 0;
                                break;
                            }
                            else if (coefficient > 0)
                                positiveCount += 1;
                        }
                        if (positiveCount == 1 || positiveCount == 2)
                        {
                            solution += 1;
                            validSolutions.Add(numberSquaredArray);
                            if (nonzeroCoeffIndices.Count > 4)
                            {
                                Console.Write($"{n}: ");
                                for (int i = 0; i < numberSquaredArray.Length; i++)
                                    if (numberSquaredArray[i] != 0)
                                        Console.Write($"{numberSquaredArray[i]}<{squareFree[i]}> +");
                                Console.WriteLine();
                            }
                        }
                    }
                }

            }


            Console.WriteLine($"Total time spent on partitions: {totalTime}");
            return solution;
        }
    }
}
