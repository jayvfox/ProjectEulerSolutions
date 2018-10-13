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

            var startPosition = new List<int> {8, 4, 2, 1};

            solution = ExpectedNumberOfSingleSheets(startPosition);

            return solution;
        }


        private static double ExpectedNumberOfSingleSheets(List<int> startPosition)
        {
            if (startPosition.SequenceEqual(new[] { 1 }))
                return 0;

            var sheetCount = startPosition.Count;
            double answer = 0;
            foreach (var sheet in startPosition)
            {
                var nextPosition = new List<int>(startPosition);
                nextPosition.Remove(sheet);
                int splitSheet = sheet / 2;
                while (splitSheet > 0)
                {
                    nextPosition.Add(splitSheet);
                    splitSheet /= 2;
                }
                answer += ExpectedNumberOfSingleSheets(nextPosition);
            }
            return (sheetCount == 1? answer+1 : answer / sheetCount);
        }
    }
}