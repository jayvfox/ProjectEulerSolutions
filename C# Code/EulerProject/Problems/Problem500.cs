using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem500
    {
        public static long limit = 500500;
        public static long Solution()
        {
            long solution = 1;
            var exponents = new List<int>();
            var primes = PrimeTools.Primes(7376512);
            var candidates = new List<long> { 1 };

            for (int i = 0; i < primes.Count; i++)
            {
                var primePower = primes[i]*primes[i];
                var maxCandidate = candidates.Max();

                if (primePower > primes.Last() && primePower > maxCandidate)
                    break;
                while (primePower < Math.Max(primes.Last(), maxCandidate))
                {
                    candidates.Add(primePower);
                    if (maxCandidate > primes.Last())
                    {
                        candidates.Remove(maxCandidate);
                        maxCandidate = candidates.Max();
                    }
                    else
                    {
                        primes.RemoveAt(primes.Count - 1);
                    }
                    primePower *= primePower;
                }
                
            }
            
            foreach (var t in candidates.Concat(primes))
                solution = (solution * t) % 500500507;
            
            return solution;
        }
    }
}