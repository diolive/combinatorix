namespace DioLive.Combinatorix.Core.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Generates all the permutations in lexicographical order
    /// </summary>
    public class Permutations<T> : CombinatorixBase<T>
    {
        public Permutations(ICollection<T> items)
            : base(items, items.Count)
        {
        }

        protected override bool NextIndices(int[] currentIndices, int maxIndex)
        {
            int lastIndex = currentIndices.Length - 1;

            // Find the first item so that a[i] < a[i+1];
            int index = lastIndex - 1;
            while (index >= 0 && currentIndices[index] >= currentIndices[index + 1])
            {
                index--;
            }

            // No more permutations to be generated.
            if (index == -1)
            {
                return false;
            }

            // Find the smallest a[j] , so that j > i & a[j] > a[i]
            int smallestIndex = lastIndex;
            while (smallestIndex > index && currentIndices[smallestIndex] <= currentIndices[index])
            {
                smallestIndex--;
            }

            // Exchange the a[i] and the last item (a[n]).
            Swap(ref currentIndices[index], ref currentIndices[smallestIndex]);

            // Reverse the items from a[i+1] till a[n];
            int first = index + 1;
            int second = lastIndex;

            while (first < second)
            {
                Swap(ref currentIndices[first++], ref currentIndices[second--]);
            }

            return true;
        }

        private static void Swap(ref int first, ref int second)
        {
            first ^= second;
            second = first ^ second;
            first ^= second;
        }
    }
}