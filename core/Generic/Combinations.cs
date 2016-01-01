namespace DioLive.Combinatorix.Core.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Generates cobination without dublications in lexicographical order
    /// </summary>
    public class Combinations<T> : CombinatorixBase<T>
    {
        public Combinations(ICollection<T> items, int count)
            : base(items, count)
        {
        }

        protected override bool NextIndices(int[] currentIndices, int maxIndex)
        {
            int index = currentIndices.Length - 1;
            for (int i = 0; i < currentIndices.Length; i++)
            {
                if (currentIndices[index] < maxIndex - i)
                {
                    break;
                }

                index--;
            }

            if (index == -1)
            {
                return false;
            }

            currentIndices[index]++;
            for (int i = index + 1; i < currentIndices.Length; i++)
            {
                currentIndices[i] = currentIndices[i - 1] + 1;
            }

            return true;
        }
    }
}