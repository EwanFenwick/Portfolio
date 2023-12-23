using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.EventBusSystem {
    public abstract class PlayerStateChangedSubscriberComponent<T> : EventBusSubscriberComponent<T> where T : PlayerStateChangedEventArgs {
        [SerializeField] private UnityEvent _OnEnteredEvent;
        [SerializeField] private UnityEvent _OnExitedEvent;

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
