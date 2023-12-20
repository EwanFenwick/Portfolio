using Portfolio.Popups;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Zenject;
using Portfolio.Dialogue;
using System;

namespace Portfolio.PlayerController {
    public class InteractionController : MonoBehaviour {

        #region Variables

        [Inject] private readonly PopupController _popupController;

        private bool _dialoguePopupOpen = false;
        private DialoguePopupRequest _testPopupRequest;

        private DialogueAgent _currentDialogueAgent;

        #endregion

        #region Properties

        public Action<bool> InteractionStateChanged { get; set; }
        public bool CanInteract => _currentDialogueAgent != null;

        #endregion

        #region Lifecycle

        private void OnEnable() {
            this.OnTriggerEnterAsObservable().Subscribe(other => CheckNewAgent(other));
            this.OnTriggerExitAsObservable().Subscribe(other => RemoveAgent(other));
        }

        #endregion

        #region Public Methods

        public void OnInteraction() {
            if(!CanInteract) {
                return;
            }

            if (_dialoguePopupOpen) {
                _popupController.ClosePopup(_testPopupRequest);
                _dialoguePopupOpen = false;
            } else {
                _testPopupRequest = new DialoguePopupRequest(
                    _currentDialogueAgent.AgentName, _currentDialogueAgent.GetDialogue());

                _popupController.RequestPopup(_testPopupRequest);
                _dialoguePopupOpen = true;
            }

            InteractionStateChanged?.Invoke(_dialoguePopupOpen);
        }

        #endregion

        #region Private Methods

        private void CheckNewAgent(Collider other) {
            if(other.TryGetComponent<DialogueAgent>(out var x)) {
                if (_currentDialogueAgent != null) {
                    _currentDialogueAgent.CanInteract = false;
                }
                _currentDialogueAgent = x;
                _currentDialogueAgent.CanInteract = true;
            }
        }

        private void RemoveAgent(Collider other) {
            if(_currentDialogueAgent != null && other.TryGetComponent<DialogueAgent>(out var x)) {
                _currentDialogueAgent.CanInteract = false;
                _currentDialogueAgent = null;
            }
        }

        #endregion
    }
}
