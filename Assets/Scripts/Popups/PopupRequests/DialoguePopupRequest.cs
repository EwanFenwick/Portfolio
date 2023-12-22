using Ink.Runtime;

namespace Portfolio.Popups {
    public class DialoguePopupRequest : PopupRequest {

        public string Speaker { get; set; }
        public Story Story { get; set; }

        public DialoguePopupRequest(string speakerName, Story story)
            : base(typeof(DialoguePopupView), dismissable: false, pausePlayerInput: true) {
            Speaker = speakerName;
            Story = story;
        }
    }
}
