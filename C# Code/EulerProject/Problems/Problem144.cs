using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectEuler
{
    public class Problem144
    {
        public static long limit = 100;
        public static long Solution()
        {
            long solution = 0;
            const double a = 4;
            const double b = 1;
            
            var A = new Tuple<double,double>(0, 10.1);
            var B = new Tuple<double, double>(1.4, -9.6);
            while (Math.Abs(B.Item1) >= 0.01 || B.Item2 < 0)
            {
                solution++;
                var newPoints = NextReflectedPoint(A, B, a, b);
                A = newPoints.Item1;
                B = newPoints.Item2;


            }
            return solution;
        }

        private static Tuple<Tuple<double, double>, Tuple<double, double>> NextReflectedPoint (Tuple<double, double> A, Tuple<double, double> B, double a, double b)
            {
            var littleM = -4 * B.Item1 / B.Item2;
            var diffX = B.Item1 - A.Item1;
            var diffY = B.Item2 - A.Item2;
            var twoM = 2 * littleM;
            var oneMinusMSquared = 1 - littleM * littleM;
            
            var bigM = (diffX * twoM - diffY * oneMinusMSquared) / (diffX * oneMinusMSquared + diffY * twoM);

            var coeff = 2*(b * bigM * B.Item2 + a * B.Item1) / (a + b * bigM * bigM);
            var C = new Tuple<double, double>(B.Item1 - coeff, B.Item2 - bigM * coeff);
            return new Tuple<Tuple<double, double>, Tuple<double, double>>(B, C);
            }

    }
}