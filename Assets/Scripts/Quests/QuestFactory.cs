using UnityEngine;
using Zenject;
using Portfolio.EventBusSystem;

namespace Portfolio.Quests {
    public class QuestFactory : PlaceholderFactory<QuestInfo, GameObject, Quest> { }

    public class CustomQuestFactory : IFactory<QuestInfo, GameObject, Quest> {
        private readonly DiContainer _container;
        private readonly GlobalEventBus _eventBus;

        public CustomQuestFactory(DiContainer container, GlobalEventBus eventBus) {
            _container = container;
            _eventBus = eventBus;
        }

        public Quest Create(QuestInfo questInfo, GameObject questStepParentObject) {
            return _container.Instantiate<Quest>(new object[] { questInfo, _eventBus, questStepParentObject });
        }
    }
}
