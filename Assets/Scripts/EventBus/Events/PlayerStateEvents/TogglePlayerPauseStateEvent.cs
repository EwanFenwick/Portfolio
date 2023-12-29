using System;

namespace Portfolio.EventBusSystem {
    //TODO: this should probably be split in half.
    //One event for pause/unpause requests and another event for when pauses and unpauses occur
    public class TogglePlayerPauseStateEvent : EventArgs {
        public PauseEventType PauseType { get; private set; }

        public TogglePlayerPauseStateEvent(PauseEventType pauseType) {
            PauseType = pauseType;
        }
    }
}
