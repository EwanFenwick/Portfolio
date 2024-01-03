using UnityEngine;
using Zenject;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public class SubscriberComponentFactory : PlaceholderFactory<SubscriberComponentType, GameObject, ISubscriberComponent> { }

    public class CustomSubscriberComponentFactory : IFactory<SubscriberComponentType, GameObject, ISubscriberComponent> {
        private readonly DiContainer _container;
        private readonly GlobalEventBus _eventBus;

        public CustomSubscriberComponentFactory(DiContainer container, GlobalEventBus eventBus) {
            _container = container;
            _eventBus = eventBus;
        }

        public ISubscriberComponent Create(SubscriberComponentType type, GameObject parent)
            => (ISubscriberComponent)_container.InstantiateComponent(type.ToComponent(), parent, new object[] { _eventBus });
    }
}
