using System;
using Portfolio.EventBusSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Portfolio.Quests {
    [Serializable]
    public class QuestStep : MonoBehaviour, IQuestStep {

        [Inject] private ISubscriberComponent _subscriberComponent;
        [Inject] private QuestInfoArgs _questArgs;

        public ReactiveProperty<bool> IsComplete { get; private set; }

        protected void CompleteQuestStep() {
            if(!IsComplete.Value) {
                IsComplete.Value = true;
                Destroy(this);
            }
        }

        public override string ToString()
        {
            return $"Step for Quest {_questArgs.QuestID}\nMust perform the action {_questArgs.Repetitions} times\nSubscriber Type:{_subscriberComponent.GetType()}";
        }
    }

    public interface IQuestStep {
        public ReactiveProperty<bool> IsComplete { get; }

        public virtual void Enable() { }

        public virtual void Disable() { }
    }
}
