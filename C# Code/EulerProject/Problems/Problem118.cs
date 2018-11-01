using System;
using System.Collections.Generic;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem118
    {
        public static List<long> ProblemDigits = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        public static List<long>[] PrimeCandidates;

        public static double Solution()
        {                        
            var subsetCount = (long)Math.Pow(2, ProblemDigits.Count);
            PrimeCandidates = new List<long>[subsetCount];
            for (int i = 0; i < subsetCount; i++)
                PrimeCandidates[i] = new List<long>();

            var primes = PrimeTools.Primes(98765432);

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
            var restOfDigits = Combinatorics.Complement(new List<long> { maxElement }, digits);
            var subsets = Combinatorics.GenerateSubsets(restOfDigits);
            foreach (var subset in subsets)
            {
                subset.Add(maxElement);
                var complement = Combinatorics.Complement(subset, digits);
                var subsetDigitSignature = UtilityFunctions.DigitSignature(subset, ProblemDigits);
                count += PrimeCandidates[subsetDigitSignature].Count * PandigitalPrimeSets(complement);
            }
            return count;
        }

    }
}