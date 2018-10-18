using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;


namespace ProjectEuler
{
    public class Problem150
    {
        public static int limit = 1000;
        public static double Solution()
        {
            double solution = double.MaxValue;
            var randoms = GenerateRandomGrid(limit);
            //randoms = TestRandoms();

            var columnSums = new double[limit][];
            var gridSums = new double[limit][];

            for (int i = 0; i < limit; i++)
                gridSums[i] = new double[i + 1];

            for (int i = 0; i < limit; i++)
            {
                columnSums[i] = new double[limit - i];
                columnSums[i][0] = randoms[limit - 1][i];
                for (int j = 1; j <= limit - i - 1; j++)
                {
                    columnSums[i][j] = columnSums[i][j - 1] + randoms[limit - 1 - j][i];
                }
                for (int x = i; x < limit; x++)
                {
                    for (int y = 0; y < x - i + 1; y++)
                    {
                        gridSums[x][y] += columnSums[i][x - i] - (y == 0 ? 0 : columnSums[i][y - 1]);
                        if (gridSums[x][y] > 0)
                            gridSums[x][y] = 0;
                    }
                    solution = Math.Min(solution, gridSums[x][x - i]);
                }
            }

            return solution;
        }

        private static double[][] GenerateRandomGrid(int size)
        {
            var numberOfRandoms = size * (size + 1) / 2;
            var randoms = new double[size][];
            double t = 0;
            for (int k = 1; k <= size; k++)
            {
                randoms[k - 1] = new double[k];
                for (int j = 1; j <= k; j++)
                {
                    t = (615949 * t + 797807) % UtilityFunctions.IntegralPower(2, 20);
                    randoms[k - 1][j - 1] = t - UtilityFunctions.IntegralPower(2, 19);
                }
            }

            return randoms;
        }

        private static double[][] TestRandoms()
        {
            return new double[][]
            {
                new double[] {15},
                new double[] {-14, -7},
                new double[] {20, -13, -5},
                new double[] {-3, 8, 23, -26},
                new double[] {1, -4, -5, -18, 5},
                new double[] {-16, 31, 2, 9, 28, 3}
            };
        }
    }
}
