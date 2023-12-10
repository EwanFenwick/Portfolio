using System;

namespace Portfolio.Popups {
    public class PopupRequest {
        public Type PopupType { get; }
        public bool Dismissable { get; }

        public PopupRequest(Type popupType, bool dismissable = false) {
            PopupType = popupType;
            Dismissable = dismissable;
        }
    }

    public class DialoguePopupRequest : PopupRequest {
        public string Dialogue { get; set; }

        public DialoguePopupRequest(Type popupType, string dialogue, bool dismissable = false)
            : base(popupType, dismissable) {
            Dialogue = dialogue;
        }
    }
}