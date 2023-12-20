using System;

namespace Portfolio.Popups {
    public class PopupRequest {
        public Type PopupType { get; }
        public bool Dismissable { get; }
        public Action ClosePopupAction { get; set; }

        public PopupRequest(Type popupType, bool dismissable = true) {
            PopupType = popupType;
            Dismissable = dismissable;
        }
    }
}