using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using Portfolio.AssetLibraries;
using MyGameDevTools.SceneLoading;
using MyGameDevTools.SceneLoading.UniTaskSupport;
using static Portfolio.SceneManagement.SceneManagementEnums;

namespace Portfolio.SceneManagement {
	public class PersistentSceneBootstrapper : MonoBehaviour {

		[Inject] private readonly SceneIndexLibrary _sceneIndexLibrary;

		private void Start() {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			LoadGameScene().Forget();
		}

		private async UniTaskVoid LoadGameScene() {
			//load new scene
			var sceneManager = new SceneManager();
			var sceneLoader = new SceneLoaderUniTask(sceneManager);

			var gameSceneInfo = _sceneIndexLibrary.GetSceneInfoIndex(SceneType.Game);
			var loadingSceneInfo = _sceneIndexLibrary.GetSceneInfoIndex(SceneType.Loading);

			await sceneLoader.TransitionToSceneAsync(gameSceneInfo, loadingSceneInfo);
		}
	}
}
