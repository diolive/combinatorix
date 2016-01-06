namespace DioLive.Combinatorix.Core.Lite
{
    public static class CombinatorixLite
    {
        public static CombinationsLite Combinations(int itemCount, int size)
        {
            return new CombinationsLite(itemCount, size);
        }

        public static PermutationsLite Permutations(int itemCount)
        {
            return new PermutationsLite(itemCount);
        }

        public static VariationsLite Variations(int itemCount, int size)
        {
            return new VariationsLite(itemCount, size);
        }
    }
}