using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using Portfolio.EventBusSystem;

using static Portfolio.Quests.QuestEnums;

namespace Portfolio.Quests {
    public class QuestManager : MonoBehaviour {
        
        #region Variables

        [Inject] private readonly GlobalEventBus _eventBus;
        [Inject] private readonly QuestFactory _questFactory;

        private Dictionary<string, Quest> _quests;

        #endregion

        #region Lifecycle

        private void Awake() {
            var allQuests = Resources.LoadAll<QuestInfo>("Quests");
            _quests = new Dictionary<string, Quest>();

            for(int i = 0, iLength = allQuests.Length; i < iLength; i++) {
                if(_quests.ContainsKey(allQuests[i].ID)) {
                    Debug.LogError($"Duplicate Quest ID found: {allQuests[i].ID}");
                }

                _quests.Add(allQuests[i].ID, _questFactory.Create(allQuests[i], this.gameObject));
            }
        }

        private void OnEnable() {
            _eventBus.Quest.Subscribe<QuestCanContinueEvent>(OnQuestCanContinue);
            _eventBus.Quest.Subscribe<QuestCanCompleteEvent>(OnQuestCanComplete);
        }

        private void OnDisable() {
            _eventBus.Quest.Unsubscribe<QuestCanContinueEvent>(OnQuestCanContinue);
            _eventBus.Quest.Unsubscribe<QuestCanCompleteEvent>(OnQuestCanComplete);
        }

        #endregion

        #region Public Methods

        public QuestState GetQuestStateByID(string ID) => GetQuestByID(ID).QuestState;
        
        #endregion

        #region Private Methods

        private bool QuestMeetsStartingRequirements(Quest quest) {
            var requirementsMet = true;

            //check if all required quests have been completed
            foreach(var prereq in quest.QuestPrerequisities) {
                if(_quests.TryGetValue(prereq.ID, out var q) && q.QuestState != QuestEnums.QuestState.Completed) {
                    requirementsMet = false;
                }
            }

            //Temporarily using 0 for player level
            if(quest.LevelPrerequisite < 0) {
                requirementsMet = false;
            }

            return requirementsMet;
        }

        private void OnQuestCanContinue(object sender, EventArgs eventArgs) {
            //TODO: Quest will have dependants to notify, like quest giver/continuer/finisher
            //If there is no designated dependant, quest will auto continue or auto complete
            Debug.Log($"Quest '{(Quest)sender}' can continue");
        }

        private void OnQuestCanComplete(object sender, EventArgs eventArgs) {
            Debug.Log($"Quest '{(Quest)sender}' can complete");
        }

        private Quest GetQuestByID(string ID) {
            if(_quests[ID] == null) {
                Debug.LogError($"Quest ID not found: {ID}");
            }
            return _quests[ID];
        }

        #endregion

#if UNITY_EDITOR

        [Button]
        private void DEBUG_CheckFirstQuestsRequirements() {
            var q = _quests.First().Value;
            if(QuestMeetsStartingRequirements(q)) {
                Debug.Log($"Quest '{q.ID}' meets all starting requirements");
                q.SetQuestStartable();
            } else {
                Debug.Log($"Quest '{q.ID}' does not meet all starting requirements");
            }
        }

        [Button]
        private void DEBUG_StartFirstQuest() {
            var q = _quests.First().Value;
            if(q.QuestState == QuestEnums.QuestState.CanStart) {
                Debug.Log($"Starting quest '{q.ID}'");
                q.StartQuest();
            } else {
                Debug.Log($"Quest '{q.ID}' cannot start");
            }
        }

        [Button]
        private void DEBUG_ContinueFirstQuest() {
            var q = _quests.First().Value;
            if(q.QuestState == QuestEnums.QuestState.CanContinue) {
                Debug.Log($"Continuing quest '{q.ID}'");
                q.ContinueQuest();
            } else {
                Debug.Log($"Quest '{q.ID}' cannot continue");
            }
        }

        [Button]
        private void DEBUG_CompleteFirstQuest() {
            Quest q = _quests.First().Value;
            if(q.QuestState == QuestEnums.QuestState.CanComplete) {
                Debug.Log($"Completing quest '{q.ID}'");
                q.CompleteQuest();
            } else {
                Debug.Log($"Quest '{q.ID}' is not completeable");
            }
        }

#endif
    }
}
