using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Portfolio.Quests {
    public class QuestManager : MonoBehaviour {
        #region Variables

        [Inject] private readonly QuestFactory _questFactory;

        private Dictionary<string, Quest> _quests;

        #endregion

        #region Lifecycle

        private void Awake() {
            var allQuests = Resources.LoadAll<QuestInfo>("Quests");
            _quests = new Dictionary<string, Quest>();

            for(int i = 0, iLength = allQuests.Length; i < iLength; i++) {
                if (_quests.ContainsKey(allQuests[i].ID)) {
                    Debug.LogError($"Duplicate Quest ID found: {allQuests[i].ID}");
                }

                _quests.Add(allQuests[i].ID, _questFactory.Create(allQuests[i], this.gameObject));
            }

            Debug.Log(_quests.First().Key);
            Debug.Log(_quests.First().Value.State);
            Debug.Log(_quests.First().Value.Reward);

        }

        #endregion
        
        [Button]
        private void DEBUG_ActivateFirstQuest() {
            _quests.First().Value.ActivateQuestStep();
        }

        #region Private Methods

        private Quest GetQuestByID(string ID) {
            if(_quests[ID] == null) {
                Debug.LogError($"Quest ID not found: {ID}");
            }
            return _quests[ID];
        }

        #endregion
    }
}
