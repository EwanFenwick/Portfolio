using UnityEngine;
using TMPro;

namespace Portfolio.Popups {
    public class TestPopupView : Popup {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TextMeshProUGUI _titleText;

#pragma warning restore 0649
        #endregion

        #region Protected Methods

        protected override void OnPopupOpen(PopupRequest request) {
            var dialogueRequest = GetPopupRequest<DialoguePopupRequest>();

            //_titleText.text = dialogueRequest.Dialogue;
        }

        #endregion
    }
}
