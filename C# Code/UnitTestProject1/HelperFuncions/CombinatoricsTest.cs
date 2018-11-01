using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ProjectEuler.HelperClasses;

namespace ProjectEuler.Test.HelperFuncions
{
    public static class CombinatoricsTest
    {
        [Fact]
        public static void GeneratePermutationsTest()
        {
            //Arrange
            var elements = new List<int> { 1, 2, 3, 4 };
            var expected = new List<List<int>> { new List<int> {2,1 }, new List<int> { 3, 1 }, new List<int> { 4, 1 },
                new List<int> {1,2 }, new List<int> { 3, 2 }, new List<int> { 4, 2 },
                new List<int> {1,3 }, new List<int> { 2, 3 }, new List<int> { 4, 3 },
                new List<int> {1,4 }, new List<int> { 2, 4 }, new List<int> { 3, 4 },
            };

            //Act
            var actual = Combinatorics.GeneratePermutations(elements, 2);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public static void GivenElements_GeneratePermutations()
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
            var actual1 = Combinatorics.GeneratePermutations(input, 3);
            var actual2 = Combinatorics.GeneratePermutations(input, 2);


            //Assert
            Assert.True(Enumerable.SequenceEqual(expected1, actual1));
            Assert.True(Enumerable.SequenceEqual(expected2, actual2));
        }

        [Fact]
        public static void GivenlongegerElements_GeneratePermuations()
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
            var actual1 = Combinatorics.GeneratePermutations(input, 3);
            var actual2 = Combinatorics.GeneratePermutations(input, 2);


            //Assert
            Enumerable.SequenceEqual(expected1, actual1);
            Enumerable.SequenceEqual(expected2, actual2);
        }

        [Fact]
        public static void Givenlongegers_GenerateSubsets()
        {
            //Arrange
            var elements = new List<long> { 1, 2, 3 };
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
            var actual = Combinatorics.GenerateSubsets(elements);

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
                Assert.True(Enumerable.SequenceEqual(expected[i], actual[i]));

        }

        [Fact]
        public static void ComplementTest()
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
            var actual1 = Combinatorics.Complement(subset1, reference);
            var actual2 = Combinatorics.Complement(subset2, reference);
            var actual3 = Combinatorics.Complement(subset3, reference);

            //Assert
            Assert.True(Enumerable.SequenceEqual(expected1, actual1));
            Assert.True(Enumerable.SequenceEqual(expected2, actual2));
            Assert.True(Enumerable.SequenceEqual(expected3, actual3));
        }

        [Theory]
        [InlineData(5, 2, 10)]
        [InlineData(20, 5, 15504)]
        [InlineData(100, 10, 17310309456440)]
        public static void ChooseTest(long n, long k, long expected)
        {
            //Act
            var actual = Combinatorics.Choose(n, k);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 4, new long[] { 1, 2, 2 })]
        [InlineData(10, 7, new long[] { 4, 6 })]
        [InlineData(10, 16, new long[] { 1, 2, 2, 5 })]
        public static void PartitionTest(long n, int position, long[] expected)
        {
            //Act
            var actual = Combinatorics.Partition(n)[n][position].ToArray();

            //Assert
            Assert.Equal(expected, actual);

        }

    }
}
