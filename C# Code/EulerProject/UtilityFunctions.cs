using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Linq;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class UtilityFunctions
    {
        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseString(string s)
        {
            string reversedString = "";
            for (int i = 0; i < s.Length; i++)
                reversedString += s[s.Length - i - 1];
            return reversedString;
        }

        
        
       

        public static long DigitSignature(long number, List<long> reference)
        {
            var digits = new List<long>();
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = number % 10;
                number = number / 10;
                if (digits.Contains(thisDigit))
                {
                    digits = null;
                    break;
                }
                digits.Add(thisDigit);
            }
            return DigitSignature(digits, reference);
        }

        public static long DigitSignature(List<long> digits, List<long> reference)
        {
            long count = 0;
            if (digits == null)
                return 0;
            foreach (var digit in digits)
            {
                var digitPosition = reference.IndexOf(digit);
                if (digitPosition == -1)
                    return 0;
                count += (long)NumberTheory.IntegralPower(2, digitPosition);
            }
            return count;
        }

        public static List<int> Digits(long number, int baseNumber = 10)
        {
            var digits = new List<int>();
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = (int)(number % baseNumber);
                number = number / baseNumber;
                digits.Add(thisDigit);
            }
            return digits;
        }

        public static long DigitSum(long number, int baseNumber = 10)
        {
            long digitSum = 0;
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = (int)(number % baseNumber);
                number = number / baseNumber;
                digitSum += thisDigit;
            }
            return digitSum;
        }

        public static HashSet<int> DigitSet(long number)
        {
            var digits = new HashSet<int>();
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = number % 10;
                number = number / 10;
                digits.Add((int)thisDigit);
            }
            return digits;
        }
        
        
    }
}