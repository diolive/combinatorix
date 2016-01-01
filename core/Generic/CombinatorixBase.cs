namespace DioLive.Combinatorix.Core.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class CombinatorixBase<T> : IEnumerable<T[]>
    {
        private readonly int count;
        private readonly T[] innerArray;

        protected CombinatorixBase(ICollection<T> items, int count)
        {
            innerArray = new T[items.Count];
            items.CopyTo(innerArray, 0);

            this.count = count;
        }

        protected virtual int[] Initialize(int count, int maxIndex)
        {
            return Enumerable.Range(0, count).ToArray();
        }

        protected abstract bool NextIndices(int[] currentIndices, int maxIndex);

        private int[] Initialize() => Initialize(count, innerArray.Length - 1);

        private bool NextIndices(int[] currentIndices) => NextIndices(currentIndices, innerArray.Length - 1);

        #region IEnumerable`1 implementation

        public IEnumerator<T[]> GetEnumerator()
        {
            return new CombinatorixEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion IEnumerable`1 implementation

        public class CombinatorixEnumerator : IEnumerator<T[]>
        {
            private CombinatorixBase<T> combinatorixBase;
            private int[] currentIndices;
            private T[] currentItems;
            private bool initialized;
            private bool invalidate = true;

            public CombinatorixEnumerator(CombinatorixBase<T> combinatorixBase)
            {
                this.combinatorixBase = combinatorixBase;
            }

            public T[] Current
            {
                get
                {
                    if (invalidate)
                    {
                        LoadCurrentItems();
                    }
                    return currentItems;
                }
            }

            object IEnumerator.Current => Current;

            void IDisposable.Dispose()
            {
            }

            public bool MoveNext()
            {
                invalidate = true;
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
                currentItems = null;
                invalidate = true;
                initialized = false;
            }

            private void LoadCurrentItems()
            {
                currentItems = currentIndices.Select(i => combinatorixBase.innerArray[i]).ToArray();
                invalidate = false;
            }
        }
    }
}