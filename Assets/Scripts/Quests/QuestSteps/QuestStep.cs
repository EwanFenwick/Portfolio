using System;
using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using Portfolio.EventBusSystem;

namespace Portfolio.Quests {
    [Serializable]
    public class QuestStep : MonoBehaviour {

        #region Variables

        [Inject] private readonly ISubscriberComponent _subscriberComponent;
        [Inject] private readonly QuestStepInfo _questStepInfo;

        private int _timesRepeated;

        #endregion

        #region Properties

        public string QuestStepID => _questStepInfo.QuestStepID;
        public event Action<string> OnComplete;

        #endregion

        #region Lifecycle

        private void Start() {
            _subscriberComponent.OnEventPerformed += OnQuestEvent;
        }

        private void OnDisable() {
            _subscriberComponent.OnEventPerformed -= OnQuestEvent;
        }

        #endregion

        #region Private Methods

        private void OnQuestEvent(EventArgs eventArgs) {
            string eventID = _questStepInfo.QuestStepType switch {
                QuestEnums.QuestStepType.Trigger =>
                    ((TriggerEnteredEvent)eventArgs).TriggerID,

                _ => throw new NotImplementedException(),
            };

            if(!eventID.Equals(_questStepInfo.QuestStepID)) {
                return;
            }

            if(++_timesRepeated >= _questStepInfo.NeededRepetitions) {
                CompleteQuestStep();
            }
        }

        private void CompleteQuestStep() {
            OnComplete?.Invoke(_questStepInfo.QuestStepID);

            //wait one frame before destroying/unsubscribing from the event bus to prevent out of bounds exceptions during Publish()
            MainThreadDispatcher.StartEndOfFrameMicroCoroutine(Destroy());
        }

        private IEnumerator Destroy() {
            yield return null;

            _subscriberComponent.Destroy();
            Destroy(this);
        }

        #endregion
    }
}
