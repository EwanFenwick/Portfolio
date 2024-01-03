using System;
using UnityEngine;
using UnityEngine.Events;

using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public abstract class PlayerStateChangedSubscriberComponent<T> : SubscriberComponent<T> where T : PlayerStateChangedEventArgs {
        [SerializeField] private UnityEvent _OnEnteredEvent;
        [SerializeField] private UnityEvent _OnExitedEvent;

        protected override void OnEnable() {
            if(_eventBus == null) {
                return;
            }
            _eventBus.PlayerState.Subscribe<T>(OnEvent);
        }

        protected override void OnDisable() {
            if(_eventBus == null) {
                return;
            }
            _eventBus.PlayerState.Unsubscribe<T>(OnEvent);
        }

        protected override void OnEvent(object sender, EventArgs eventArgs) {
            switch(((PlayerStateChangedEventArgs)eventArgs).StatePhase) {
                case PlayerStatePhase.Entered:
                    _OnEnteredEvent?.Invoke();
                    break;

                case PlayerStatePhase.Exited:
                    _OnExitedEvent?.Invoke();
                    break;

                default:
                    Debug.LogError("Unexpected PlayerStatePhase encountered");
                    break;
            }
        }
    }
}
