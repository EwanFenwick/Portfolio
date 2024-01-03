using Ink.Runtime;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.Popups {
    public class DialoguePopupRequest : PopupRequest {

        public string Speaker { get; set; }
        public Story Story { get; set; }

        public DialoguePopupRequest(string speakerName, Story story)
            : base(typeof(DialoguePopupPresenter), dismissable: false, pauseEventType: PauseEventType.Dialogue) {
            Speaker = speakerName;
            Story = story;
        }
    }
}
