using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.HelperClasses
{
    public class NumberTheory
    {
        /// <summary>
        /// Solves the following congruence: 
        /// x = a1 (mod n1)
        /// x = a2 (mod n2)
        /// </summary>
        /// <returns></returns>
        public static long ChineseRemainderTheorem(long a1, long a2, long n1, long n2)
        {
            var inverse = ModularInverse(n1, n2);
            var n = n1 * n2;
            var temp = ((a2 - a1) * inverse * n1 + a1) % n;
            return (temp < 0 ? n + temp : temp);
        }

        /// <summary>
        /// Computes a list of divisors of n. 
        /// </summary>
        /// <param name="n">Number to find divisors of.</param>
        /// <param name="primeFactors">The prime fators of n (optional).</param>
        /// <param name="primes">A list of pimes (used to compute primeFactors).</param>
        /// <returns></returns>
        public static List<long> Divisors(long n, List<Tuple<long, int>> primeFactors = null, List<long> primes = null)
        {
            var divisors = new List<long>();
            divisors.Add(1);
            if (primeFactors == null)
                primeFactors = PrimeFactors(n, primes);
            foreach (var p in primeFactors)
            {
                var prime = p.Item1;
                var exponent = p.Item2;

                var currentDivisors = divisors.ToArray();
                foreach (var d in currentDivisors)
                {
                    var newDivisor = d;
                    for (int i = 0; i < exponent; i++)
                    {
                        newDivisor *= prime;
                        divisors.Add(newDivisor);
                    }
                }
            }
            divisors.Sort();
            return divisors;
        }

        /// <summary>
        /// Computes the Moebius function, defined as 
        ///              |  0 if n is not square-free
        /// Moebius(n) = |  1 if n has an even number of prime divisors
        ///              | -1 if n has an odd number of prime divisor.
        /// </summary>
        /// <param name="n">The number to evauate the Moebius functon at.</param>
        /// <param name="primeFactors">A list of prime factors of n (optinal).</param>
        /// <param name="primes">A list of pimes (used to compute primeFactors).</param>
        /// <returns></returns>
        public static int Moebius(long n, List<Tuple<long, int>> primeFactors = null, List<long> primes = null)
        {
            if (n == 1)
                return 1;
            if (primeFactors != null)
            {
                foreach (var t in primeFactors)
                {
                    if (t.Item2 > 1)
                        return 0;
                }
                return (primeFactors.Count % 2 == 1) ? 1 : -1;
            }

            if (primes == null)
                primes = PrimeTools.Primes((long)Math.Sqrt(n));
            var exponentCount = 0;
            bool primesCount = true;
            foreach (var p in primes)
            {
                if (n == 1)
                    break;
                if (p * p > n)
                    break;

                exponentCount = 0;
                while (n % p == 0)
                {
                    primesCount = !primesCount;
                    exponentCount++;
                    n /= p;
                }
                if (exponentCount > 1)
                    return 0;
            }
            if (n > 1)
                primesCount = !primesCount;
            return primesCount ? 1 : -1;
        }

        /// <summary>
        /// Calculate Eulers totient function, defined as the number 
        /// of positive integers less than n and relatively prime to n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Totient(long n)
        {
            {
                var primes = PrimeTools.Primes(n);
                if (primes[primes.Count - 1] == n)
                    return n - 1;

                long numPrimes = primes.Count;

                long totient = n;
                long currentNum = n, temp, p, prevP = 0;
                for (int i = 0; i < numPrimes; i++)
                {
                    p = primes[i];
                    if (p > currentNum) break;
                    temp = currentNum / p;
                    if (temp * p == currentNum)
                    {
                        currentNum = temp;
                        i--;
                        if (prevP != p)
                        {
                            prevP = p;
                            totient -= (totient / p);
                        }
                    }
                }
                return totient;
            }
        }

        /// <summary>
        /// Computes the integer part of the square root of n. TODO: implement faster algorithm.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Isqrt(long n)
        {
            return (long)Math.Sqrt(n);
        }

        /// <summary>
        /// Calculates the prime factors (With multiplicity_ of n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="primes">The set of primes to use for checking (optional).</param>
        /// <returns></returns>
        public static List<Tuple<long, int>> PrimeFactors(long n, List<long> primes = null)
        {
            var factors = new List<Tuple<long, int>>();
            if (primes == null)
                primes = PrimeTools.Primes((long)Math.Sqrt(n));
            var exponentCount = 0;
            foreach (var p in primes)
            {
                if (n == 1)
                    break;
                if (p * p > n)
                {
                    factors.Add(new Tuple<long, int>(n, 1));
                    break;
                }
                exponentCount = 0;
                while (n % p == 0)
                {
                    exponentCount++;
                    n /= p;
                }
                if (exponentCount > 0)
                    factors.Add(new Tuple<long, int>(p, exponentCount));
            }
            return factors;
        }

        /// <summary>
        /// Generates a list of tuples (a,b) such that p = a^2+b^2.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<long> SumOfSquares(long p)
        {
            var pairs = new List<long>();
            if (p == 2)
                return new List<long> { 1, 1 };
            if (p % 4 == 3)
                return pairs;
            for (long i = 1; i <= Math.Sqrt(p / 2); i++)
            {
                var candidate = (long)Math.Sqrt(p - i * i);
                if (candidate * candidate + i * i == p)
                {
                    pairs.Add(i);
                    pairs.Add(candidate);
                }
            }
            return pairs;
        }

        /// <summary>
        /// Computes the largest power of prime p which divides k!.
        /// </summary>
        /// <param name="k"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static long LargestPowerDividingFactorial(long k, long p)
        {
            long power = 0;
            while ((k = k/p) > 0)
                power += k;

            return power;
        }

        /// <summary>
        /// Computes m^exp for integers. 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static long IntegralPower(long m, long exp)
        {
            long power = 1;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    power = (power * m);
                exp >>= 1;
                m = m * m;
            }
            return power;
        }

        /// <summary>
        /// Computes a to the power b modulo n.
        /// </summary>
        /// <param name="a">Base</param>
        /// <param name="exp">Power</param>
        /// <param name="n">Modulus</param>
        /// <returns></returns>
        public static long ModPower(long a, long exp, long n)
        {
            if (n == 1) return 0;
            long modPower = 1;

            a = a % n;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    modPower = (modPower * a) % n;
                exp >>= 1;
                a = (a * a) % n;
            }
            return modPower;
        }

        /// <summary>
        /// Computes the Greatest Common Divisor of a and b and returns the generalised modular inveres 
        /// of a and be with respect to the other.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="inverseA"></param>
        /// <param name="inverseB"></param>
        /// <returns></returns>
        public static long Gcd(long a, long b, out long inverseA, out long inverseB)
        {
            var x = new long[2];
            var y = new long[2];

            x[0] = 1; y[0] = 0;
            x[1] = 0; y[1] = 1;
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                var m = (a - b) / temp;
                a = temp;
                var tempX = x[0] - m * x[1];
                var tempY = y[0] - m * y[1];
                x[0] = x[1]; x[1] = tempX;
                y[0] = y[1]; y[1] = tempY;
            }
            if (a < 0)
            {
                inverseA = -x[0];
                inverseB = -y[0];
                return -a;
            }
            inverseA = x[0];
            inverseB = y[0];
            return a;
        }

        /// <summary>
        /// Computes the Greatest Common Divisor of a and b. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Computes the Least Common Multiple of a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long Lcm(long a, long b)
        {
            return (a / Gcd(a, b)) * b;
        }

        /// <summary>
        /// Returns an array of Carmichael numbers less than or equal to n. 
        /// The Carmichael number of n is the smallest exponent e such that all integers
        /// have order at most e mod n. 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long[] CarmichaelArray(long n)
        {
            var tracker = 0;
            var charmichaelArray = new long[n + 1];
            charmichaelArray[1] = 1;

            var numbers = new List<long> { 1 };
            var primes = PrimeTools.Primes(n);

            foreach (var p in primes)
            {

                long factor = p;
                numbers.Sort();
                var numbersLength = numbers.Count;
                if ((numbersLength - tracker) / (double)n > 0.1)
                {
                    tracker = numbersLength;
                }
                if (numbersLength == n)
                    break;
                while (factor <= n)
                {
                    if (p == 2 && factor > 4)
                    {
                        charmichaelArray[factor] = factor / 2 - factor / 4;
                    }
                    else
                        charmichaelArray[factor] = factor - factor / p;
                    numbers.Add(factor);

                    for (int i = 1; i < numbersLength; i++)
                    {
                        var numberToUse = numbers[i];
                        var index = numberToUse * factor;
                        if (index <= n)
                        {
                            charmichaelArray[index] = Lcm(charmichaelArray[numberToUse], charmichaelArray[factor]);
                            numbers.Add(index);
                        }
                        else break;
                    }
                    factor *= p;
                }
            }
            return charmichaelArray;
        }

        /// <summary>
        /// Returns a number e such that n^e = 1 mod baseNumber.
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long MultiplicativeOrder(long baseNumber, long n)
        {
            if (Gcd(baseNumber, n) != 1)
                return 0;
            long currentMod = 1;
            for (int i = 1; true; i++)
            {
                currentMod = (currentMod * baseNumber % n);
                if (currentMod == 1)
                    return i;
            }
        }

        /// <summary>
        /// Computes the modular inverse of a number with respect to n. 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long ModularInverse(long number, long n)
        {
            long result, dummy;
            if (Gcd(number, n, out result, out dummy) != 1)
                return 0;

            return (result % n + n) % n;
        }

        /// <summary>
        /// Checks whether an integer is a perfect square.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPerfectSquare(long n)
        {
            // Quickfail
            if (n < 0 || ((n & 2) != 0) || ((n & 7) == 5) || ((n & 11) == 8))
                return false;
            if (n == 0)
                return true;

            // Check mod 255 = 3 * 5 * 17, for fun
            long y = n;
            y = (y & 0xffffffffL) + (y >> 32);
            y = (y & 0xffffL) + (y >> 16);
            y = (y & 0xffL) + ((y >> 8) & 0xffL) + (y >> 16);
            if (Constants._SquareNumbersLessThan256[(int)y])
                return false;

            // Divide out powers of 4 using binary search
            if ((n & 0xffffffffL) == 0)
                n >>= 32;
            if ((n & 0xffffL) == 0)
                n >>= 16;
            if ((n & 0xffL) == 0)
                n >>= 8;
            if ((n & 0xfL) == 0)
                n >>= 4;
            if ((n & 0x3L) == 0)
                n >>= 2;

            if ((n & 0x7L) != 1)
                return false;

            // Compute sqrt using something like Hensel's lemma
            long r, t, z;
            r = Constants._HenselStartingPosition[(int)((n >> 3) & 0x3ffL)];
            do
            {
                z = n - r * r;
                if (z == 0)
                    return true;
                if (z < 0)
                    return false;
                t = z & (-z);
                r += (z & t) >> 1;
                if (r > (t >> 1))
                    r = t - r;
            } while (t <= (1L << 33));
            return false;
        }

        
    }
}
