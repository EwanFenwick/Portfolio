using UnityEngine;

namespace Portfolio.Dialogue {
    public class DialogueAgent : MonoBehaviour {
        public bool CanInteract { get; set; } //TODO: use for dialogue highlight/indicator later

        public string GetDialogue() => "Test Dialogue";
    }
}
