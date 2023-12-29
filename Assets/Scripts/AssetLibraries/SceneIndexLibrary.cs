using System.Collections.Generic;
using UnityEngine;
using Portfolio.Utilities;
using static Portfolio.SceneManagement.SceneManagementEnums;

namespace Portfolio.AssetLibraries {
	[CreateAssetMenu(fileName = "SceneIndexLibrary", menuName = "Portfolio/AssetLibraries/New SceneIndexLibrary", order = 0)]
	public class SceneIndexLibrary : ScriptableObject {
		
		#region Editor Variables
#pragma warning disable 0649

		[SerializeField] private List<SerialisableSceneDictionary> _assets;

#pragma warning restore 0649
        #endregion

        #region Public Methods

        public int GetSceneIndex(SceneType sceneType)
			=> _assets.Find(x => x.Key == sceneType).Value;

        public MyGameDevTools.SceneLoading.LoadSceneInfoIndex GetSceneInfoIndex(SceneType sceneType)
			=> new(GetSceneIndex(sceneType));

        #endregion
	}
}
