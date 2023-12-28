using UnityEngine;

namespace Portfolio.Tweening {
    public abstract class BaseTween : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField, Min(0.01f)] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

#pragma warning restore 0649
        #endregion

        #region Properties

        public float Duration => _duration;

        public AnimationCurve AnimationCurve => _animationCurve;

        protected float StartOfTween => AnimationCurve.Evaluate(0f);
        protected float EndOfTween => AnimationCurve.Evaluate(1f);

        #endregion

        #region Public Methods

        public abstract void UpdateComponentProgress(float evaluatedProgress);

        public void ResetTween() => ResetComponentProgress();

        public void ResetTween(bool resetToEnd) => ResetComponentProgress(resetToEnd);

        #endregion

        #region Protected Methods

        protected virtual void ResetComponentProgress(bool resetToEnd = false)
            => UpdateComponentProgress(resetToEnd ? EndOfTween : StartOfTween);

        #endregion
    }
}
