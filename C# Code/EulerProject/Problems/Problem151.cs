using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem151
    {
        public static long limit = 100;
        public static double Solution()
        {
            double solution = 0;

            var startPosition = new int[] {1, 1, 1, 1};

            solution = ExpectedNumberOfSingleSheets(startPosition);

            return Math.Round(solution,6);
        }


        private static double ExpectedNumberOfSingleSheets(int[] startPosition)
        {
            if (startPosition[0] == 1 && startPosition.Sum() == 1)
                return 0;

            var sheetCount = startPosition.Sum();
            double answer = 0;
            for (int i = 0; i <  startPosition.Length; i++)
            {
                if (startPosition[i] == 0)
                    continue;
                int[] nextPosition = (int[])startPosition.Clone();
                nextPosition[i] -= 1;
                for (int j = 0; j < i; j++)
                    nextPosition[j] += 1;
                answer += startPosition[i]*ExpectedNumberOfSingleSheets(nextPosition);
            }
            answer = (sheetCount == 1 ? answer + 1 : answer / sheetCount);
            return answer;
        }
    }
}