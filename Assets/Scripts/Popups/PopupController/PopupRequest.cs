using System;

public class PopupRequest {
    public Type PopupType { get; }
    public bool Dismissable { get; }

    public PopupRequest(Type popupType, bool dismissable = false) {
        PopupType = popupType;
        Dismissable = dismissable;
    }
}