using System.Collections.Generic;
using UnityEngine;
using Portfolio.Rewards;
using Portfolio.EventBusSystem;

using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    public class Quest {

        #region Variables

        private readonly QuestInfo _questInfo;
        private readonly GlobalEventBus _eventBus;
        private readonly GameObject _parentObject;
        private readonly QuestStepFactory _questStepFactory;

        private GameObject _questGameObject;
        private List<QuestStep> _activeQuestSteps;

        #endregion

        #region Properties

        // General
        public string ID => _questInfo.ID;
        public QuestState QuestState { get; private set; }

        // Prerequisites
        public int LevelPrerequisite => _questInfo._levelPrerequisite;
        public QuestInfo[] QuestPrerequisities => _questInfo._questPrerequisities;

        // Steps
        public int CurrentQuestStep { get; private set; }
        public QuestStepDict[] QuestSteps => _questInfo._questSteps;

        // Rewards
        public Reward Reward => _questInfo._reward;

        #endregion

        public Quest(QuestInfo questInfo, GlobalEventBus eventBus, GameObject parentObject, QuestStepFactory stepFactory) {
            _questInfo = questInfo;
            _eventBus = eventBus;
            _parentObject = parentObject;
            _questStepFactory = stepFactory;

            //TODO: get info from saved data
            QuestState = QuestState.RequirementsNotMet;
            CurrentQuestStep = 0;
        }

        #region Public Methods

        public void SetQuestStartable() => QuestState = QuestState.CanStart;

        public void StartQuest() {
            ContinueQuest();
        }

        public void ContinueQuest() {
            ActivateCurrentQuestSteps(GetCurrentQuestSteps());
            QuestState = QuestState.Progressing;
        }

        public void CompleteQuest() {
            QuestState = QuestState.Completed;
            UnityEngine.Object.Destroy(_questGameObject);

            //TODO: give out reward
            Debug.Log($"Rewarding player {Reward}");
        }

        public override string ToString() => ID;

        #endregion

        #region Private Methods

        private QuestStepInfo[] GetCurrentQuestSteps() => QuestSteps[CurrentQuestStep].Value;

        private void ActivateCurrentQuestSteps(QuestStepInfo[] currentSteps) {
            if(currentSteps.Length == 0) {
                Debug.LogError($"No quest steps found for current quest: {ID}");
                return;
            }

            if(_questGameObject == null) {
                _questGameObject = new(ID);
                _questGameObject.transform.parent = _parentObject.transform;
            }

            _activeQuestSteps = new();

            for(int i = 0, iLength = currentSteps.Length; i < iLength; i++) {
                var step = _questStepFactory.Create(currentSteps[i], _questGameObject);
                
                step.OnComplete += OnQuestStepCompleted;
                _activeQuestSteps.Add(step);
            }
        }

        private void OnQuestStepCompleted(string questStepID) {
            for(int i = 0, iLength = _activeQuestSteps.Count; i < iLength; i++) {
                if(questStepID.Equals(_activeQuestSteps[i].QuestStepID)) {
                    _activeQuestSteps[i].OnComplete -= OnQuestStepCompleted;
                }
            }

            _activeQuestSteps.RemoveAll(s => s.QuestStepID.Equals(questStepID));

            //check if there are more steps to complete
            if(_activeQuestSteps.Count != 0) {
                return;
            }

            //move to next stage of the quest
            if(++CurrentQuestStep >= QuestSteps.Length) {
                QuestState = QuestState.CanComplete;
                _eventBus.Quest.Publish(this, new QuestCanCompleteEvent());
            } else {
                QuestState = QuestState.CanContinue;
                _eventBus.Quest.Publish(this, new QuestCanContinueEvent());
            }
        }

        #endregion
    }
}
