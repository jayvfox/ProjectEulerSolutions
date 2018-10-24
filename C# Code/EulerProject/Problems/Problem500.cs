using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem500
    {
        public static long limit = 500500;
        public static long Solution()
        {
            long solution = 1;
            var exponents = new List<int>();
            var primes = UtilityFunctions.Primes(7376512);
            var candidates = new List<long>(primes);

            for (int i = 0; i < primes.Count; i++)
            {
                var primePower = primes[i]*primes[i];
                var max = candidates.Max();
                if (primePower > max)
                    break;
                while (primePower < max)
                {
                    candidates.Add(primePower);
                    candidates.Remove(max);
                    max = candidates.Max();
                    primePower *= primePower;
                }
                
            }

            foreach (var t in candidates)
                solution = (solution * t) % 500500507;

            return solution;
        }
    }
}