using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjectEuler.HelperClasses;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeStuff();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(Problem429.Solution());
            stopwatch.Stop();
            Console.WriteLine($"Solution took {stopwatch.ElapsedMilliseconds} milliseconds.");
            //Console.ReadKey();
        }

        static void TimeStuff()
        {

            
            var stopwatch = new Stopwatch();
            while (true)
            {
                var limit = 100000000;
                stopwatch.Start();
                var primes3 = PrimeTools.Primes(limit);
                stopwatch.Stop();
                Console.WriteLine($"Primes3 took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                stopwatch.Reset();

                stopwatch.Start();
                var primes2 = PrimeTools.Primes(limit);
                stopwatch.Stop();
                Console.WriteLine($"Primes2 took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                stopwatch.Reset();

                stopwatch.Start();
                var primes1 = PrimeTools.Primes(limit);
                stopwatch.Stop();
                Console.WriteLine($"Primes1 took {stopwatch.ElapsedMilliseconds} milliseconds to run.");
                stopwatch.Reset();

                

               
                Console.ReadLine();
            }
        }
    }
}
