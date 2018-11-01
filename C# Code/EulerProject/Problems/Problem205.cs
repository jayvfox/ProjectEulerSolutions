using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem205
    {
        public static long limit = 100;
        public static double Solution()
        {
            double solution = 0;

            double peter = 0;
            long colin = 0;

            long totalColin = 0;
            long totalPeter = 0;

            var peterCount = (double)NumberTheory.IntegralPower(4, 9);
            var colinCount = (double)NumberTheory.IntegralPower(6, 6);

            for (int n = 36; n >=6; n--)
            {
                colin = DieRolls(n,6,6);
                totalColin += colin;

                solution += (colin/colinCount) * (peter);

                var tempPeter = DieRolls(n, 9, 4);
                totalPeter += tempPeter;

                peter += tempPeter / peterCount;
                
            }
            
            return Math.Round(solution,7);
        }


        private static long DieRolls(int n, int numberOfDice, int max)
        {
            if (n == 0 || numberOfDice == 0)
                return 0;
            if (n < numberOfDice)
                return 0;
            if (numberOfDice == 1 && n <= max)
                return 1;
            if (n == numberOfDice)
                return 1;


            long sum = 0;
            for (var firstDice = 1; firstDice <= max; firstDice++)
            {
                sum += DieRolls(n - firstDice, numberOfDice - 1, max);
            }
            return sum;
        }

    }
}