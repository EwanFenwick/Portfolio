using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using Portfolio.EventBusSystem;
using System;

namespace Portfolio.Popups {
    public class DialoguePopupView : Popup {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private ScrollRect _dialogueScollView;
        [SerializeField] private DialogueChoiceView _dialogueChoicePrefab;
        [SerializeField] private GameObject _dialogueChoiceIndicator;

#pragma warning restore 0649
        #endregion

        private Story _currentStory;

        protected override void OnPopupOpen(PopupRequest request) {
            var dialogueRequest = GetPopupRequest<DialoguePopupRequest>();

            _speakerText.text = dialogueRequest.Speaker;
            _currentStory = dialogueRequest.Story;

            StartStory();

            _dialogueScollView.verticalNormalizedPosition = 1f;

            _eventBus.Subscribe<DialogueContinuedEvent>(ContinueStory);

            void StartStory() {
                _dialogueText.text = _currentStory.Continue();
            }
        }

        protected override void OnPopupClosed() {
            _eventBus.Unsubscribe<DialogueContinuedEvent>(ContinueStory);
            base.OnPopupClosed();
        }

        private void ContinueStory(object sender, EventArgs eventArgs) => _dialogueText.text = ((DialogueContinuedEvent)eventArgs).NextDialogueLine;
    }
}
