using Cysharp.Threading.Tasks;
using MyGameDevTools.SceneLoading;
using Portfolio.AssetCollections;
using UnityEngine;
using Zenject;
using static Portfolio.SceneManagement.SceneManagementEnums;

namespace Portfolio.SceneManagement {
	public class PersistentSceneBootstrapper : MonoBehaviour {

		[Inject] private readonly SceneIndexLibrary _sceneAssetCollection;

		private async void Start() {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			await LoadGameScene();
		}

		private async UniTask LoadGameScene() {
			//load new scene
			ISceneManager sceneManager = new SceneManager();
			ISceneLoaderAsync sceneLoader = new SceneLoaderAsync(sceneManager);

			var gameSceneInfo = new LoadSceneInfoIndex(_sceneAssetCollection.GetSceneIndex(SceneType.Game));
			var loadingSceneInfo = new LoadSceneInfoIndex(_sceneAssetCollection.GetSceneIndex(SceneType.Loading));

			await sceneLoader.TransitionToSceneAsync(gameSceneInfo, loadingSceneInfo);
		}
	}
}
