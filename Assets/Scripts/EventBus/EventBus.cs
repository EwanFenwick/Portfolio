using System;
using System.Collections.Generic;

namespace Portfolio.EventBusSystem {
    public class EventBus {

        #region Variables

        private Dictionary<Type, List<EventHandler>> _subscribersByType;

        #endregion

        public EventBus() {
            _subscribersByType = new Dictionary<Type, List<EventHandler>>();
        }

        #region Public Methods

        public void Subscribe<T>(EventHandler eventHandler) where T : EventArgs {
            var type = typeof(T);
            if(!_subscribersByType.TryGetValue(type, out var subscribers)) {
                _subscribersByType.Add(type, new List<EventHandler>());
            }
            _subscribersByType[type].Add(eventHandler);
        }

        public void Unsubscribe<T>(EventHandler eventHandler) where T : EventArgs {
            var type = typeof(T);
            if (_subscribersByType.TryGetValue(type, out var subscribers)) {
                _subscribersByType[type].Remove(eventHandler);
            }
        }

        public void Publish(object sender, EventArgs eventArgs) {
            if (_subscribersByType.TryGetValue(eventArgs.GetType(), out var subscribers)) {
                for (int i = 0, iLength = subscribers.Count; i < iLength; i++) {
                    subscribers[i](sender, eventArgs);
                }
            }
        }

        #endregion
    }
}
