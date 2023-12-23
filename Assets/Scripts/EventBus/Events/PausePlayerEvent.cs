using System;

namespace Portfolio.EventBusSystem {
    public class PausePlayerEvent : EventArgs {
        public PauseEventType PauseType { get; private set; }

        public PausePlayerEvent(PauseEventType pauseType) {
            PauseType = pauseType;
        }
    }
}
