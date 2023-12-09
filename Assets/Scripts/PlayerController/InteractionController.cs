using Portfolio.PopupController;
using Portfolio.TestPopup;
using UnityEngine;
using Zenject;

public class InteractionController : MonoBehaviour {

    [Inject] PopupController _popupController;

    bool _testPopupOpen = false;
    PopupRequest _testPopupRequest;

    private void OnEnable() {
        _testPopupRequest = new PopupRequest(typeof(TestPopupView));
    }

    public void OnInteraction() {
        if(_testPopupOpen) {
            _popupController.ClosePopup(_testPopupRequest);
            _testPopupOpen = false;
        } else {
            _popupController.RequestPopup(_testPopupRequest);
            _testPopupOpen = true;
        }
    }
}
