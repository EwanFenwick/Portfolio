using System;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public class PlayerStateChangedEventArgs : EventArgs {
        public PlayerStatePhase StatePhase { get; set; }

        public PlayerStateChangedEventArgs(PlayerStatePhase statePhase) {
            StatePhase = statePhase;
        }
    }
}
