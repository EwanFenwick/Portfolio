using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.EventBusSystem {
    public class PlayerPausedSubscriberComponent : EventBusSubscriberComponent<PausePlayerEvent> {
        [SerializeField] private UnityEvent<bool> _uniEvent;

        protected override void OnEvent(object sender, EventArgs eventArgs) {
            _uniEvent?.Invoke(!((PausePlayerEvent)eventArgs).IsPaused);
        }
    }

    
}