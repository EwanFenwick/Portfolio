using Portfolio.Utilities;
using UnityEngine;

namespace Portfolio.Dialogue {
    public class DialogueAgent : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private string _agentName;
        [SerializeField] private TextAsset _inkDialogueJSON;
        [SerializeField] private LookAtTarget _dialogueIndicator;

#pragma warning restore 0649
        #endregion

        private bool _canInteract;

        #region Properties

        public string AgentName => _agentName;
        public TextAsset Dialogue => _inkDialogueJSON;

        #endregion

        #region Public Methods

        public void SetDialogueState(bool canInteract, Transform target) {
            _dialogueIndicator.SetTarget(target);
            _dialogueIndicator.gameObject.SetActive(canInteract);
            _canInteract = canInteract;
        }

        #endregion
    }
}
