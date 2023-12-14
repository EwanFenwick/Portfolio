using UnityEngine;

namespace Portfolio.Tweening {
    public class ScaleTransformTween : Tween<Transform, Vector3> {
        public override void UpdateComponentProgress(float evaluatedProgress)
            => _component.localScale = Vector3.LerpUnclamped(_from, _to, evaluatedProgress);
    }
}
