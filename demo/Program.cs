namespace DioLive.Combinatorix.Demo
{
    using Core;
    using Core.Generic;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class Program
    {
        private struct Results
        {
            public int Counter;
            public TimeSpan Time;
        }

        public static void Main()
        {
#if DEBUG
            Console.Read();
#endif

            string sourceString = "0123456789012";
            var sourceArray = sourceString.ToCharArray(); //Enumerable.Range(0, 20).ToArray();
            var count = 4;

            Console.Write("NonGeneric: ");
            Console.WriteLine(Measure(() => NonGeneric(sourceString.GetEnumerator(), count)));

            Console.Write("Generic: ");
            Console.WriteLine(Measure(() => Generic(sourceArray, count)));
        }

        private static TimeSpan Measure(Func<Results> function)
        {
            return TimeSpan.FromTicks((long)
                 Enumerable.Range(0, 30)
                     .Select(i => function())
                     .Average(r => r.Time.Ticks)
            );
        }

        private static Results Generic<T>(ICollection<T> items, int count)
        {
            //var combs = Combinatorix.Combinations(items, count);
            //var combs = Combinatorix.Permutations(items);
            var combs = Combinatorix.Variations(items, count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int nCounter = 0;
            foreach (var thisComb in combs)
            {
                nCounter++;
                for (int i = 0; i < thisComb.Length; i++)
                {
                    T nVal = thisComb[i];
                    //Console.Write("{0}, ", nVal);
                }

                //Console.WriteLine();
            }

            stopwatch.Stop();

            return new Results { Counter = nCounter, Time = stopwatch.Elapsed };
        }

        private static Results NonGeneric(System.Collections.IEnumerator sourceEnumerator, int count)
        {
            //var combs = new Combinations(sourceEnumerator, count);
            //var combs = new Permutations(sourceEnumerator);
            var combs = new Variations(sourceEnumerator, count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int nCounter = 0;
            while (combs.MoveNext())
            {
                Array thisComb = (Array)combs.Current;
                nCounter++;
                for (int i = 0; i < thisComb.Length; i++)
                {
                    //int nVal = (int)thisComb.GetValue(i);	// Just access the value. This requres boxing.
                    Object nVal = thisComb.GetValue(i);     // Just access the value. This requres no boxing.
                    //Console.Write("{0}, ", nVal);
                }

                //Console.WriteLine();
            }

            stopwatch.Stop();

            return new Results { Counter = nCounter, Time = stopwatch.Elapsed };
        }
    }
}