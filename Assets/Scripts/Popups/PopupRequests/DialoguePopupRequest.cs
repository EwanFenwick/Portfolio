namespace Portfolio.Popups {
    public class DialoguePopupRequest : PopupRequest {
        public string Speaker { get; set; }
        public string Dialogue { get; set; }

        public DialoguePopupRequest(string speakerName, string dialogueText)
            : base(typeof(DialoguePopupView), dismissable: false) {
            Speaker = speakerName;
            Dialogue = dialogueText;
        }
    }
}
