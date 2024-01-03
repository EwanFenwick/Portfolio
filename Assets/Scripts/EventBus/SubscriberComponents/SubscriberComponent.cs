using System;
using UnityEngine;
using Zenject;

namespace Portfolio.EventBusSystem {
    public abstract class SubscriberComponent<T> : MonoBehaviour, ISubscriberComponent
        where T : EventArgs {
        #region  Variables

        protected GlobalEventBus _eventBus;

        #endregion

        #region Properties

        public event Action<EventArgs> OnEventPerformed;

        #endregion

        #region Lifecycle

        [Inject]
        public void Initialise(GlobalEventBus eventBus) {
            _eventBus = eventBus;
            OnEnable();
        }

        public void Destroy() {
            Destroy(this);
        }

        protected abstract void OnEnable();

        protected abstract void OnDisable();

        #endregion

        #region Protected Methods

        protected virtual void OnEvent(object sender, EventArgs eventArgs) {
            OnEventPerformed?.Invoke(eventArgs);
        }

        #endregion
    }

    public interface ISubscriberComponent {
        public event Action<EventArgs> OnEventPerformed;

        public void Initialise(GlobalEventBus eventBus);
        public void Destroy();
    }
}