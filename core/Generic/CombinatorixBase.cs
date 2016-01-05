namespace DioLive.Combinatorix.Core.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class CombinatorixBase<T> : IEnumerable<T[]>
    {
        private readonly T[] items;
        private readonly int size;

        protected CombinatorixBase(ICollection<T> items, int size)
        {
            this.items = new T[items.Count];
            items.CopyTo(this.items, 0);

            this.size = size;
        }

        protected virtual int[] Initialize(int size, int maxIndex)
        {
            return Enumerable.Range(0, size).ToArray();
        }

        protected abstract bool NextIndices(int[] currentIndices, int maxIndex);

        private int[] Initialize() => Initialize(size, items.Length - 1);

        private bool NextIndices(int[] currentIndices) => NextIndices(currentIndices, items.Length - 1);

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
                currentItems = currentIndices.Select(i => combinatorixBase.items[i]).ToArray();
                invalidate = false;
            }
        }
    }
}