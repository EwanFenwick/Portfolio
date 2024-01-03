using System;
using UnityEngine;

using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    [Serializable]
    public class QuestStepInfo {
        [field: SerializeField] public string QuestID { get; private set; }
        [field: SerializeField] public string QuestStepID { get; private set; }
        [field: SerializeField] public QuestStepType QuestStepType { get; private set; }
        [field: SerializeField] public int NeededRepetitions { get; private set; }

#if UNITY_EDITOR
        public void EditorSetQuestID(string id) => QuestID = id;
#endif
    }
}