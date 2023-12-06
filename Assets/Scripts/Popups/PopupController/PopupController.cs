using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Portfolio.PopupController {
    public class PopupController {
        [Inject] private PopupControllerView _popupControllerView;

        private List<PopupRequest> _activePopups = new List<PopupRequest>();

        private Queue<PopupRequest> _popupQueue = new Queue<PopupRequest>();

        public PopupController() {
            _activePopups.ObserveEveryValueChanged(x => x.Count).Subscribe(OnActivePopupsChanged);
            _popupQueue.ObserveEveryValueChanged(x => x.Count).Subscribe(OnPopupQueueChanged);
        }

        public void RequestPopup(PopupRequest request) {
            if(request == null) {
                return;
            }

            _popupQueue.Enqueue(request);
        }

        private void TryOpenNextPopup() {
            if(_activePopups.Any(p => p.Dismissable == false)) {
                return;
            }
            var newPopup = _popupQueue.Dequeue();
            if (_popupQueue.Count > 0) {
                _activePopups.Add(newPopup);
            }

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
    }
}
