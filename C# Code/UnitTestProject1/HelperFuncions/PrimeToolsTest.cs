using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ProjectEuler.HelperClasses;

namespace ProjectEuler.Test.HelperFuncions
{
    public static class PrimeToolsTest
    {
        [Fact]
        public static void IsPrimeTest()
        {
            var primes = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 1000001531, 68426879, 124699, 188748146801 };
            var composites = new List<long> { 124699 * 83, 91, 1001, 1741 * 1741 };
            var allArePrime = true;
            primes.ForEach(p => allArePrime &= PrimeTools.IsPrime(p));
            composites.ForEach(c => allArePrime &= !PrimeTools.IsPrime(c));

            Assert.True(allArePrime);

        }
    }
}
