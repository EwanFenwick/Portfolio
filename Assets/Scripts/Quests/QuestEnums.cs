using System;

using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.Quests {
    public static class QuestEnums {
        #region Enums

        public enum QuestState {
            RequirementsNotMet,
            CanStart,
            InProgress,
            CanComplete,
            Completed,
            Failed,
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
