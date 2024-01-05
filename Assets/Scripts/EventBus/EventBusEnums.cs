using System;

namespace Portfolio.EventBusSystem {
    public static class EventBusEnums {
        #region Enums
    
        public enum PauseEventType {
            None,
            Unpause,
            Dialogue,
        }

        public enum PlayerStatePhase {
            Entered,
            Exited,
        }

        public enum SubscriberComponentType {
            Aim,
            Dialogue,
            TriggerEntered,
        }

        #endregion

        #region Enum Extensions

        public static Type ToComponent(this SubscriberComponentType type) => type switch {
            SubscriberComponentType.Aim => typeof(AimStateChangedSubscriberComponent),
            SubscriberComponentType.Dialogue => typeof(DialogueStateChangedSubscriberComponent),
            SubscriberComponentType.TriggerEntered => typeof(TriggerEnteredSubscriberComponent),
            _ => null,
        };

        #endregion
    }
}