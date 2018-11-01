using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    /// <summary>
    /// Use Wilson's Theorem and factorisation of the sum to deduce that the sum equals 9 x the modular inverse of -4!. 
    /// </summary>
    public class Problem381
    {
        public static long limit = NumberTheory.IntegralPower(10,8);
        public static long Solution()
        {
            long solution = 0;

            var primes = PrimeTools.Primes(limit);

            foreach (var p in primes.Where(t => t > 4))
            {
                var inverse = NumberTheory.ModularInverse(-24, p);

                var residue = ((inverse + p) * 9) % p;
                solution += (residue % p);
            }

            return solution;
        }
    }
}