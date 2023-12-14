using Cysharp.Threading.Tasks;
using Portfolio.Tweening;
using UnityEngine;

namespace Portfolio.Popups {
    public abstract class Popup : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] TweenController _openAnimation;

#pragma warning restore 0649
        #endregion

        #region Variables

        private PopupRequest _popupRequest;

        #endregion

        #region Public Methods

        public async UniTaskVoid Open(PopupRequest popupRequest) {
            _popupRequest = popupRequest;
            gameObject.SetActive(true);

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

        #endregion

        #region Protected Methods

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