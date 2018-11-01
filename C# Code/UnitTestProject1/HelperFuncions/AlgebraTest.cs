using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ProjectEuler.HelperClasses;

namespace ProjectEuler.Test.HelperFuncions
{
    public static class AlgebraTest
    {
        [Theory]
        [InlineData(1, 100, 1, 5050)]
        [InlineData(1, 100, 2, 338350)]
        [InlineData(5, 10, 4, 24979)]
        public static void PowerSumTest(long a, long b, int exponent, long expected)
        {
            //Act
            var actual = Algebra.PowerSum(a, b, exponent);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 100, 1, 5050 % 101)]
        [InlineData(1, 100, 2, 338350 % 101)]
        [InlineData(5, 10, 4, 24979 % 101)]
        public static void PowerSumModdedTest(long a, long b, int exponent, long expected)
        {
            //Act
            var actual = Algebra.PowerSum(a, b, exponent, 101);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
