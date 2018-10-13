using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeStuff();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(Problem151.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadKey();
        }

        static void TimeStuff()
        {
            var stopwatch = new Stopwatch();
            while (true)
            {
                for (int i = 2; i < 100; i++)
                    if (UtilityFunctions.IsPrime(i))
                        Console.WriteLine(i);

                long number = 188748146801;
                stopwatch.Restart();
                var isPrime = UtilityFunctions.IsPrime(number);
                stopwatch.Stop();
                Console.WriteLine($"IsPrime took {stopwatch.ElapsedMilliseconds} milliseconds and returned {isPrime}.");
                stopwatch.Restart();
                number = 99194853094755497;
                isPrime = UtilityFunctions.IsPrime(number);  
                stopwatch.Stop();
                Console.WriteLine($"IsPrime took {stopwatch.ElapsedMilliseconds} milliseconds and returned {isPrime}.");

                Console.ReadKey();
            }
        }
    }
}
