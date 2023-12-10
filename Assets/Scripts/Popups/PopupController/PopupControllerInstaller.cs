using UnityEngine;
using Zenject;

namespace Portfolio.Popups {
    public class PopupControllerInstaller : MonoInstaller {
        [SerializeField] private GameObject HUD;

        public override void InstallBindings() {
            Container.Bind<PopupControllerView>().FromComponentInNewPrefab(HUD).AsSingle().NonLazy();
            Container.Bind<PopupController>().FromNew().AsSingle().NonLazy();
        }
    }
}
