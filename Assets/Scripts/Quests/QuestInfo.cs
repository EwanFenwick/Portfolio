using System;
using System.Linq;
using UnityEngine;
using Portfolio.Rewards;
using Portfolio.Utilities;

using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "Portfolio/Quests/New QuestInfo", order = 0)]
    public class QuestInfo : ScriptableObject {
        [field: SerializeField] public string ID { get; private set; }

        [Header("General")]
        public string _displayName;

        [Header("Requirements")]
        public int _levelPrerequisite;
        public QuestInfo[] _questPrerequisities;

        [Header("Steps")]
        public QuestStepDict[] _questSteps;

        [Header("Rewards")]
        public Reward _reward;

#if UNITY_EDITOR

        private void OnValidate() {
            ID = this.name;
            foreach(var args in _questSteps) {
                args.Value.ToList().ForEach(x => x.EditorSetQuestID(this.name));
            }

            _questSteps.OrderBy(x => x.Key).ToArray();

            UnityEditor.EditorUtility.SetDirty(this);
        }

#endif
    }

    [Serializable]
    public class QuestStepDict : SerialisableDictionary<int, QuestStepInfo[]> { }
    
    [Serializable]
    public class QuestStepInfo {
        [field: SerializeField] public string QuestID { get; private set; }
        [field: SerializeField] public string QuestStepID { get; private set; }
        [field: SerializeField] public QuestStepType QuestStepType { get; private set; }
        [field: SerializeField] public int NeededRepetitions { get; private set; }

        public void EditorSetQuestID(string id) => QuestID = id;
    }
}