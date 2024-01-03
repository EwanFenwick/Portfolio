using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public class AimStateChangedEvent : PlayerStateChangedEventArgs {
        public AimStateChangedEvent(PlayerStatePhase statePhase) : base(statePhase) { }
    }
}
