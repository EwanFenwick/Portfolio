using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UniRx;
using Ink.Runtime;
using Cysharp.Threading.Tasks.Triggers;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks;
using Portfolio.EventBusSystem;

namespace Portfolio.Popups {
    public class DialoguePopupPresenter : Popup {

        #region Editor Variables
#pragma warning disable 0649

        [Header("Story Texts")]
        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private ScrollRect _dialogueScollView;

        [Header("Dialogue Choices")]

        [SerializeField] private RectTransform _dialogueChoicesParent;
        [SerializeField] private DialogueChoiceView _dialogueChoicePrefab;

        [Header("Dialogue Choice Indicator")]
        [SerializeField] private DialogueIndicatorView _choiceIndicator;

#pragma warning restore 0649
        #endregion

        #region Variables

        //TODO: This and other meta data can be integrated into the story file via tags 
        private readonly Color _textNormalColour = Color.white;
        private readonly Color _textHighlightColour = Color.cyan;

        private Story _currentStory;
        private List<DialogueChoiceView> _choiceViewInstances = new();
        private CompositeDisposable _choiceDisposables;

        private GameObject _lastClickedGameObject;

        #endregion

        #region Properties

        public bool ChoicesAvailable => _currentStory.currentChoices.Count > 0;

        #endregion

        #region Protected Methods

        protected override void OnPopupOpen(PopupRequest request) {
            var dialogueRequest = GetPopupRequest<DialoguePopupRequest>();

            _speakerText.text = dialogueRequest.Speaker;
            _currentStory = dialogueRequest.Story;

            _choiceDisposables = new CompositeDisposable();

            _choiceIndicator.Hide();
            ContinueStory();

            _eventBus.Misc.Subscribe<DialogueContinuedEvent>(ContinueStory);
        }

        protected override void OnPopupClosed() {
            _eventBus.Misc.Unsubscribe<DialogueContinuedEvent>(ContinueStory);

            _lastClickedGameObject = null;
            _choiceDisposables.Dispose();

            _choiceIndicator.Hide();

            base.OnPopupClosed();
        }

        protected override void OnCloseClicked() {
            _eventBus.Misc.Unsubscribe<DialogueContinuedEvent>(ContinueStory);
            base.OnCloseClicked();
        }

        #endregion

        #region Private Methods

        private void ContinueStory() {
            if(!_currentStory.canContinue) {
                OnCloseClicked();
                return;
            }
            
            ContinueStory(_currentStory.Continue());
        }

        private void ContinueStory(object sender, EventArgs eventArgs)
            => ContinueStory(((DialogueContinuedEvent)eventArgs).NextDialogueLine);

        private void ContinueStory(string text) {
            _dialogueText.text = text;
            ScrollToTopOfDialogueView();
            DisplayChoices();
        }

        private void ScrollToTopOfDialogueView()
            => _dialogueScollView.verticalNormalizedPosition = 1f;

        private void DisplayChoices() {
            var choices = _currentStory.currentChoices;

            HideChoices();

            if(!ChoicesAvailable){
                _lastClickedGameObject = null;
                _choiceIndicator.Hide();
                return;
            }

            //TODO: It might be more SOLID to turn this into a factory class later
            for(int i = 0, iLength = choices.Count; i < iLength; i++) {
                DialogueChoiceView choice;

                if(_choiceViewInstances.Count > i) {
                    choice = _choiceViewInstances[i];
                    choice.gameObject.SetActive(true);

                    HighlightChoiceIfHovering(choice);
                } else {
                    choice = Instantiate(_dialogueChoicePrefab, _dialogueChoicesParent);
                    _choiceViewInstances.Add(choice);
                }

                choice.Initialise(choices[i].text, Color.white);

                SubscribeToChoiceView(choice, i);
            }

            void HighlightChoiceIfHovering(DialogueChoiceView choice) {
                //The new InputSystem does not check if the pointer enters a gameobject when it is enabled.
                //This method allows the manual 'selection' of a choiceview if the pointer clicked on one, and a new choice is enabled under it.
                if(_lastClickedGameObject == null) {
                    return;
                }

                //TODO: if the number of choices changes, the mouse might be hovering over a different one. Raycasting might be needed in that case as the InputSystem hides the current gamobject the pointer is over.
                if(_lastClickedGameObject.Equals(choice.gameObject)) {
                    choice.HighlightText(_textHighlightColour).Forget();
                    _choiceIndicator.MoveTo((RectTransform)choice.transform).Forget();
                }
            }

            void SubscribeToChoiceView(DialogueChoiceView choice, int index) {
                choice.ChoiceButton.onClick.AsObservable().Subscribe(_ => MakeChoice(choice, index)).AddTo(_choiceDisposables);

                choice.GetAsyncPointerEnterTrigger().SubscribeAwait(async x => {
                    await UniTask.WhenAll(
                        MoveIndicator(x),
                        choice.HighlightText(_textHighlightColour));
                }).AddTo(_choiceDisposables);

                choice.GetAsyncPointerExitTrigger().SubscribeAwait(async _ => {
                    await UniTask.WhenAll(
                        HideIndicator(),
                        choice.HighlightText(_textNormalColour));
                }).AddTo(_choiceDisposables);
            }
        }

        private void HideChoices() {
            _choiceViewInstances.ForEach(x => x.gameObject.SetActive(false));
            _choiceDisposables.Clear();
        }

        private async UniTask MoveIndicator(PointerEventData eventData)
            => await _choiceIndicator.MoveTo((RectTransform)eventData.pointerEnter.transform);

        private async UniTask HideIndicator()
            => await _choiceIndicator.FadeOut();

        private void MakeChoice(DialogueChoiceView choice, int choiceIndex) {
            _lastClickedGameObject = choice.gameObject;

            _currentStory.ChooseChoiceIndex(choiceIndex);

            ContinueStory();
        }

        #endregion
    }
}
