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
                var limit = 1000000000;
                var sum = 0;
                stopwatch.Start();
                for (int i = 1; i < limit; i++)
                {
                    sum += limit / i;
                }
                stopwatch.Stop();
                Console.WriteLine($"Dividing took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                stopwatch.Reset();
                sum = 0;
                stopwatch.Start();
                for (int i = 1; i < limit; i++)
                {
                    sum += limit % i;                    
                }
                stopwatch.Stop();
                Console.WriteLine($"Modding took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                Console.ReadLine();
            }
        }
    }
}
