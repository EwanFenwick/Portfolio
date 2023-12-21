using Portfolio.Popups;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Zenject;
using Portfolio.Dialogue;
using System;
using Portfolio.EventBusSystem;

namespace Portfolio.PlayerController {
    public class InteractionController : MonoBehaviour {

        #region Variables

        [Inject] private readonly PopupController _popupController;
        [Inject] private readonly EventBus _eventBus;

        private bool _dialoguePopupOpen = false;
        private DialoguePopupRequest _testPopupRequest;

        private DialogueAgent _currentDialogueAgent;

        #endregion

        #region Properties

        public bool CanInteract => _currentDialogueAgent != null;

        #endregion

        #region Lifecycle

        private void OnEnable() {
            this.OnTriggerEnterAsObservable().Subscribe(other => CheckNewAgent(other));
            this.OnTriggerExitAsObservable().Subscribe(other => RemoveAgent(other));

            _eventBus.Subscribe<InteractionPerformedEvent>(OnInteractionPerformed);
        }

        private void OnDisable() {
            _eventBus.Unsubscribe<InteractionPerformedEvent>(OnInteractionPerformed);
        }

        #endregion

        #region Public Methods

        public void OnInteractionPerformed(object sender, EventArgs eventArgs) {
            if(!CanInteract) {
                return;
            }

            if (_dialoguePopupOpen) {
                _popupController.ClosePopup(_testPopupRequest);
                _dialoguePopupOpen = false;

                _eventBus.Publish(this, new PausePlayerEvent(false));
            }
            else {
                _testPopupRequest = new DialoguePopupRequest(
                    _currentDialogueAgent.AgentName, _currentDialogueAgent.GetDialogue());

                _popupController.RequestPopup(_testPopupRequest);
                _dialoguePopupOpen = true;

                _eventBus.Publish(this, new PausePlayerEvent(true));
            }
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
