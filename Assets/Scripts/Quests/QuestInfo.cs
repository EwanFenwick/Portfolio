using UnityEngine;
using Portfolio.Rewards;
using Portfolio.Utilities;
using static Portfolio.Quests.QuestEnums;
using System;

namespace Portfolio.Quests {
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "Portfolio/Quests/New QuestInfo", order = 0)]
    public class QuestInfo : ScriptableObject {
        [field: SerializeField] public string ID { get; private set; }

        [Header("General")]
        public string _displayName;

        [Header("Requirements")]
        public QuestInfo[] _questPrerequisities;

        [Header("Steps")]
        public QuestStepDict[] _questSteps;

        [Header("Rewards")]
        public Reward _reward;

#if UNITY_EDITOR

        private void OnValidate() {
            ID = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        }

#endif
    }

    [Serializable]
    public class QuestStepDict : SerialisableDictionary<QuestStepType, QuestInfoArgs> { }
    
    [Serializable]
    public class QuestInfoArgs {
        [field: SerializeField] public string QuestID { get; private set; }
        [field: SerializeField] public int Repetitions { get; private set; }
    }
}