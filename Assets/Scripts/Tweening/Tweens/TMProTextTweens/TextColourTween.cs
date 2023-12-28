using TMPro;
using UnityEngine;

namespace Portfolio.Tweening {
    public class TextColourTween : Tween<TextMeshProUGUI, Color> {
        public override void UpdateComponentProgress(float evaluatedProgress)
            => _component.color = Color.LerpUnclamped(_start, _end, evaluatedProgress);

        protected override void ResetToDynamicStart() {
            _start = _component.color;
        }
    }
}
