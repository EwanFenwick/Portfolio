using NaughtyAttributes;

namespace Portfolio.EventBusSystem {
    public class TriggerEnteredSubscriberComponent : SubscriberComponent<TriggerEnteredEvent> {

        protected override void OnEnable() {
            if(_eventBus == null) {
                return;
            }
            _eventBus.Triggers.Subscribe<TriggerEnteredEvent>(OnEvent);
        }

        protected override void OnDisable() {
            if(_eventBus == null) {
                return;
            }
            _eventBus.Triggers.Unsubscribe<TriggerEnteredEvent>(OnEvent);
        }

        [Button]
        private void DEBUG_SendStepOneEvent() {
            base.OnEvent(this, new TriggerEnteredEvent("TestQuest_One"));
        }

        [Button]
        private void DEBUG_SendStepTwoEvent() {
            base.OnEvent(this, new TriggerEnteredEvent("TestQuest_Two"));
        }
    }
}
