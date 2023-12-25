using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using Portfolio.EventBusSystem;
using System;
using System.Collections.Generic;
using UniRx;

namespace Portfolio.Popups {
    public class DialoguePopupView : Popup {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private ScrollRect _dialogueScollView;
        [SerializeField] private RectTransform _dialogueChoicesParent;
        [SerializeField] private DialogueChoiceView _dialogueChoicePrefab;
        [SerializeField] private GameObject _dialogueChoiceIndicator;

#pragma warning restore 0649
        #endregion

        #region Veriables

        private Story _currentStory;

        #endregion

        #region Protected Methods

        protected override void OnPopupOpen(PopupRequest request) {
            var dialogueRequest = GetPopupRequest<DialoguePopupRequest>();

            _speakerText.text = dialogueRequest.Speaker;
            _currentStory = dialogueRequest.Story;

            ContinueStory();

            _dialogueScollView.verticalNormalizedPosition = 1f;

            _eventBus.Subscribe<DialogueContinuedEvent>(ContinueStory);
        }

        protected override void OnPopupClosed() {
            _eventBus.Unsubscribe<DialogueContinuedEvent>(ContinueStory);
            base.OnPopupClosed();
        }

        #endregion

        #region Private Methods

        private void ContinueStory()
            => ContinueStory(_currentStory.Continue());

        private void ContinueStory(object sender, EventArgs eventArgs)
            => ContinueStory(((DialogueContinuedEvent)eventArgs).NextDialogueLine);

        private void ContinueStory(string text) {
            _dialogueText.text = text;
            DisplayChoices();
        }

        private void DisplayChoices() {
            List<Choice> choices = _currentStory.currentChoices;

            //TODO: cache choice onjects for later and reuse them for better performance.
            foreach(Transform transform in _dialogueChoicesParent) {
                Destroy(transform.gameObject);
            }

            HideIndicator();

            for(int i = 0, iLength = choices.Count; i < iLength; i++) {
                var choice = Instantiate(_dialogueChoicePrefab, _dialogueChoicesParent);
                choice.Initialise(choices[i].text, _dialogueChoiceIndicator);

                //this does not work when inlined, the index must be cached like this!
                var index = i;
                choice.ChoiceButton.onClick.AsObservable().Subscribe(x => MakeChoice(index)).AddTo(choice);
            }
        }

        private void HideIndicator() {
            _dialogueChoiceIndicator.SetActive(false);
        }

        public void MakeChoice(int choiceIndex) {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }

        #endregion
    }
}
