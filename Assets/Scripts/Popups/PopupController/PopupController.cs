using System;
using System.Collections.Generic;
using System.Linq;
using Portfolio.EventBusSystem;
using UniRx;

namespace Portfolio.Popups {
    public class PopupController {

        #region Variables

        private readonly PopupControllerView _popupControllerView;
        private readonly EventBus _eventBus;

        private List<PopupRequest> _activePopups = new List<PopupRequest>();
        private Queue<PopupRequest> _popupQueue = new Queue<PopupRequest>();

        #endregion

        public PopupController(EventBus eventBus, PopupControllerView popupControllerView) {
            _eventBus = eventBus;
            _popupControllerView = popupControllerView;

            _activePopups.ObserveEveryValueChanged(x => x.Count).Subscribe(OnActivePopupsChanged);
            _popupQueue.ObserveEveryValueChanged(x => x.Count).Subscribe(OnPopupQueueChanged);

            _eventBus.Subscribe<ClosePopupEvent>(OnCloseRequested);
        }

        #region Public Methods

        public void RequestPopup(PopupRequest request) {
            if(request == null) {
                return;
            }

            _popupQueue.Enqueue(request);
        }

        public void ClosePopup(PopupRequest request) {
            if(request == null) {
                return;
            }

            if(_activePopups.Any(x => x.PopupType == request.PopupType)) {
                _popupControllerView.ClosePopup(request.PopupType);

                _activePopups.RemoveAll(x => x.PopupType == request.PopupType);
                _activePopups.TrimExcess();
            }

            if(request.PausePlayerControl) {
                FirePlayerPauseEvent(false);
            }
        }

        public void CloseAllPopups(bool clearQueue = false) {
            if(clearQueue) {
                _popupQueue.Clear();
            }

            _activePopups.Clear();
            _activePopups.TrimExcess();

            _popupControllerView.CloseAllPopups();
            FirePlayerPauseEvent(false);
        }

        #endregion

        #region Private Methods

        private void OnCloseRequested(object sender, EventArgs eventArgs) {
            ClosePopup(((ClosePopupEvent)eventArgs).Request);
        }

        private void TryOpenNextPopup() {
            if(_activePopups.Any(p => p.Dismissable == false)) {
                return;
            }

            var popupRequest = _popupQueue.Dequeue();
            _activePopups.Add(popupRequest);

            _popupControllerView.ShowPopup(popupRequest);

            if(popupRequest.PausePlayerControl) {
                FirePlayerPauseEvent(true);
            }
        }

        private void FirePlayerPauseEvent(bool isPaused) {
            _eventBus.Publish(this, new PausePlayerEvent(isPaused));
        }

        private void OnActivePopupsChanged(int count) {
            if(count == 0 && _popupQueue.Count > 0) {
                TryOpenNextPopup();
            }
        }

        private void OnPopupQueueChanged(int count) {
            if(count > 0 && _activePopups.Count == 0) {
                TryOpenNextPopup();
            }
        }

        #endregion
    }
}
