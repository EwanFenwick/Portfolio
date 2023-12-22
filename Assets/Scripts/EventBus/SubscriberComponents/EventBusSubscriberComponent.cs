using System;
using UnityEngine;
using Zenject;

namespace Portfolio.EventBusSystem {
    public abstract class EventBusSubscriberComponent<T> : MonoBehaviour where T : EventArgs {
        
        #region  Variables

        [Inject] private readonly EventBus _eventBus;

        #endregion

        #region Protected Methods

        protected abstract void OnEvent(object sender, EventArgs eventArgs);

        protected virtual void OnEnableInternal() { }
        protected virtual void OnDisableInternal() { }

        #endregion

        #region Private Methods

        private void OnEnable() {
            _eventBus.Subscribe<T>(OnEvent);
            OnEnableInternal();
        }

        private void OnDisable() {
            OnDisableInternal();
            _eventBus.Unsubscribe<T>(OnEvent);
        }

        #endregion
    }
}