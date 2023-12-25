using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Portfolio.Popups {
    public class DialogueChoiceView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private TextMeshProUGUI _choiceText;
        [SerializeField] private Button _choiceButton;

        public Button ChoiceButton => _choiceButton;

        public GameObject Indicator { get; set; }


        public void Initialise(string choiceText, GameObject indicator) {
            _choiceText.text = choiceText;
            Indicator = indicator;
        }

        public void OnPointerEnter(PointerEventData eventData) {

            Indicator.SetActive(true);
            var indicatorPos = Indicator.transform.position;
            Indicator.transform.position = new(transform.position.x, indicatorPos.y, indicatorPos.z);
        }

        public void OnPointerExit(PointerEventData eventData) {
            Indicator.SetActive(false);
        }
    }
}