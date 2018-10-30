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
            Console.WriteLine(Problem639.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            //Console.ReadKey();
        }

        static void TimeStuff()
        {
            var stopwatch = new Stopwatch();
            while (true)
            {
                var limit = 10000000;
                long sum = 0;
                stopwatch.Start();
                for (int i = 1; i <= limit; i++)
                {
                    sum = (sum + UtilityFunctions.ModPower(i,50,1000000007)) % 1000000007;
                }
                stopwatch.Stop();
                Console.WriteLine($"Summing took {stopwatch.ElapsedMilliseconds} milliseconds to run: {sum}");
                stopwatch.Reset();
                sum = 0;
                stopwatch.Start();
                sum = UtilityFunctions.PowerSum(1, limit, 50, 1000000007);
                stopwatch.Stop();
                Console.WriteLine($"Formula took {stopwatch.ElapsedMilliseconds} milliseconds to run: {sum}");
                Console.ReadLine();
            }
        }
    }
}
