namespace DioLive.Combinatorix.Core.Generic
{
    using System.Collections.Generic;

    public static class Combinatorix
    {
        public static Combinations<T> Combinations<T>(ICollection<T> items, int count)
        {
            return new Combinations<T>(items, count);
        }

        public static Permutations<T> Permutations<T>(ICollection<T> items)
        {
            return new Permutations<T>(items);
        }

        public static Variations<T> Variations<T>(ICollection<T> items, int count)
        {
            return new Variations<T>(items, count);
        }
    }
}