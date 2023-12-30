namespace Portfolio.Quests {
    public class QuestEnums {
        public enum QuestState {
            RequirementsNotMet,
            CanStart,
            Started,
            Inprogress,
            CanComplete,
            Completed,
            Failed,
        }

        public enum QuestStepType {
            Trigger,
        }
    }
}
