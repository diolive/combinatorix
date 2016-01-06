namespace DioLive.Combinatorix.Core.Lite
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class CombinatorixLiteBase : IEnumerable<int[]>
    {
        private readonly int itemCount;
        private readonly int size;

        protected CombinatorixLiteBase(int itemCount, int size)
        {
            this.itemCount = itemCount;
            this.size = size;
        }

        protected virtual int[] Initialize(int count, int maxIndex)
        {
            return Enumerable.Range(0, count).ToArray();
        }

        protected abstract bool NextIndices(int[] currentIndices, int maxIndex);

        private int[] Initialize() => Initialize(size, itemCount - 1);

        private bool NextIndices(int[] currentIndices) => NextIndices(currentIndices, itemCount - 1);

        #region IEnumerable`1 implementation

        public IEnumerator<int[]> GetEnumerator()
        {
            return new CombinatorixEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion IEnumerable`1 implementation

        public class CombinatorixEnumerator : IEnumerator<int[]>
        {
            private CombinatorixLiteBase combinatorixBase;
            private int[] currentIndices;
            private bool initialized;

            public CombinatorixEnumerator(CombinatorixLiteBase combinatorixBase)
            {
                this.combinatorixBase = combinatorixBase;
            }

            public int[] Current => currentIndices;

            object IEnumerator.Current => Current;

            void IDisposable.Dispose()
            {
            }

            public bool MoveNext()
            {
                if (!initialized)
                {
                    currentIndices = combinatorixBase.Initialize();
                    initialized = true;
                    return true;
                }
                else
                {
                    return combinatorixBase.NextIndices(currentIndices);
                }
            }

            public void Reset()
            {
                currentIndices = null;
                initialized = false;
            }
        }
    }
}