using System;
using UnityEngine;
using Zenject;

namespace Portfolio.EventBusSystem {
    public abstract class SubscriberComponent<T> : MonoBehaviour, ISubscriberComponent
        where T : EventArgs {
        #region  Variables

        [Inject] protected GlobalEventBus _eventBus;

        #endregion

        #region Lifecycle

        protected abstract void OnEnable();

        protected abstract void OnDisable();
        
        #endregion

        #region Protected Methods

        protected abstract void OnEvent(object sender, EventArgs eventArgs);

        #endregion
    }

    //Marker interface to allow use without type <T>
    public interface ISubscriberComponent { }
}