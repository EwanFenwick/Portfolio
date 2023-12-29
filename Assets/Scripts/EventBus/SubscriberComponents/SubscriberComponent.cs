using System;
using UnityEngine;
using Zenject;

namespace Portfolio.EventBusSystem {
    public abstract class SubscriberComponent<T> : MonoBehaviour where T : EventArgs {
        #region  Variables

        [Inject] protected readonly GlobalEventBus _eventBus;

        #endregion

        #region Lifecycle

        protected abstract void OnEnable();

        protected abstract void OnDisable();
        
        #endregion

        #region Protected Methods

        protected abstract void OnEvent(object sender, EventArgs eventArgs);

        #endregion
    }
}