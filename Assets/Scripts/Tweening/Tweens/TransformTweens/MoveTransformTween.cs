using UnityEngine;

namespace Portfolio.Tweening {
    public class MoveTransformTween : Tween<Transform, Vector3> {
        public override void UpdateComponentProgress(float evaluatedProgress)
            => _component.localPosition = Vector3.LerpUnclamped(_from, _to, evaluatedProgress);
    }
}
