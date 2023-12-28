using UnityEngine;

namespace Portfolio.Tweening {
    public class MoveRectTransformTween : Tween<RectTransform, Vector3> {
        public override void UpdateComponentProgress(float evaluatedProgress)
            => _component.anchoredPosition = Vector3.LerpUnclamped(_start, _end, evaluatedProgress);

        protected override void ResetToDynamicStart() {
            _start = _component.anchoredPosition;
        }
    }
}