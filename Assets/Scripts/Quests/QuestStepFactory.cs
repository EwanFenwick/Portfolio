using System;
using Portfolio.EventBusSystem;
using UnityEngine;
using Zenject;
using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    public class QuestStepFactory : PlaceholderFactory<QuestStepType, QuestInfoArgs, GameObject, QuestStep> { }

    public class CustomQuestStepFactory : IFactory<QuestStepType, QuestInfoArgs, GameObject, QuestStep> {

        private readonly GlobalEventBus _eventBus;
        private readonly DiContainer _container;

        public CustomQuestStepFactory(DiContainer container, GlobalEventBus eventBus) {
            _container = container;
            _eventBus = eventBus;
            Debug.Log($"factory bus enabled: {_eventBus != null}");
        }

        public QuestStep Create(QuestStepType type, QuestInfoArgs args, GameObject parent) {
            var subscriber = type switch {
                //TODO: this injection isn't working
                QuestStepType.Trigger
                    => _container.InstantiateComponent<TriggerEnteredSubscriberComponent>(parent),
                
                _ => throw new Exception($"Invalid Quest Step Type: {type}"),
            };

            //_container.InjectGameObject(parent);

            return _container.InstantiateComponent<QuestStep>(parent, new object[] { subscriber, args });
        }
    }
}
