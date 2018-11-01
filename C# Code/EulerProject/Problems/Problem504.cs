using ProjectEuler.HelperClasses;


namespace ProjectEuler
{
    public class Problem504
    {
        public static long limit = 100;
        public static long Solution()
        {
            long solution = 0;

            for (int a = 1; a <= limit; a++)
            {
                for (int c = 1; c <= a; c++)
                {
                    for (int b = 1; b <= limit; b++)
                    {
                        for (int d = 1; d <= b; d++)
                        {
                            var internalPoints =
                                ((a + c) * (b + d)
                                - NumberTheory.Gcd(a, b)
                                - NumberTheory.Gcd(b, c)
                                - NumberTheory.Gcd(c, d)
                                - NumberTheory.Gcd(d, a)) / 2 + 1;
                            if (NumberTheory.IsPerfectSquare(internalPoints))
                            {
                                solution += 4;
                                if (a==c || b==d)
                                {
                                    solution -= 2;
                                    if (a==c && b == d)
                                    {
                                        solution -= 1;
                                    }
                                        
                                }
                            }
                        }
                    }
                }
            }


            return solution;
        }
    }
}