
namespace ProjectEuler.HelperClasses
{
    public static class Algebra
    {
        /// <summary>
        /// Computes the sum of k^th powers of consecutive integers. Negative numbers are allowed. 
        /// </summary>
        /// <param name="startNumber">Number to start the sum at (inclusive).</param>
        /// <param name="endNumber">Number to end the sum at (inclusive).</param>
        /// <param name="exponent">The exponent k.</param>
        /// <returns></returns>
        public static long PowerSum(long startNumber, long endNumber, int exponent)
        {
            var sums = new long[exponent + 1];
            sums[0] = endNumber - startNumber + 1;
            for (int i = 1; i <= exponent; i++)
            {
                long newSum = NumberTheory.IntegralPower(endNumber + 1, i + 1) - NumberTheory.IntegralPower(startNumber, i + 1);
                for (int r = 0; r < i; r++)
                {
                    newSum -= Combinatorics.Choose(i + 1, r) * sums[r];
                }
                sums[i] = newSum / (i + 1);
            }
            return sums[exponent];
        }

        /// <summary>
        /// Computes the modular sum of k^th powers of consecutive integers. Negative numbers are allowed. 
        /// </summary>
        /// <param name="startNumber">Number to start the sum at (inclusive).</param>
        /// <param name="endNumber">Number to end the sum at (inclusive).</param>
        /// <param name="exponent">The exponent k.</param>
        /// <param name="modBase">The modulus.</param>
        /// <returns></returns>
        public static long PowerSum(long startNumber, long endNumber, long exponent, long modBase)
        {
            startNumber = startNumber % modBase;
            endNumber = endNumber % modBase;
            var sums = new long[exponent + 1];
            sums[0] = endNumber - startNumber + 1;
            for (long i = 1; i <= exponent; i++)
            {
                long newSum = (NumberTheory.ModPower(endNumber + 1, i + 1, modBase) - NumberTheory.ModPower(startNumber, i + 1, modBase)) % modBase;
                for (long r = 0; r < i; r++)
                {
                    newSum -= ((Combinatorics.Choose(i + 1, r) % modBase) * sums[r]) % modBase;
                }
                var inverse = NumberTheory.ModularInverse(i + 1, modBase);
                if (inverse == 0)
                    sums[i] = sums[0];
                else
                    sums[i] = (newSum * inverse) % modBase;
            }
            return (sums[exponent] + modBase) % modBase;
        }
    }
}
