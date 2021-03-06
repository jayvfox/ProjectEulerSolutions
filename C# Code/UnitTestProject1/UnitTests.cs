﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace ProjectEuler.Test
{
    public class UnitTests
    {

        [Fact]
        public static void GeneratePermutationsTest()
        {
            //Arrange
            var elements  = new List<int> { 1, 2, 3, 4};
            var expected = new List<List<int>> { new List<int> {2,1 }, new List<int> { 3, 1 }, new List<int> { 4, 1 },
                new List<int> {1,2 }, new List<int> { 3, 2 }, new List<int> { 4, 2 },
                new List<int> {1,3 }, new List<int> { 2, 3 }, new List<int> { 4, 3 },
                new List<int> {1,4 }, new List<int> { 2, 4 }, new List<int> { 3, 4 },
            };

            //Act
            var actual = UtilityFunctions.GeneratePermutations(elements,2);

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
            var actual = UtilityFunctions.Moebius(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void DivisorTest()
        {
            //Arrange
            long n = 540;
            var expected = new List<long> { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 27, 30, 36, 45, 54, 60, 90, 108, 135, 180, 270 , 540};

            //Act
            var actual = UtilityFunctions.Divisors(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void FactorisationTest()
        {
            //Arrange
            long n = 76905467955;
            var expected = new List<Tuple<long, int>> { new Tuple<long, int>(3,3), new Tuple<long, int>(5,1), new Tuple<long, int>(71,1), new Tuple<long, int>(8023523,1) };

            //Act
            var actual = UtilityFunctions.PrimeFactors(n);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1, 100, 1, 5050)]
        [InlineData(1, 100, 2, 338350)]
        [InlineData(5, 10, 4, 24979)]
        public static void PowerSumTest(long a, long b, int exponent, long expected)
        {
            //Act
            var actual = UtilityFunctions.PowerSum(a, b, exponent);

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
            var actual = UtilityFunctions.PowerSum(a, b, exponent, 101);

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
            var actual = UtilityFunctions.LargestPowerDividingFactorial(n,p);

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
            var actual = UtilityFunctions.Isqrt(n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void DigitsTest()
        {
            // Arrange 
            long n = 6872368725336316;
            List<int> expected = new List<int> { 6, 8, 7, 2, 3, 6, 8, 7, 2, 5, 3, 3, 6, 3, 1, 6 };
            expected.Reverse();
            //Act
            var actual = UtilityFunctions.Digits(n);

            //Assert
            Assert.True(actual.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 4, 625)]
        [InlineData(10, 9, 1000000000)]
        [InlineData(7, 0, 1)]
        public void IntegerPowerTest(long a, long exp, long expected)
        {
            //Act
            var actual = UtilityFunctions.IntegralPower(a,exp);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("15948","84951")]
        [InlineData("", "")]
        [InlineData("Your Mom, dud3", "3dud ,moM ruoY")]
        public void ReverseStringTest(string s, string expected)
        {
            //Act
            var actual = UtilityFunctions.ReverseString(s);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GivenElements_GeneratePermutations()
        {
            //Arrange
            var input = new List<string> { "1", "2", "3" };
            var expected1 = new List<string>
            {
                "123",
                "132",
                "213",
                "231",
                "312",
                "321"
            };
            var expected2 = new List<string> { "12", "13", "21", "23", "31", "32" };

            //Act
            var actual1 = UtilityFunctions.GeneratePermutations(input, 3);
            var actual2 = UtilityFunctions.GeneratePermutations(input, 2);
            

            //Assert
            Assert.True(Enumerable.SequenceEqual(expected1, actual1));
            Assert.True(Enumerable.SequenceEqual(expected2, actual2));
        }

        [Fact]
        public void GivenlongegerElements_GeneratePermuations()
        {
            //Arrange
            var input = new List<long> { 1, 2, 3 };
            var expected1 = new List<long>
            {
               123,
               132,
               213,
               231,
               312,
               321
            };
            var expected2 = new List<long> { 12, 13, 21, 23, 31, 32 };

            //Act
            var actual1 = UtilityFunctions.GeneratePermutations(input, 3);
            var actual2 = UtilityFunctions.GeneratePermutations(input, 2);


            //Assert
            Enumerable.SequenceEqual(expected1, actual1);
            Enumerable.SequenceEqual(expected2, actual2);
        }

        [Fact]
        public void IsPrimeTest()
        {
            var primes = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61,67, 71,73, 79, 83, 1000001531, 68426879, 124699, 188748146801};
            var composites = new List<long> { 124699 * 83, 91, 1001, 1741 * 1741 };
            var allArePrime = true;
            primes.ForEach(p => allArePrime &= UtilityFunctions.IsPrime(p));
            composites.ForEach(c => allArePrime &= !UtilityFunctions.IsPrime(c));

            Assert.True(allArePrime);

        }

        [Fact]
        public void Givenlongegers_GenerateSubsets()
        {
            //Arrange
            var elements = new List<long>{ 1, 2, 3 };
            var expected = new List<List<long>>
            {
                new List<long>{ },
                new List<long>{ 1},
                new List<long>{ 2},
                new List<long>{ 1,2},
                new List<long>{ 3},
                new List<long>{ 1,3},
                new List<long>{ 2,3},
                new List<long>{ 1,2,3},
            };

            //Act
            var actual = UtilityFunctions.GenerateSubsets(elements);

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
                Assert.True(Enumerable.SequenceEqual(expected[i], actual[i]));

        }

        [Fact]
        public void GivenSubset_ComputeComplement()
        {
            //Arrange
            var reference = new List<long> { 1, 2, 3, 4, 5 };
            var subset1 = new List<long> { 1, 3, 5 };
            var subset2 = new List<long> { 2, 3, 7, 8 };
            var subset3 = new List<long> { 1, 2, 3, 4, 5 };
            var expected1 = new List<long> { 2, 4 };
            var expected2 = new List<long> { 1, 4, 5 };
            var expected3 = new List<long>();

            //Act
            var actual1 = UtilityFunctions.Complement(subset1, reference);
            var actual2 = UtilityFunctions.Complement(subset2, reference);
            var actual3 = UtilityFunctions.Complement(subset3, reference);

            //Assert
            Assert.True(Enumerable.SequenceEqual(expected1, actual1));
            Assert.True(Enumerable.SequenceEqual(expected2, actual2));
            Assert.True(Enumerable.SequenceEqual(expected3, actual3));
        }

        [Fact]
        public void GivenNumber_ComputePhi()
        {
            //Arrange
            var numbers = new long[] { 2, 3, 11, 15, 25, 100, 125 };
            var expected = new long[] { 1, 2, 10, 8, 20, 40, 100 };
            var actual = new long[numbers.Length];
            //Act
            for (int i = 0; i < numbers.Length; i++)
            {
                actual[i] = UtilityFunctions.Totient(numbers[i]);
            }

            //Assert
            Enumerable.SequenceEqual(expected, actual);
        }

        [Theory]
        [InlineData(2,3,5,3)]
        [InlineData(11, 40, 100, 1)]
        [InlineData(7, 40, 100, 1)]
        [InlineData(109, 40, 100, 1)]
        public void TestModularPower(long a, long b, long n, long expected)
        {
            //Act
            var actual = UtilityFunctions.ModPower(a, b, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestCarmichaelFunction()
        {
            //Arrange 
            var expected = new long[] { 0, 1, 1, 2, 2, 4, 2, 6, 2, 6, 4, 10, 2, 12, 6, 4, 4, 16, 6, 18, 4, 6, 10, 22, 2, 20, 12, 18, 6, 28, 4, 30, 8, 10, 16, 12, 6 };

            //Act
            var actual = UtilityFunctions.CarmichaelArray(36);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(2, 3, 2)]
        [InlineData(7, 100, 4)]
        [InlineData(3, 34, 16)]
        public void TestMultiplicativeOrder(long a, long n, long expected)
        {
            //Act
            var actual = UtilityFunctions.MultiplicativeOrder(a, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, 5)]
        [InlineData(111111, 629 , 37)]
        [InlineData(101, 940654, 1)]
        public void TestGcd(long a, long b, long expected)
        {
            //Act
            var actual = UtilityFunctions.Gcd(a, b);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, 1700)]
        [InlineData(111111, 629, 1888887)]
        [InlineData(101, 940654, 101* 940654)]
        public void TestLcm(long a, long b, long expected)
        {
            //Act
            var actual = UtilityFunctions.Lcm(a, b);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, new long[] { 6, -7 })]
        public void TestGcdInverse(long a, long b, long[] expected)
        {
            //Act
            var actual = new long[2];
            UtilityFunctions.Gcd(a, b, out actual[0], out actual[1]);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2,5,3)]
        [InlineData(7, 100, 43)]
        public void TestModularInverse(long a, long n, long expected)
        {
            //Act
            var actual = UtilityFunctions.ModularInverse(a, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestIsPerfectSquare()
        {
            //Act
            Random rnd = new Random();
            long n;
            for (int i = 0; i < 10; i++)
            {
                n = rnd.Next();
                var actual = UtilityFunctions.IsPerfectSquare(n*n);
                Assert.True(actual);
                n = rnd.Next();
                actual = UtilityFunctions.IsPerfectSquare(n * n+1);
                Assert.False(actual);
            }

            //Assert
            
        }

        [Theory]
        [InlineData(5,2,10)]
        [InlineData(20, 5, 15504)]
        [InlineData(100, 10, 17310309456440)]
        public void ChooseTest(long n, long k, long expected)
        {
            //Act
            var actual = UtilityFunctions.Choose(n, k);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 4, new long[] { 1, 2, 2 })]
        [InlineData(10, 7, new long[] { 4, 6})]
        [InlineData(10, 16, new long[] { 1, 2, 2, 5 })]
        public void PartitionTest(long n, int position, long[] expected)
        {
            //Act
            var actual = UtilityFunctions.Partition(n)[n][position].ToArray();

            //Assert
            Assert.Equal(expected, actual);
            
        }


    }
}
