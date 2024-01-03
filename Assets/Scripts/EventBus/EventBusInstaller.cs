using UnityEngine;
using Zenject;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public class EventBusInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<GlobalEventBus>().FromNew().AsSingle().NonLazy();

            Container.BindFactory<SubscriberComponentType, GameObject, ISubscriberComponent, SubscriberComponentFactory>().FromFactory<CustomSubscriberComponentFactory>().NonLazy();
        }
    }
}