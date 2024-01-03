using System;

namespace Portfolio.EventBusSystem {
    public class TriggerEnteredEvent : EventArgs {
        public string TriggerID { get; set; }

        public TriggerEnteredEvent(string triggerID) {
            TriggerID = triggerID;
        }
    }
}