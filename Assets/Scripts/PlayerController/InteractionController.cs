using Portfolio.Popups;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Zenject;
using Portfolio.Dialogue;

namespace Portfolio.PlayerController {
    public class InteractionController : MonoBehaviour {

        [Inject] private readonly PopupController _popupController;

        private bool _testPopupOpen = false;
        private DialoguePopupRequest _testPopupRequest;

        private DialogueAgent _currentDialogueAgent;


        private void OnEnable() {
            _testPopupRequest = new DialoguePopupRequest("");

            this.OnTriggerEnterAsObservable().Subscribe(other => CheckNewAgent(other));
            this.OnTriggerExitAsObservable().Subscribe(other => RemoveAgent(other));
        }

        public void OnInteraction() {
            if(_currentDialogueAgent == null) {
                return;
            }

            _testPopupRequest.Dialogue = _currentDialogueAgent.GetDialogue();

            if (_testPopupOpen) {
                _popupController.ClosePopup(_testPopupRequest);
                _testPopupOpen = false;
            } else {
                _popupController.RequestPopup(_testPopupRequest);
                _testPopupOpen = true;
            }
        }

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
    }
}
