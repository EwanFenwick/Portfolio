using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio.Tweening {
    public class TweenController : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private bool _playOnStart;
        [SerializeField] private BaseTween _tween;

#pragma warning restore 0649
        #endregion

        #region Variables

        private float _progress;
        private bool _isReversed;
        private CancellationTokenSource cancelToken;

        #endregion

        #region Lifecycle

        private void Start() => PlayOnStartAndEnable().Forget();

        private void OnEnable() => PlayOnStartAndEnable().Forget();

        private void OnDisable() => StopAndResetTween(_isReversed);

        #endregion

        #region Public Methods

        public async UniTask Play() => await Play(false);

        public async UniTask PlayReversed() => await Play(true);

        public void Stop() => cancelToken?.Cancel();

        public void StopAndResetTween(bool resetToEnd = false) {
            Stop();
            ResetTween(resetToEnd);
        }

        #endregion

        #region Private Methods

        private async UniTaskVoid PlayOnStartAndEnable() {
            if(_playOnStart) {
                await Play();
            }
        }

        private async UniTask Play(bool reversed) {
            _isReversed = reversed;
            _progress = 0f;

            _tween.ResetTween();
            UpdateTweenComponent();

            cancelToken = new CancellationTokenSource();
            await PlayTween();
        }

        private async UniTask PlayTween() {
            while(_progress <= _tween.Duration) {
                cancelToken.Token.ThrowIfCancellationRequested();

                _progress += Time.deltaTime;
                UpdateTweenComponent();

                await UniTask.DelayFrame(1, cancellationToken: cancelToken.Token);
            }

            ResetTween(!_isReversed);
        }

        private void ResetTween(bool resetToEnd = false) {
            _tween.ResetTween();

            if(resetToEnd) {
                SetTweenProgress(_tween.Duration);
            }
        }

        private void UpdateTweenComponent() {
            var progress = _isReversed ? (_tween.Duration - _progress) : _progress;
            UpdateTweenComponentProgress(_tween, progress);
        }

        private void SetTweenProgress(float progress) {
            UpdateTweenComponentProgress(_tween, progress);
        }

        private void UpdateTweenComponentProgress(BaseTween tween, float progress) {
            var progressToEvaluate = tween.AnimationCurve.postWrapMode switch {
                WrapMode.Loop => progress / tween.Duration,
                WrapMode.PingPong => Mathf.PingPong(progress / tween.Duration, 1f),
                _ => Mathf.Clamp01(progress / tween.Duration),
            };

            tween.UpdateComponentProgress(tween.AnimationCurve.Evaluate(progressToEvaluate));
        }

        #endregion
    }
}
