using Portfolio.Popups;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Zenject;
using Portfolio.Dialogue;
using System;
using Portfolio.EventBusSystem;
using Ink.Runtime;

namespace Portfolio.PlayerController {
    public class DialogueInteractionController : MonoBehaviour {

        #region Variables

        [Inject] private readonly PopupController _popupController;
        [Inject] private readonly EventBus _eventBus;

        private DialoguePopupRequest _dialoguePopupRequest;
        private DialogueAgent _dialogueAgent;
        private Story _currentStory;

        #endregion

        #region Properties

        public bool CanInteract => _dialogueAgent != null;

        #endregion

        #region Lifecycle

        private void OnEnable() {
            this.OnTriggerEnterAsObservable().Subscribe(other => CheckNewAgent(other)).AddTo(this);
            this.OnTriggerExitAsObservable().Subscribe(other => RemoveAgent(other)).AddTo(this);

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

            if(_currentStory == null) {
                StartNewDialogue();
                return;
            }

            if(_currentStory.canContinue) {
                ContinueCurrentDialogue();
            } else {
                CloseCurrentDialogue();
            }

            void StartNewDialogue() {
                _currentStory = new Story(_dialogueAgent.Dialogue.text);

                _dialoguePopupRequest = new DialoguePopupRequest(_dialogueAgent.AgentName, _currentStory);
                _popupController.RequestPopup(_dialoguePopupRequest);
            }

            void ContinueCurrentDialogue() => _eventBus.Publish(this, new DialogueContinuedEvent(_currentStory.Continue()));

            void CloseCurrentDialogue() {
                _popupController.ClosePopup(_dialoguePopupRequest);
                _currentStory = null;
            }
        }

        #endregion

        #region Private Methods

        private void CheckNewAgent(Collider other) {
            if(other.TryGetComponent<DialogueAgent>(out var agent)) {
                if (_dialogueAgent != null) {
                    _dialogueAgent.DisableDialogue();
                }
                _dialogueAgent = agent;
                _dialogueAgent.EnableDialogue(transform);
            }
        }

        private void RemoveAgent(Collider other) {
            if(_dialogueAgent != null && other.TryGetComponent<DialogueAgent>(out var x)) {
                _dialogueAgent.DisableDialogue();
                _dialogueAgent = null;
            }
        }

        #endregion
    }
}
