using TMPro;
using UnityEngine;

namespace Portfolio.Tweening {
    public class ColourTextTween : Tween<TextMeshProUGUI, Color> {
        public override void UpdateComponentProgress(float evaluatedProgress)
            => _component.color = Color.LerpUnclamped(_from, _to, evaluatedProgress);
    }
}
