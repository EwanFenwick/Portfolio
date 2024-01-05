using System;
using UnityEngine;
using Zenject;

namespace Portfolio.EventBusSystem {
    public abstract class PublisherComponent<T> : MonoBehaviour 
        where T : EventArgs {

        protected GlobalEventBus _eventBus;

        #region Lifecycle

        [Inject]
        public void Initialise(GlobalEventBus eventBus) {
            _eventBus = eventBus;
        }

        #endregion

        #region Protected Methods

        protected abstract void PublishEvent();

        #endregion
    }
}
