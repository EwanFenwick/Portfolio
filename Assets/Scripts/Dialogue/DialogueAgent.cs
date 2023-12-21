using UnityEngine;

namespace Portfolio.Dialogue {
    public class DialogueAgent : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private string _agentName;
        [SerializeField] private GameObject _dialogueIndicator;

#pragma warning restore 0649
        #endregion

        #region Properties

        public bool CanInteract { get; set; } //TODO: use for dialogue highlight/indicator later
        public string AgentName => _agentName;

        #endregion

        #region Public Methods

        public string GetDialogue() => "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac nisl massa. Pellentesque id magna odio. Nulla nec leo dui.\n\nMorbi nisl odio, pulvinar et quam scelerisque, sagittis viverra augue. Aliquam convallis tempus cursus.\n\nSuspendisse ullamcorper rutrum purus ac tincidunt.\nInteger in justo ut mauris gravida fringilla. \nPhasellus sodales, enim non molestie volutpat, ligula lacus laoreet ligula, nec eleifend odio velit quis justo.\n\nSed dictum erat non molestie efficitur.";

        #endregion
    }
}
