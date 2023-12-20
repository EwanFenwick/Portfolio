using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Portfolio.Popups {
    public class DialoguePopupView : Popup {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private ScrollRect _dialogueScollView;

#pragma warning restore 0649
        #endregion

        protected override void OnPopupOpen(PopupRequest request) {
            var dialogueRequest = GetPopupRequest<DialoguePopupRequest>();

            _speakerText.text = dialogueRequest.Speaker;
            _dialogueText.text = dialogueRequest.Dialogue;

            _dialogueScollView.verticalNormalizedPosition = 1f;
        }
    }
}
