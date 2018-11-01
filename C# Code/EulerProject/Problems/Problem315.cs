using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    /// <summary>
    /// We only count the difference, i.e. the overlapping segments between two numbers. 
    /// </summary>
    public class Problem315
    {
        public static long lowerLimit = 10000000;
        public static long upperLimit = 2*lowerLimit;
        public static Dictionary<int,int> costDictionary = new Dictionary<int, int>();
        const int zero = 0b1110111;
        const int one = 0b0010010;
        const int two = 0b1011101;
        const int three = 0b1011011;
        const int four = 0b0111010;
        const int five = 0b1101011;
        const int six = 0b1101111;
        const int seven = 0b1110010;
        const int eight = 0b1111111;
        const int nine = 0b1111011;
        const int blank = 0b0000000;
        public static long Solution()
        {
            long solution = 0;

            var primes = PrimeTools.Primes(upperLimit).Where(p => p>=lowerLimit);
            
            foreach (var p in primes)
            {
                solution += TotalCostPerNumber((int)p);
            }

            return solution;
        }

        private static int TotalCostPerNumber(int number)
        {
            if (number < 10)
                return 0;
            if (costDictionary.ContainsKey(number))
                return costDictionary[number];
            
            var digitSum = (int)UtilityFunctions.DigitSum(number);

            var cost = CostToChangeNumber(digitSum, number) + TotalCostPerNumber(digitSum);
            costDictionary.Add(number, cost);
            return cost;            
        }

        private static int CostToChangeDigit(int a, int b)
        {
            return 2*BitCount(a & b);
        }

        private static int CostToChangeNumber(int a, int b)
        {
            var cost = 0;
            if (a > b)
                return CostToChangeNumber(b, a);
            var bDigits = NumberToBitstrings(b);
            var aDigits = NumberToBitstrings(a, bDigits.Count);
            for (int i = 0; i < bDigits.Count; i++)
                cost += CostToChangeDigit(aDigits[i], bDigits[i]);
            return cost;
        }

        private static int BitCount(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (n - 1);
            }
            return count;
        }

        private static List<int> NumberToBitstrings(int n, int length=0)
        {
            var bitStrings = new List<int>();
            while (n > 0)
            {
                var digit = n % 10;
                n /= 10;
                bitStrings.Add(DigitToBitString(digit));
                length--;
            }
            while (length > 0)
            {
                bitStrings.Add(blank);
                length--;
            }
            return bitStrings;
        }

        private static int DigitToBitString(int n)
        {
            switch (n)
            {
                case 0: return zero;
                case 1: return one;
                case 2: return two;
                case 3: return three;
                case 4: return four;
                case 5: return five;
                case 6: return six;
                case 7: return seven;
                case 8: return eight;
                case 9: return nine;
                default: throw new Exception("Digits cannot be larger than 9...");
            }
        }
    }
}
