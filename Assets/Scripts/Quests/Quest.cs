using UnityEngine;
using Zenject;
using Portfolio.Rewards;
using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    public class Quest {

        #region Variables

        [Inject] private readonly QuestStepFactory _questStepFactory;

        private readonly QuestInfo _questInfo;
        private readonly GameObject _parentObject;

        #endregion

        #region Properties

        public int ID { get; set; }
        
        public QuestState State { get; private set; }

        public QuestInfo[] QuestPrerequisities => _questInfo._questPrerequisities;

        public int CurrentQuestStep { get; private set; }

        public QuestStepDict[] QuestSteps => _questInfo._questSteps;

        public Reward Reward => _questInfo._reward;

        #endregion

        public Quest(QuestInfo questInfo, GameObject parentObject) {
            _questInfo = questInfo;
            _parentObject = parentObject;

            //TODO: get info from saved data
            State = QuestState.RequirementsNotMet;
            CurrentQuestStep = 0;
        }

        public void MoveToNextStep() => CurrentQuestStep++;

        public (QuestStepType type, QuestInfoArgs args) GetCurrentQuestStep() {
            return (QuestSteps[CurrentQuestStep].Key, QuestSteps[CurrentQuestStep].Value);
        }

        public void ActivateQuestStep() {
            //TEMP
            var (type, args) = GetCurrentQuestStep();
            GameObject g = new(args.QuestID);
            g.transform.parent = _parentObject.transform;
            var qs = _questStepFactory.Create(type, args, g);
            Debug.Log(qs);
        }
    }
}
