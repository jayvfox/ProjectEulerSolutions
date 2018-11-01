using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ProjectEuler.HelperClasses;

namespace ProjectEuler.Test.HelperFuncions
{
    public static class NumberTheoryTest
    {
        [Theory]
        [InlineData(1, 2, 2, 5, 7)]
        [InlineData(0, 1, 3, 5, 6)]
        [InlineData(3, 6, 5, 9, 33)]
        public static void ChineseRemainderTheoremTest(int a, int b, int n, int m, int expected)
        {
            //Act
            var actual = NumberTheory.ChineseRemainderTheorem(a, b, n, m);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(24, 0)]
        [InlineData(1681, 0)]
        [InlineData(15, 1)]
        [InlineData(105, -1)]
        [InlineData(210, 1)]
        [InlineData(3672663, -1)]
        public static void MoebiusTest(long n, int expected)
        {
            //Act
            var actual = NumberTheory.Moebius(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void DivisorTest()
        {
            //Arrange
            long n = 540;
            var expected = new List<long> { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 27, 30, 36, 45, 54, 60, 90, 108, 135, 180, 270, 540 };

            //Act
            var actual = NumberTheory.Divisors(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void FactorisationTest()
        {
            //Arrange
            long n = 76905467955;
            var expected = new List<Tuple<long, int>> { new Tuple<long, int>(3, 3), new Tuple<long, int>(5, 1), new Tuple<long, int>(71, 1), new Tuple<long, int>(8023523, 1) };

            //Act
            var actual = NumberTheory.PrimeFactors(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(50, 7, 8)]
        [InlineData(200, 5, 49)]
        [InlineData(1000, 7, 164)]
        public static void LargestPowerDividingFactorialTest(long n, long p, long expected)
        {
            //Act
            var actual = NumberTheory.LargestPowerDividingFactorial(n, p);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(256, 16)]
        [InlineData(200, 14)]
        [InlineData(1000, 31)]
        [InlineData(68743871, 8291)]
        [InlineData(20865628187880, 4567890)]
        public static void iSqrtTest(long n, long expected)
        {
            //Act
            var actual = NumberTheory.Isqrt(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 4, 625)]
        [InlineData(10, 9, 1000000000)]
        [InlineData(7, 0, 1)]
        public static void IntegerPowerTest(long a, long exp, long expected)
        {
            //Act
            var actual = NumberTheory.IntegralPower(a, exp);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void GivenNumber_ComputePhi()
        {
            //Arrange
            var numbers = new long[] { 2, 3, 11, 15, 25, 100, 125 };
            var expected = new long[] { 1, 2, 10, 8, 20, 40, 100 };
            var actual = new long[numbers.Length];
            //Act
            for (int i = 0; i < numbers.Length; i++)
            {
                actual[i] = NumberTheory.Totient(numbers[i]);
            }

            //Assert
            Enumerable.SequenceEqual(expected, actual);
        }

        [Theory]
        [InlineData(2, 3, 5, 3)]
        [InlineData(11, 40, 100, 1)]
        [InlineData(7, 40, 100, 1)]
        [InlineData(109, 40, 100, 1)]
        public static void TestModularPower(long a, long b, long n, long expected)
        {
            //Act
            var actual = NumberTheory.ModPower(a, b, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void TestCarmichaelFunction()
        {
            //Arrange 
            var expected = new long[] { 0, 1, 1, 2, 2, 4, 2, 6, 2, 6, 4, 10, 2, 12, 6, 4, 4, 16, 6, 18, 4, 6, 10, 22, 2, 20, 12, 18, 6, 28, 4, 30, 8, 10, 16, 12, 6 };

            //Act
            var actual = NumberTheory.CarmichaelArray(36);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(2, 3, 2)]
        [InlineData(7, 100, 4)]
        [InlineData(3, 34, 16)]
        public static void TestMultiplicativeOrder(long a, long n, long expected)
        {
            //Act
            var actual = NumberTheory.MultiplicativeOrder(a, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, 5)]
        [InlineData(111111, 629, 37)]
        [InlineData(101, 940654, 1)]
        public static void TestGcd(long a, long b, long expected)
        {
            //Act
            var actual = NumberTheory.Gcd(a, b);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, 1700)]
        [InlineData(111111, 629, 1888887)]
        [InlineData(101, 940654, 101 * 940654)]
        public static void TestLcm(long a, long b, long expected)
        {
            //Act
            var actual = NumberTheory.Lcm(a, b);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, new long[] { 6, -7 })]
        public static void TestGcdInverse(long a, long b, long[] expected)
        {
            //Act
            var actual = new long[2];
            NumberTheory.Gcd(a, b, out actual[0], out actual[1]);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 5, 3)]
        [InlineData(7, 100, 43)]
        public static void TestModularInverse(long a, long n, long expected)
        {
            //Act
            var actual = NumberTheory.ModularInverse(a, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void TestIsPerfectSquare()
        {
            //Act
            Random rnd = new Random();
            long n;
            for (int i = 0; i < 10; i++)
            {
                n = rnd.Next();
                var actual = NumberTheory.IsPerfectSquare(n * n);
                Assert.True(actual);
                n = rnd.Next();
                actual = NumberTheory.IsPerfectSquare(n * n + 1);
                Assert.False(actual);
            }
            //Assert
        }
    }
}
