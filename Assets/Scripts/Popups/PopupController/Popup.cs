using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using Portfolio.EventBusSystem;
using Portfolio.Tweening;

namespace Portfolio.Popups {
    public abstract class Popup : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private TweenController _openAnimation;
        [SerializeField, Tooltip("Optional: for dismissable popups")] private Button _closeButton;

#pragma warning restore 0649
        #endregion

        #region Variables

        protected EventBus _eventBus;

        private PopupRequest _popupRequest;
        private CompositeDisposable _closeDisposable;

        #endregion

        #region Lifecycle

        public virtual void Initialise(EventBus eventBus) {
            _eventBus = eventBus;
        }

        #endregion

        #region Public Methods

        public async UniTaskVoid Open(PopupRequest popupRequest) {
            _popupRequest = popupRequest;
            gameObject.SetActive(true);

            //call OnOpen before the opening animation
            OnPopupOpen(popupRequest);

            ConfigureCloseButton();

            //await the end of the opening animation
            _openAnimation.StopAndResetTween();
            await _openAnimation.Play();
        }

        public async UniTaskVoid Close() {
            _closeDisposable?.Dispose();

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

        protected virtual void OnCloseClicked() {
            _eventBus.Publish(this, new ClosePopupEvent(_popupRequest));
        }

        protected T GetPopupRequest<T>() where T : PopupRequest => (T)_popupRequest;

        #endregion

        #region Private Methods

        private void ConfigureCloseButton() {
            if(_closeButton == null) {
                return;
            }

            if(_popupRequest.Dismissable) {
                _closeDisposable = new CompositeDisposable();
                _closeButton.onClick.AsObservable().Subscribe(x => OnCloseClicked()).AddTo(_closeDisposable);
            }

            _closeButton.gameObject.SetActive(_popupRequest.Dismissable);
        }

        #endregion
    }
}