namespace Portfolio.EventBusSystem {
    public class AimStateChangedEvent : PlayerStateChangedEventArgs {
        public AimStateChangedEvent(PlayerStatePhase statePhase) : base(statePhase) { }
    }
}
