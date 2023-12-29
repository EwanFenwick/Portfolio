using System;

namespace Portfolio.EventBusSystem {
    public class DialogueContinuedEvent : EventArgs {
        public string NextDialogueLine { get; private set; }

        public DialogueContinuedEvent(string nextDialogueLine) {
            NextDialogueLine = nextDialogueLine;
        }
    }
}
