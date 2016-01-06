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
        private const char PROGRESS_CHAR = '▓';

        public static void Main()
        {
#if DEBUG
            // Manual delay for slow performance analyzers
            Console.WriteLine("Press any key to start");
            Console.Read();
#endif

            //TestsWithStrings();
            TestsWithIntArrays();
        }

        private static void TestsWithStrings()
        {
            #region Combinations

            Console.WriteLine("\t[ Testing: Combinations ]");

            TestString(Tests.Combinations, "0123456789012345678901234", 3, 20);
            TestString(Tests.Combinations, "0123456789012345678901234", 4, 20);
            TestString(Tests.Combinations, "0123456789012345678901234", 5, 20);

            TestString(Tests.Combinations, "0123456789012", 4, 30);
            TestString(Tests.Combinations, "0123456789012", 6, 30);

            TestString(Tests.Combinations, "012345", 3, 30);
            TestString(Tests.Combinations, "012345", 5, 30);

            #endregion Combinations

            #region Permutations

            Console.WriteLine("\t[ Testing: Permutations ]");

            const int DOES_NOT_MATTER = 0;

            TestString(Tests.Permutations, "01234567", DOES_NOT_MATTER, 30);
            TestString(Tests.Permutations, "012345", DOES_NOT_MATTER, 30);
            TestString(Tests.Permutations, "0123", DOES_NOT_MATTER, 30);

            #endregion Permutations

            #region Variations

            Console.WriteLine("\t[ Testing: Variations ]");

            TestString(Tests.Variations, "0123456789012345678901234", 3, 20);
            TestString(Tests.Variations, "0123456789012345678901234", 4, 20);

            TestString(Tests.Variations, "0123456789012", 4, 30);
            TestString(Tests.Variations, "0123456789012", 6, 30);

            TestString(Tests.Variations, "012345", 3, 30);
            TestString(Tests.Variations, "012345", 5, 30);

            #endregion Variations
        }

        private static void TestsWithIntArrays()
        {
            #region Combinations

            Console.WriteLine("\t[ Testing: Combinations ]");

            TestIntArray(Tests.Combinations, Enumerable.Range(0, 25).ToArray(), 3, 20);
            TestIntArray(Tests.Combinations, Enumerable.Range(0, 25).ToArray(), 4, 20);
            TestIntArray(Tests.Combinations, Enumerable.Range(0, 25).ToArray(), 5, 20);

            TestIntArray(Tests.Combinations, Enumerable.Range(0, 13).ToArray(), 4, 30);
            TestIntArray(Tests.Combinations, Enumerable.Range(0, 13).ToArray(), 6, 30);

            TestIntArray(Tests.Combinations, Enumerable.Range(0, 6).ToArray(), 3, 30);
            TestIntArray(Tests.Combinations, Enumerable.Range(0, 6).ToArray(), 5, 30);

            #endregion Combinations

            #region Permutations

            Console.WriteLine("\t[ Testing: Permutations ]");

            const int DOES_NOT_MATTER = 0;

            TestIntArray(Tests.Permutations, Enumerable.Range(0, 8).ToArray(), DOES_NOT_MATTER, 30);
            TestIntArray(Tests.Permutations, Enumerable.Range(0, 6).ToArray(), DOES_NOT_MATTER, 30);
            TestIntArray(Tests.Permutations, Enumerable.Range(0, 4).ToArray(), DOES_NOT_MATTER, 30);

            #endregion Permutations

            #region Variations

            Console.WriteLine("\t[ Testing: Variations ]");

            TestIntArray(Tests.Variations, Enumerable.Range(0, 25).ToArray(), 3, 20);
            TestIntArray(Tests.Variations, Enumerable.Range(0, 25).ToArray(), 4, 20);

            TestIntArray(Tests.Variations, Enumerable.Range(0, 13).ToArray(), 4, 30);
            TestIntArray(Tests.Variations, Enumerable.Range(0, 13).ToArray(), 6, 30);

            TestIntArray(Tests.Variations, Enumerable.Range(0, 6).ToArray(), 3, 30);
            TestIntArray(Tests.Variations, Enumerable.Range(0, 6).ToArray(), 5, 30);

            #endregion Variations
        }

        private static void TestString(Tests test, string sourceString, int size, int repeats)
        {
            Console.WriteLine($"=== \"{sourceString}\" per {size} ({repeats} repeats)");
            var sourceArray = sourceString.ToCharArray(); //Enumerable.Range(0, 20).ToArray();
            NonGenericTest(test, sourceString.GetEnumerator(), size, repeats);
            GenericTest(test, sourceArray, size, repeats);
            Console.WriteLine();
        }

        private static void TestIntArray(Tests test, int[] sourceArray, int size, int repeats)
        {
            Console.WriteLine($"=== \"int[{sourceArray.Length}]\" per {size} ({repeats} repeats)");
            NonGenericTest(test, sourceArray.GetEnumerator(), size, repeats);
            GenericTest(test, sourceArray, size, repeats);
            Console.WriteLine();
        }

        private static void NonGenericTest(Tests test, System.Collections.IEnumerator sourceEnumerator, int size, int repeats)
        {
            TimeSpan averageTime;
            switch (test)
            {
                case Tests.Combinations:
                    averageTime = MeasureAverageTime(() => MeasureNonGeneric(new Combinations(sourceEnumerator, size)), repeats);
                    break;

                case Tests.Permutations:
                    averageTime = MeasureAverageTime(() => MeasureNonGeneric(new Permutations(sourceEnumerator)), repeats);
                    break;

                case Tests.Variations:
                    averageTime = MeasureAverageTime(() => MeasureNonGeneric(new Variations(sourceEnumerator, size)), repeats);
                    break;

                default:
                    throw new ArgumentException("Unknown test: " + test, nameof(test));
            }

            Console.WriteLine($"\rNonGeneric: \t{averageTime}");
        }

        private static void GenericTest<T>(Tests test, ICollection<T> sourceItems, int size, int repeats)
        {
            TimeSpan averageTime;
            switch (test)
            {
                case Tests.Combinations:
                    averageTime = MeasureAverageTime(() => MeasureGeneric(Combinatorix.Combinations(sourceItems, size)), repeats);
                    break;

                case Tests.Permutations:
                    averageTime = MeasureAverageTime(() => MeasureGeneric(Combinatorix.Permutations(sourceItems)), repeats);
                    break;

                case Tests.Variations:
                    averageTime = MeasureAverageTime(() => MeasureGeneric(Combinatorix.Variations(sourceItems, size)), repeats);
                    break;

                default:
                    throw new ArgumentException("Unknown test: " + test, nameof(test)); ;
            }

            Console.WriteLine($"\rGeneric: \t{averageTime}");
        }

        private static TimeSpan MeasureAverageTime(Func<MeasureResult> function, int repeats = 1)
        {
            if (repeats < 0)
            {
                throw new ArgumentException("Repeats count can not be negative", nameof(repeats));
            }

            if (repeats == 0)
            {
                return TimeSpan.Zero;
            }

            if (repeats == 1)
            {
                return function().Time;
            }

            return TimeSpan.FromTicks((long)
                 Enumerable.Range(0, repeats)
                     .Select(i => function())
                     .Average(r => r.Time.Ticks)
            );
        }

        private static MeasureResult MeasureNonGeneric(CombinatorialBase combs)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int nCounter = 0;
            while (combs.MoveNext())
            {
                Array thisComb = (Array)combs.Current;
                nCounter++;
                for (int i = 0; i < thisComb.Length; i++)
                {
                    //int nVal = (int)thisComb.GetValue(i);	// Just access the value. This requres boxing.
                    object nVal = thisComb.GetValue(i);     // Just access the value. This requres no boxing.
#if VERBOSE
                    Console.Write("{0}, ", nVal);
#endif
                }

#if VERBOSE
                Console.WriteLine();
#endif
            }

            stopwatch.Stop();

#if !VERBOSE
            Console.Write(PROGRESS_CHAR);
#endif

            return new MeasureResult(nCounter, stopwatch.Elapsed);
        }

        private static MeasureResult MeasureGeneric<T>(CombinatorixBase<T> combs)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int nCounter = 0;
            foreach (T[] thisComb in combs)
            {
                nCounter++;
                for (int i = 0; i < thisComb.Length; i++)
                {
                    T nVal = thisComb[i];
#if VERBOSE
                    Console.Write("{0}, ", nVal);
#endif
                }

#if VERBOSE
                Console.WriteLine();
#endif
            }

            stopwatch.Stop();

#if !VERBOSE
            Console.Write(PROGRESS_CHAR);
#endif

            return new MeasureResult(nCounter, stopwatch.Elapsed);
        }
    }
}