using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace Portfolio.Popups {
    public class PopupController {

        #region Variables

        [Inject] private PopupControllerView _popupControllerView;

        private List<PopupRequest> _activePopups = new List<PopupRequest>();
        private Queue<PopupRequest> _popupQueue = new Queue<PopupRequest>();

        #endregion

        public PopupController() {
            _activePopups.ObserveEveryValueChanged(x => x.Count).Subscribe(OnActivePopupsChanged);
            _popupQueue.ObserveEveryValueChanged(x => x.Count).Subscribe(OnPopupQueueChanged);
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
        }

        public void CloseAllPopups(bool clearQueue = false) {
            if(clearQueue) {
                _popupQueue.Clear();
            }

            _activePopups.Clear();
            _activePopups.TrimExcess();

            _popupControllerView.CloseAllPopups();
        }

        #endregion

        #region Private Methods

        private void TryOpenNextPopup() {
            if(_activePopups.Any(p => p.Dismissable == false)) {
                return;
            }

            var newPopup = _popupQueue.Dequeue();
            _activePopups.Add(newPopup);

            _popupControllerView.ShowPopup(newPopup.PopupType);
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
