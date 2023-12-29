using Zenject;

namespace Portfolio.EventBusSystem {
    public class EventBusInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<GlobalEventBus>().FromNew().AsSingle().NonLazy();
        }
    }
}