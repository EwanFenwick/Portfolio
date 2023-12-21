using System;

namespace Portfolio.EventBusSystem {
    public class PausePlayerEvent : EventArgs {
        public bool IsPaused { get; private set; }

        public PausePlayerEvent(bool isPaused) {
            IsPaused = isPaused;
        }
    }
}
