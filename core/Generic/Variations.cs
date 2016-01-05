namespace DioLive.Combinatorix.Core.Generic
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generates all the variations but NOT in lexicographical oreder
    /// </summary>
    public class Variations<T> : CombinatorixBase<T>
    {
        private bool isPermutateNow;

        private IEnumerator<int[]> permutationsEnumerator;
        private IEnumerator<int[]> combinationsEnumerator;

        public Variations(ICollection<T> items, int size)
            : base(items, size)
        {
        }

        protected override int[] Initialize(int size, int maxIndex)
        {
            int[] indices = Enumerable.Range(0, maxIndex + 1).ToArray();
            var combinations = Combinatorix.Combinations(indices, size);
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
            var permutations = Combinatorix.Permutations(combinationsEnumerator.Current);
            permutationsEnumerator = permutations.GetEnumerator();
        }
    }
}