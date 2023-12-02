using System;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.Utilities;
using static Portfolio.SceneManagement.SceneManagementEnums;

namespace Portfolio.AssetCollections {
	[CreateAssetMenu(fileName = "SceneIndexLibrary", menuName = "ScriptableObjects/SceneIndexLibraryScriptableObject", order = 0)]
	public class SceneIndexLibrary : ScriptableObject {

		[SerializeField] private List<SceneIndex> _assets;

		public int GetSceneIndex(SceneType sceneType) {
			return _assets.Find(i => i.Key == sceneType).Value;
		}

		#region Nested Classes

		[Serializable]
		public class SceneIndex : SerialisableDictionary<SceneType, int> { }

		#endregion
	}
}
