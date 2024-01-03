using System;
using System.Collections.Generic;

namespace Portfolio.EventBusSystem {
    public class GlobalEventBus {

        public EventBus Input { get; private set; }
        public EventBus PlayerState { get; private set; }
        public EventBus Popups { get; private set; }
        public EventBus Quest { get; private set; }
        public EventBus Triggers { get; private set; }
        public EventBus Misc { get; private set; }

        public GlobalEventBus() {
            Input = new EventBus();
            PlayerState = new EventBus();
            Popups = new EventBus();
            Quest = new EventBus();
            Triggers = new EventBus();
            Misc = new EventBus();
        }
    }

    public class EventBus {
        #region Variables

        private readonly Dictionary<Type, List<EventHandler>> _subscribersByType;

        #endregion

        public EventBus() {
            _subscribersByType = new Dictionary<Type, List<EventHandler>>();
        }

        #region Public Methods

        public void Subscribe<T>(EventHandler eventHandler) where T : EventArgs {
            var type = typeof(T);
            if (!_subscribersByType.TryGetValue(type, out var _)) {
                _subscribersByType.Add(type, new List<EventHandler>());
            }
            _subscribersByType[type].Add(eventHandler);
        }

        public void Unsubscribe<T>(EventHandler eventHandler) where T : EventArgs {
            var type = typeof(T);
            if (_subscribersByType.TryGetValue(type, out var _)) {
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
