using System;
using UnityEngine;

namespace Portfolio.Utilities {
	public abstract class SerialisableDictionary<TKey, TValue>
			where TKey : IComparable {

		#region Editor Variables
#pragma warning disable 0649

		[SerializeField] protected TKey _key;

		[SerializeField] protected TValue _value;

#pragma warning restore 0649
		#endregion

		#region Properties

		public TKey Key => _key;

		public TValue Value => _value;

		#endregion
	}
}
