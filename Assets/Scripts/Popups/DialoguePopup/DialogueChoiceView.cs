using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Portfolio.Tweening;

namespace Portfolio.Popups {
    public class DialogueChoiceView : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TextMeshProUGUI _choiceText;
        [SerializeField] private Button _choiceButton;

        [SerializeField] private TweenController _highlightTweenController;
        [SerializeField] private TextColourTween _textTween;

#pragma warning restore 0649
        #endregion

        #region Properties

        public TextMeshProUGUI ChoiceText => _choiceText;
        public Button ChoiceButton => _choiceButton;

        #endregion

        #region Lifecycle

        public void Initialise(string choiceText, Color colour) {
            _choiceText.text = choiceText;
            _choiceText.color = colour;
        }

        public async UniTask HighlightText(Color colour) {
            _highlightTweenController.StopAndResetTween();
            _textTween.SetDynamicTarget(colour);

            await _highlightTweenController.Play();
        }

        #endregion
    }
}