using System;

namespace Portfolio.EventBusSystem {
    public class PlayerStateChangedEventArgs : EventArgs {
        public PlayerStatePhase StatePhase { get; set; }

        public PlayerStateChangedEventArgs(PlayerStatePhase statePhase) {
            StatePhase = statePhase;
        }
    }
}
