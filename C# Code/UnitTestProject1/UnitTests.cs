using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace ProjectEuler.Test
{
    public class UnitTests
    {

        [Fact]
        public void NonSquareFreeIntegersTest()
        {
            //Arrange
            var expected = new List<long> {4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64 ,68 ,72, 76, 80, 84, 88, 92, 96, 100, 9, 18, 27, 45, 54, 63, 81, 90, 99, 25, 50, 75, 49, 98};
            expected.Sort();

            // Act
            var actual = UtilityFunctions.NonSquareFreeIntegers(100);
            actual.Sort();

            // Assert
            Assert.Equal(expected.ToArray(), actual.ToArray());
        }

        [Fact]
        public void SquareFreeIntegersTest()
        {
            //Arrange
            var expected = new List<long> {1, 2, 3, 5, 6, 7, 10, 11, 13, 14, 15, 17, 19, 21, 22, 23, 26, 29, 30,
                                          31, 33, 34, 35, 37, 38, 39, 41, 42, 43, 46, 47, 51, 53, 55, 57, 58, 59, 61,
                                          62, 65, 66, 67, 69, 70, 71,73,74, 77,78, 79, 82, 83, 85, 86,87, 89, 91,93,94,95,97};

            // Act
            List<long> dummy;
            var actual = UtilityFunctions.SquareFreeIntegers(100, out dummy);
            actual.Sort();

            // Assert
            Assert.Equal(expected.ToArray(), actual.ToArray());
        }

        [Fact]
        public void PartitionIntoPerfectSquaresTest()
        {
            //Arrange
            var expected5 = new List<long[]> { new long[] { 5, 0 }, new long[] { 1, 1 } }.ToArray();
            var expected23 = new List<long[]>
            {
                new long[] { 3, 1, 0, 1 },
                new long[] { 7, 0, 0, 1 },
                new long[] { 1, 1, 2, 0 },
                new long[] { 5, 0, 2, 0 },
                new long[] { 2, 3, 1, 0 },
                new long[] { 6, 2, 1, 0 },
                new long[] { 10, 1, 1, 0 },
                new long[] { 14, 0, 1, 0 },
                new long[] { 3, 5, 0, 0 },
                new long[] { 7, 4, 0, 0 },
                new long[] { 11, 3, 0, 0 },
                new long[] { 15, 2, 0, 0 },
                new long[] { 19, 1, 0, 0 },
                new long[] { 23, 0, 0, 0 }
            }.ToArray().Reverse();


            // Act
            var actual5 = UtilityFunctions.PartitionIntoPerfectSquares(5).ToArray();
            var actual23 = UtilityFunctions.PartitionIntoPerfectSquares(23).ToArray();

            // Assert
            Assert.Equal(expected5, actual5);
            Assert.Equal(expected23, actual23);
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
        public void IsPrimesTest()
        {
            var primes = new List<long> { 2, 83, 1000001531, 68426879, 124699 };
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
        public void CarmichaelFunctionTest()
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
        public void MultiplicativeOrderTest(long a, long n, long expected)
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
        public void GcdTest(long a, long b, long expected)
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
        public void LcmTest(long a, long b, long expected)
        {
            //Act
            var actual = UtilityFunctions.Lcm(a, b);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 85, new long[] { 6, -7 })]
        public void GcdInverseTest(long a, long b, long[] expected)
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
        public void ModularInverseTest(long a, long n, long expected)
        {
            //Act
            var actual = UtilityFunctions.ModularInverse(a, n);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsPerfectSquareTest()
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

    }
}
