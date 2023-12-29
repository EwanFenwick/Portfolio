using System;

namespace Portfolio.Utilities {
    public interface ISerialisableDictionary<TKey, TValue>
        where TKey : IComparable {

        #region Properties

        public TKey Key { get; }

        public TValue Value { get; }

        #endregion
    }
}