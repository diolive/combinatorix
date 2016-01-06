namespace DioLive.Combinatorix.Core.Lite
{
    using System.Collections.Generic;

    /// <summary>
    /// Generates all the variations but NOT in lexicographical oreder
    /// </summary>
    public class VariationsLite : CombinatorixLiteBase
    {
        private bool isPermutateNow;

        private IEnumerator<int[]> permutationsEnumerator;
        private IEnumerator<int[]> combinationsEnumerator;

        public VariationsLite(int itemCount, int size)
            : base(itemCount, size)
        {
        }

        protected override int[] Initialize(int size, int maxIndex)
        {
            var combinations = CombinatorixLite.Combinations(maxIndex + 1, size);
            combinationsEnumerator = combinations.GetEnumerator();

            return base.Initialize(size, maxIndex);
        }

        protected override bool NextIndices(int[] currentIndices, int maxIndex)
        {
            if (isPermutateNow)
            {
                if (permutationsEnumerator.MoveNext())
                {
                    permutationsEnumerator.Current.CopyTo(currentIndices, 0);
                    return true;
                }

                isPermutateNow = false;
            }

            if (!combinationsEnumerator.MoveNext())
            {
                return false;
            }

            isPermutateNow = true;
            ReinitializePermutations();

            combinationsEnumerator.Current.CopyTo(currentIndices, 0);

            return true;
        }

        private void ReinitializePermutations()
        {
            var permutations = Generic.Combinatorix.Permutations(combinationsEnumerator.Current);
            permutationsEnumerator = permutations.GetEnumerator();
        }
    }
}