namespace DioLive.Combinatorix.Demo
{
    using System;

    internal struct MeasureResult
    {
        public int Counter { get; }

        public TimeSpan Time { get; }

        public MeasureResult(int counter, TimeSpan time)
        {
            Counter = counter;
            Time = time;
        }
    }
}