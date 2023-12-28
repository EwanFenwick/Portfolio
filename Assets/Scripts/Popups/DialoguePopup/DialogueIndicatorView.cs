using UnityEngine;
using Cysharp.Threading.Tasks;
using Portfolio.Tweening;

namespace Portfolio.Popups {
    public class DialogueIndicatorView : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [Header("Movement Tweening")]
        [SerializeField] private TweenController _moveTweenController;
        [SerializeField] private MoveRectTransformTween _moveTween;

        [Header("Alpha Fade Tweening")]
        [SerializeField] private TweenController _fadeTweenController;

#pragma warning restore 0649
        #endregion

        bool _isHighlighting = false;

        #region Public Methods

        public async UniTask MoveTo(RectTransform target) {
            _moveTween.SetDynamicTarget(target.localPosition);

            if(_isHighlighting) {
                _isHighlighting = true;
                _fadeTweenController.StopAndResetTween();
                _moveTweenController.StopAndResetTween();
                await _moveTweenController.Play();
            } else {
                _isHighlighting = true;
                ((RectTransform)_moveTweenController.transform).anchoredPosition = target.localPosition;
                await FadeIn();
            }
        }

        public async UniTask FadeOut() {
            _fadeTweenController.StopAndResetTween();
            await _fadeTweenController.Play();
            _isHighlighting = false;
        }

        public void Hide() {
            _fadeTweenController.StopAndResetTween(true);
            _isHighlighting = false;
        }

        #endregion

        #region Private Methods

        private async UniTask FadeIn() {
            _fadeTweenController.StopAndResetTween(true);
            await _fadeTweenController.PlayReversed();
        }

        #endregion
    }
}
