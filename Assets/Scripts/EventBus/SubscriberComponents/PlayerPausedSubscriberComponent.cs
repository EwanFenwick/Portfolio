using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.EventBusSystem {
    public class PlayerPausedSubscriberComponent : EventBusSubscriberComponent<PausePlayerEvent> {
        [SerializeField] private bool _invertBool;
        [SerializeField] private UnityEvent<bool> _uniEvent;

        protected override void OnEvent(object sender, EventArgs eventArgs) {
            var pause = ((PausePlayerEvent)eventArgs).IsPaused;
            _uniEvent?.Invoke(_invertBool ? !pause : pause);
        }
    }

    
}