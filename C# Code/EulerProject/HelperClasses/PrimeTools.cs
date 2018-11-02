using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler.HelperClasses
{
    public static class PrimeTools
    {
        /// <summary>
        /// Returns a list of all prime numbers not greater than upperLimit. 
        /// </summary>
        /// <param name="upperLimit">The upper bound for primes (inclusive).</param>
        /// <returns></returns>
        public static List<long> Primes(long upperLimit)
        {
            List<long> primes = new List<long>();
            if (upperLimit == 1)
                return primes;
            primes.Add(2);

            var half = (upperLimit - 1) / 2;
            bool[] nums = new bool[half];
            var limit = 3037000499; //(int)Math.Sqrt(long.MaxValue);
            for (long i = 0; i < half; i++)
            {
                if (!nums[i])
                {
                    var number = i * 2 + 3;
                    primes.Add(number);
                    if (number <= limit)
                    {
                        for (var j = ((number * number) / 2) - 1; j < half; j += number)
                        {
                            nums[j] = true;
                        }
                    }
                }
            }
            return primes;
        }

        /// <summary>
        /// Returns a sieve such that sieve[n] = false iff 2n+3 is prime. 
        /// </summary>
        /// <param name="upperLimit">The upper bound for primes (inclusive).</param>
        /// <returns></returns>
        public static PrimeSieve PrimeSieve(long upperLimit)
        {
            var half = (upperLimit - 1) / 2;
            bool[] nums = new bool[half];
            var limit = 3037000499; //(int)Math.Sqrt(long.MaxValue);
            for (long i = 0; i < half; i++)
            {
                if (!nums[i])
                {
                    var number = i * 2 + 3;
                    if (number <= limit)
                    {
                        for (var j = ((number * number) / 2) - 1; j < half; j += number)
                        {
                            nums[j] = true;
                        }
                    }
                }
            }
            return new PrimeSieve(nums);
        }

        /// <summary>
        /// Tests whether an integer is prime. 
        /// </summary>
        /// <param name="n">The number to check for primality.</param>
        /// <returns></returns>
        public static bool IsPrime(long n)
        {
            return IsPrime((ulong)n);
        }

        /// <summary>
        /// Tests whether an integer is prime.
        /// </summary>
        /// <param name="n">The number to check for primality.</param>
        /// <returns></returns>
        public static bool IsPrime(uint n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3 || n == 5 || n == 7) return true;
            if (n % 2 == 0) return false;

            var n1 = n - 1;
            var r = 1;
            var d = n1;
            while (d % 2 == 0)
            {
                r++;
                d >>= 1;
            }
            if (!Witness(2, r, d, n, n1)) return false;
            if (n < 2047) return true;
            return Witness(7, r, d, n, n1)
                   && Witness(61, r, d, n, n1);
        }

        /// <summary>
        /// a single instance of the Miller-Rabin Witness loop, optimized for odd numbers less than 2e32. 
        /// Used in primality testing. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <param name="n1"></param>
        /// <returns></returns>
        private static bool Witness(int a, int r, uint d, uint n, uint n1)
        {
            var x = ModPow((ulong)a, d, n);
            if (x == 1 || x == n1) return true;

            while (r > 1)
            {
                x = ModPow(x, 2, n);
                if (x == 1) return false;
                if (x == n1) return true;
                r--;
            }
            return false;
        }

        /// <summary>
        /// Computes a modular power. 
        /// </summary>
        /// <param name="value">Base number.</param>
        /// <param name="exponent">Exponent/</param>
        /// <param name="modulus">Modulus.</param>
        /// <returns></returns>
        private static uint ModPow(ulong value, uint exponent, uint modulus)
        {
            //value %= modulus; // unnecessary here because we know this is true every time already
            ulong result = 1;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1) result = result * value % modulus;
                value = value * value % modulus;
                exponent >>= 1;
            }
            return (uint)result;
        }

        /// <summary>
        /// Tests whether an integer is prime.
        /// </summary>
        /// <param name="n">The number to check for primality.</param>
        /// <returns></returns>
        public static bool IsPrime(ulong n)
        {
            if (n <= uint.MaxValue) return IsPrime((uint)n);
            if (n % 2 == 0) return false;

            BigInteger bn = n; // converting to BigInteger here to avoid converting up to 48 times below
            var n1 = bn - 1;
            var r = 1;
            var d = n1;
            while (d.IsEven)
            {
                r++;
                d >>= 1;
            }
            if (!Witness(2, r, d, bn, n1)) return false;
            if (!Witness(3, r, d, bn, n1)) return false;
            if (!Witness(5, r, d, bn, n1)) return false;
            if (!Witness(7, r, d, bn, n1)) return false;
            if (!Witness(11, r, d, bn, n1)) return false;
            if (n < 2152302898747) return true;
            if (!Witness(13, r, d, bn, n1)) return false;
            if (n < 3474749660383) return true;
            if (!Witness(17, r, d, bn, n1)) return false;
            if (n < 341550071728321) return true;
            if (!Witness(19, r, d, bn, n1)) return false;
            if (!Witness(23, r, d, bn, n1)) return false;
            if (n < 3825123056546413051) return true;
            return Witness(29, r, d, bn, n1)
                   && Witness(31, r, d, bn, n1)
                   && Witness(37, r, d, bn, n1);
        }

        /// <summary>
        /// A single instance of the Miller-Rabin Witness loop. Used in primality testing.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <param name="n1"></param>
        /// <returns></returns>
        private static bool Witness(BigInteger a, int r, BigInteger d, BigInteger n, BigInteger n1)
        {
            var x = BigInteger.ModPow(a, d, n);
            if (x == BigInteger.One || x == n1) return true;

            while (r > 1)
            {
                x = BigInteger.ModPow(x, 2, n);
                if (x == BigInteger.One) return false;
                if (x == n1) return true;
                r--;
            }
            return false;
        }
    }

    public class PrimeSieve
    {
        private bool[] _PrimeSieve;

        public bool this[long i]
        {
            get
            {
                if (i == 2) return true;
                if (i % 2 == 0) return false;
                return !_PrimeSieve[(i - 3) / 2];
            }
        }

        internal PrimeSieve(bool[] array)
        {
            _PrimeSieve = array;
        }
    }
}
