using UnityEngine;
using Zenject;

namespace Portfolio.Quests {
    public class QuestFactory : PlaceholderFactory<QuestInfo, GameObject, Quest> { }

    public class CustomQuestFactory : IFactory<QuestInfo, GameObject, Quest> {
        private readonly DiContainer _container;

        public CustomQuestFactory(DiContainer container) {
            _container = container;
        }

        public Quest Create(QuestInfo questInfo, GameObject questStepParentObject) {
            return _container.Instantiate<Quest>(new object[] { questInfo, questStepParentObject });
        }
    }
}
