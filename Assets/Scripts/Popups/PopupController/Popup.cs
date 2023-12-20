using Cysharp.Threading.Tasks;
using Portfolio.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Portfolio.Popups {
    public abstract class Popup : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] TweenController _openAnimation;
        [SerializeField, Tooltip("Optional: for dismissable popups")] Button _closeButton;

#pragma warning restore 0649
        #endregion

        #region Variables

        private PopupRequest _popupRequest;

        #endregion

        #region Public Methods

        public async UniTaskVoid Open(PopupRequest popupRequest) {
            _popupRequest = popupRequest;
            gameObject.SetActive(true);

            ConfigureCloseButton();

            //call OnOpen before the opening animation
            OnPopupOpen(popupRequest);

            //await the end of the opening animation
            _openAnimation.StopAndResetTween();
            await _openAnimation.Play();
        }

        public async UniTaskVoid Close() {
            //await the end of the closing animation
            _openAnimation.StopAndResetTween(true);
            await _openAnimation.PlayReversed();

            //disable gameobject after the closing animation
            OnPopupClosed();
        }

        private void ConfigureCloseButton() {
            if (_closeButton == null) {
                return;
            }

            _closeButton.gameObject.SetActive(_popupRequest.Dismissable);
            _closeButton.onClick.AddListener(OnCloseClicked);
        }

        #endregion

        #region Protected Methods

        protected virtual void OnCloseClicked() {
            _popupRequest.ClosePopupAction?.Invoke();
        }

        protected abstract void OnPopupOpen(PopupRequest request);

        protected virtual void OnPopupClosed() {
            _popupRequest = null;
            gameObject.SetActive(false);
        }

        protected T GetPopupRequest<T>()
            where T : PopupRequest => (T)_popupRequest;

        #endregion
    }
}