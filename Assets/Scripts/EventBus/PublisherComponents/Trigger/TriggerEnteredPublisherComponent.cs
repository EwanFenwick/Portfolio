using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;

namespace Portfolio.EventBusSystem {
    public class TriggerEnteredPublisherComponent : PublisherComponent<TriggerEnteredEvent> {
        [SerializeField] private string _triggerID;

        CompositeDisposable _disposable;

        private void OnEnable() {
            _disposable = new CompositeDisposable();
            gameObject.GetAsyncTriggerEnterTrigger().Subscribe(x => PublishEvent()).AddTo(_disposable);
        }

        private void OnDisable() {
            _disposable.Dispose();
        }

        protected override void PublishEvent() {
            Debug.Log($"Publishing Trigger Entered Event with ID: {_triggerID}");
            _eventBus.Triggers.Publish(this, new TriggerEnteredEvent(_triggerID));
        }
    }
}
