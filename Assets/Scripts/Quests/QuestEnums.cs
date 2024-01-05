using System;

using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.Quests {
    public static class QuestEnums {
        #region Enums

        public enum QuestState {
            RequirementsNotMet,
            CanStart,
            Progressing,
            CanContinue,
            CanComplete,
            Completed,
            Failed,
            InProgress = Progressing | CanContinue,
        }

        public enum QuestStepType {
            Trigger,
        }

        #endregion

        #region Enum Extensions

        public static SubscriberComponentType ToSubscriberComponentType(this QuestStepType stepType) => stepType switch {
            QuestStepType.Trigger => SubscriberComponentType.TriggerEntered,
            _ => throw new NotImplementedException(),
        };

        #endregion
    }
}
