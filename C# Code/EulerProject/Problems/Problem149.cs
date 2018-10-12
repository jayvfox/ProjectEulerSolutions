using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;


namespace ProjectEuler
{
    public class Problem149
    {
        public static int limit = 2000;
        public static double Solution()
        {
            double solution = 0;
            var randomGrid = GenerateRandomGrid(limit);
            var check = randomGrid.ToArray();

            for (int i = 0; i < limit;i++)
            {
                var currentVector = randomGrid.Column(i).ToList();
                solution = MaxSum(currentVector, solution);
                currentVector = randomGrid.Row(i).ToList();
                solution = MaxSum(currentVector, solution);
            }

            for (int i = 0; i < limit; i++)
            {
                var diagonal1 = new List<double>();
                var diagonal2 = new List<double>();
                var diagonal3 = new List<double>();
                var diagonal4 = new List<double>();
                
                for (int j = 0; j <= i; j++)
                {
                    diagonal1.Add(randomGrid[j, i - j]);
                    diagonal2.Add(randomGrid[limit - j - 1, limit - i + j - 1]);
                    diagonal3.Add(randomGrid[limit - j - 1, i - j]);
                    diagonal4.Add(randomGrid[j, limit - i + j - 1]);

                }
                solution = MaxSum(diagonal1, solution);
                solution = MaxSum(diagonal2, solution);
                solution = MaxSum(diagonal3, solution);
                solution = MaxSum(diagonal4, solution);
            }
            
            return solution;
        }

        private static Matrix<double> GenerateRandomGrid(int size)
        {
            var randoms = new double[size * size];
            var loopLimit = Math.Min(size * size, 55);
            for (long k = 1; k <= loopLimit; k++)
            {
                randoms[k - 1] = ((100003 - 200003 * k + 300007 * k * k * k) % 1000000) - 500000;
            }

            loopLimit = size * size;
            for (long k = 56; k < loopLimit; k++)
            {
                randoms[k - 1] = ((randoms[k - 25] + randoms[k - 56] + 1000000) % 1000000) - 500000;
            }

            return CreateMatrix.Dense(size, size, randoms);
        }

        private static double MaxSum(List<double> vector, double currentMax)
        {
            vector.RemoveAll(t => t == 0);
            if (vector.Select(t => Math.Max(0, t)).Sum() < currentMax)
                return currentMax;

            var subSum = new List<double>();
            double currentSum = 0;
            for (int i = 0; i < vector.Count; i++)
            {
                currentSum += vector[i];
                if (i == vector.Count - 1)
                {
                    subSum.Add(currentSum);
                    break;
                }
                if (vector[i] * vector[i + 1] > 0)
                    continue;
                                    
                subSum.Add(currentSum);
                currentSum = 0;
            }

            if (subSum[0] < 0)
                subSum.RemoveAt(0);
            if (subSum.Last() < 0)
                subSum.RemoveAt(subSum.Count - 1);

            for (int i = 0; i < subSum.Count; i += 2)
            {
                currentSum = subSum[i];
                currentMax = Math.Max(currentSum, currentMax);
                for (int j = i + 1; j < subSum.Count; j += 2)
                {
                    currentSum += subSum[j] + subSum[j + 1];
                    currentMax = Math.Max(currentSum, currentMax);
                }
            }
            return currentMax;
        }
    }
}