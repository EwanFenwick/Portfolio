using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Tweening {
    public class ImageColourTween : Tween<Image, Color> {
        public override void UpdateComponentProgress(float evaluatedProgress) {
            Color colour = Color.LerpUnclamped(_start, _end, evaluatedProgress);
            _component.color = colour;
        }

        protected override void ResetToDynamicStart() {
            _start = _component.color;
        }
    }
}
