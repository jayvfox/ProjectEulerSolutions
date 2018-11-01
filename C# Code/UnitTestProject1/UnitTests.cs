using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ProjectEuler.HelperClasses;


namespace ProjectEuler.Test
{
    public class UnitTests
    {

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
        [InlineData("15948","84951")]
        [InlineData("", "")]
        [InlineData("Your Mom, dud3", "3dud ,moM ruoY")]
        public static void ReverseStringTest(string s, string expected)
        {
            //Act
            var actual = UtilityFunctions.ReverseString(s);

            //Assert
            Assert.Equal(expected, actual);
        }
        
    }
}
