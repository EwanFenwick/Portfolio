using System;
using Portfolio.EventBusSystem;
using UnityEngine;
using Zenject;
using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    public class QuestStepFactory : PlaceholderFactory<QuestStepInfo, GameObject, QuestStep> { }

    public class CustomQuestStepFactory : IFactory<QuestStepInfo, GameObject, QuestStep> {

        private readonly DiContainer _container;
        private readonly SubscriberComponentFactory _componentFactory;

        private GameObject _parent;

        public CustomQuestStepFactory(DiContainer container, SubscriberComponentFactory componentFactory) {
            _container = container;
            _componentFactory = componentFactory;
        }

        public QuestStep Create(QuestStepInfo args, GameObject parent) {
            if(_parent == null){
                _parent = parent;
            }

            var subscriber = args.QuestStepType switch {
                QuestStepType.Trigger
                    => _componentFactory.Create(args.QuestStepType.ToSubscriberComponentType(), parent),
                
                _ => throw new Exception($"Invalid Quest Step Type: {args.QuestStepType}"),
            };

            return _container.InstantiateComponent<QuestStep>(parent, new object[] { subscriber, args });
        }
    }
}
