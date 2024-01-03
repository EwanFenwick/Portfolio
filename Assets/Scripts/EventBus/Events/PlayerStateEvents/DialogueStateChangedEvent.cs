using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.EventBusSystem {
    public class DialogueStateChangedEvent : PlayerStateChangedEventArgs {
        public DialogueStateChangedEvent(PlayerStatePhase statePhase) : base(statePhase) { }
    }
}
