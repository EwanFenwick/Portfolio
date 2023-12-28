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

        private bool _isReversed;
        private CancellationTokenSource _cancelToken;

        #endregion

        #region Properties

        public bool IsAtStart { get; private set; }
        public bool IsAtEnd { get; private set; }

        #endregion

        #region Lifecycle

        private void Start() => PlayOnStartAndEnable().Forget();

        private void OnEnable() => PlayOnStartAndEnable().Forget();

        private void OnDisable() {
            StopAndResetTween(_isReversed);
            _cancelToken?.Dispose();
            _cancelToken = null;
        }

        #endregion

        #region Public Methods

        public async UniTask Play() => await Play(false);

        public async UniTask PlayReversed() => await Play(true);

        public void Stop() => _cancelToken?.Cancel();

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

            _tween.ResetTween();

            _cancelToken = new CancellationTokenSource();

            IsAtEnd = false;
            IsAtStart = false;

            await PlayTween();

            ResetTween(!_isReversed);
        }

        private async UniTask PlayTween() {
            var progress = 0f;
            while(progress <= _tween.Duration) {
                _cancelToken.Token.ThrowIfCancellationRequested();

                progress += Time.deltaTime;
                UpdateTween(progress);

                await UniTask.DelayFrame(1, cancellationToken: _cancelToken.Token);
            }
        }

        private void ResetTween(bool resetToEnd = false) {
            _tween.ResetTween(resetToEnd);

            IsAtStart = !resetToEnd;
            IsAtEnd = resetToEnd;
        }

        private void UpdateTween(float progress)
            => UpdateTweenProgress(_tween, CalculateTweenProgress(progress));

        private float CalculateTweenProgress(float progress)
            => _isReversed ? (_tween.Duration - progress) : progress;

        private void UpdateTweenProgress(BaseTween tween, float progress)
            => tween.UpdateComponentProgress(
                tween.AnimationCurve.Evaluate(tween.AnimationCurve.postWrapMode switch {
                    WrapMode.Loop => progress / tween.Duration,
                    WrapMode.PingPong => Mathf.PingPong(progress / tween.Duration, 1f),
                    _ => Mathf.Clamp01(progress / tween.Duration),
                }));

        #endregion
    }
}
