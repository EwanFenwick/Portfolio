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

        #endregion

        #region Private Methods

        private void OnEnable() {
            _eventBus.Subscribe<T>(OnEvent);
        }

        private void OnDisable() {
            _eventBus.Unsubscribe<T>(OnEvent);
        }

        #endregion
    }
}