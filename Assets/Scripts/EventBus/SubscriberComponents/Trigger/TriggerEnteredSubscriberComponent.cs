using System;
using UnityEngine;

namespace Portfolio.EventBusSystem {
    public class TriggerEnteredSubscriberComponent : SubscriberComponent<TriggerEnteredEvent> {
    
        protected override void OnEnable()
        {
            Debug.Log($"trigger event enabled: {_eventBus != null}");
            _eventBus.Triggers.Subscribe<TriggerEnteredEvent>(OnEvent);
        }

        protected override void OnDisable()
        {
            _eventBus.Triggers.Unsubscribe<TriggerEnteredEvent>(OnEvent);
        }

        protected override void OnEvent(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
