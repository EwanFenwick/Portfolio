using System;
using Portfolio.Popups;

namespace Portfolio.EventBusSystem {
    public class ClosePopupEvent : EventArgs {
        public PopupRequest Request { get; set; }

        public ClosePopupEvent(PopupRequest request) {
            Request = request;
        }
    }
}
