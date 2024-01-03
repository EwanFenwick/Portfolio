using System;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.Popups {
    public class PopupRequest {
        public Type PopupType { get; }
        public bool Dismissable { get; }
        public PauseEventType PauseEventType { get; set; }

        public PopupRequest(Type popupType, bool dismissable = true, PauseEventType pauseEventType = PauseEventType.None) {
            PopupType = popupType;
            Dismissable = dismissable;
            PauseEventType = pauseEventType;
        }
    }
}