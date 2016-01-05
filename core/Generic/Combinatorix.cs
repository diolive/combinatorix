namespace DioLive.Combinatorix.Core.Generic
{
    using System.Collections.Generic;

    public static class Combinatorix
    {
        public static Combinations<T> Combinations<T>(ICollection<T> items, int size)
        {
            return new Combinations<T>(items, size);
        }

        public static Permutations<T> Permutations<T>(ICollection<T> items)
        {
            return new Permutations<T>(items);
        }

        public static Variations<T> Variations<T>(ICollection<T> items, int size)
        {
            return new Variations<T>(items, size);
        }
    }
}