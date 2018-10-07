using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EulerProject
{
    public class Problem118
    {
        public static List<long> ProblemDigits = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        public static List<long>[] PrimeCandidates;

        public static double Solution()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
                        
            var subsetCount = (long)Math.Pow(2, ProblemDigits.Count);
            PrimeCandidates = new List<long>[subsetCount];
            for (int i = 0; i < subsetCount; i++)
                PrimeCandidates[i] = new List<long>();

            var primes = UtilityFunctions.Sieve(98765432);

            foreach (var p in primes)
            {
                var digitSignature = UtilityFunctions.DigitSignature(p, ProblemDigits);
                if (digitSignature > 0)
                    PrimeCandidates[digitSignature].Add(p);
            }

            return PandigitalPrimeSets(ProblemDigits);
         }


        internal static long PandigitalPrimeSets(List<long> digits)
        {
            if (digits.Count == 0)
                return 1;
            long count = 0;
            digits.Sort();
            var maxElement = digits[digits.Count - 1];
            var restOfDigits = UtilityFunctions.Complement(new List<long> { maxElement }, digits);
            var subsets = UtilityFunctions.GenerateSubsets(restOfDigits);
            foreach (var subset in subsets)
            {
                subset.Add(maxElement);
                var complement = UtilityFunctions.Complement(subset, digits);
                var subsetDigitSignature = UtilityFunctions.DigitSignature(subset, ProblemDigits);
                count += PrimeCandidates[subsetDigitSignature].Count * PandigitalPrimeSets(complement);
            }
            return count;
        }

    }
}