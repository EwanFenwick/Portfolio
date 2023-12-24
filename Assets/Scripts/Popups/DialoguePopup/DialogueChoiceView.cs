using TMPro;
using UnityEngine;

namespace Portfolio.Popups {
    public class DialogueChoiceView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _choiceText;

        public DialogueChoiceView(string choiceText) {
            _choiceText.text = choiceText;
        }
    }
}