using UnityEngine;

namespace Portfolio.Tweening {
    public abstract class BaseTween : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

#pragma warning restore 0649
        #endregion

        #region Properties

        public float Duration => _duration;

        public AnimationCurve AnimationCurve => _animationCurve;

        #endregion

        #region Public Methods

        public abstract void UpdateComponentProgress(float evaluatedProgress);

        public void ResetTween() => ResetComponentProgress();

        #endregion

        #region Protected Methods

        protected abstract void ResetComponentProgress();

        #endregion
    }
}
