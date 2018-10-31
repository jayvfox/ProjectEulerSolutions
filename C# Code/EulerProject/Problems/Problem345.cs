using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem345
    {
        public static long Solution()
        {
            long solution = 0;

            var numberGrid = new int[][]{
                             new[] { 7, 53, 183, 439, 863, 497, 383, 563, 79, 973, 287, 63, 343, 169, 583 },
                             new[] { 627, 343, 773, 959, 943, 767, 473, 103, 699, 303, 957, 703, 583, 639, 913 },
                             new[] { 447, 283, 463, 29, 23, 487, 463, 993, 119, 883, 327, 493, 423, 159, 743 },
                             new[] { 217, 623, 3, 399, 853, 407, 103, 983, 89, 463, 290, 516, 212, 462, 350 },
                             new[] { 960, 376, 682, 962, 300, 780, 486, 502, 912, 800, 250, 346, 172, 812, 350 },
                             new[] { 870, 456, 192, 162, 593, 473, 915, 45, 989,   873, 823, 965, 425, 329, 803 },
                             new[] { 973, 965, 905, 919, 133, 673, 665, 235, 509, 613, 673, 815, 165, 992, 326 },
                             new[] { 322, 148, 972, 962, 286, 255, 941, 541, 265, 323, 925, 281, 601, 95, 973 },
                             new[] { 445, 721, 11, 525, 473, 65, 511, 164, 138, 672, 18, 428, 154, 448, 848 },
                             new[] { 414, 456, 310, 312, 798, 104, 566, 520, 302, 248, 694, 976, 430, 392, 198 },
                             new[] { 184, 829, 373, 181, 631, 101, 969, 613, 840, 740, 778, 458, 284, 760, 390 },
                             new[] { 821, 461, 843, 513, 17, 901, 711, 993, 293, 157, 274, 94, 192, 156, 574 },
                             new[] { 34, 124, 4, 878, 450, 476, 712, 914, 838, 669, 875, 299, 823, 329, 699 },
                             new[] { 815, 559, 813, 459, 522, 788, 168, 586, 966, 232, 308, 833, 251, 631, 107 },
                             new[] { 813, 883, 451, 509, 615, 77, 281, 613, 459, 205, 380, 274, 302, 35, 805 }};

            var dimension = numberGrid.Length;

            var columns = Enumerable.Range(0, dimension).Reverse().ToArray();
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = i + 1; j < dimension; j++)
                    {
                        var currentSum = numberGrid[i][columns[i]] + numberGrid[j][columns[j]];
                        var newSum = numberGrid[i][columns[j]] + numberGrid[j][columns[i]];
                        if (newSum > currentSum)
                        {
                            var temp = columns[i];
                            columns[i] = columns[j];
                            columns[j] = temp;
                            changed = true;
                        }
                    }
                }
            }

            for (int i = 0; i < dimension; i++)
                solution += numberGrid[i][columns[i]];
            
            return solution;
        }
    }
}