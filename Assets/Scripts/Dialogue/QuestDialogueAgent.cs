using System.Collections;
using System.Collections.Generic;
using Portfolio.Quests;
using UnityEngine;
using Zenject;

namespace Portfolio.Dialogue {
    public class QuestDialogueAgent : DialogueAgent {

        #region Editor Variables
#pragma warning disable 0649

        [Header("Quest Dialogue Agent")]
        [SerializeField] private TextAsset _questDialogueJSON;
        [SerializeField] private string _questID;

#pragma warning restore 0649
        #endregion

        [Inject]
        private readonly QuestManager _questManager;

        public override TextAsset Dialogue => _questDialogueJSON;

        private Quest _quest;

    }
}
