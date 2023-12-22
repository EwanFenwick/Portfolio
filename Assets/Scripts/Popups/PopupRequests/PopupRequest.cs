using System;

namespace Portfolio.Popups {
    public class PopupRequest {
        public Type PopupType { get; }
        public bool Dismissable { get; }
        public bool PausePlayerControl { get; set; }

        public PopupRequest(Type popupType, bool dismissable = true, bool pausePlayerInput = false) {
            PopupType = popupType;
            Dismissable = dismissable;
            PausePlayerControl = pausePlayerInput;
        }
    }
}