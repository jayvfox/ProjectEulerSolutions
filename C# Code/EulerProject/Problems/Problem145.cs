using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    public class Problem145
    {
        public static long limit = 9;
        public static long Solution()
        {
            long solution = 0;

            for (long i = 2; i <= limit; i++)
            {
                if ((i % 2) == 0)
                {
                    var k = i / 2;
                    solution += NumberTheory.IntegralPower(30, k - 1) * 20;
                }
              // else if(i%4 == 1)
              // {
              //     var k = (i - 1) / 4;
              //     solution += NumberTheory.IntegralPower(600, k) * 5;
              // }
                else if (i % 4 == 3)
                {
                    var k = (i + 1) / 4;
                    solution += NumberTheory.IntegralPower(500, k - 1) * 100;
                }

            }
                        
            return solution;
        }
    }
}