using System;
using UnityEngine;
using UnityEngine.Events;
using Portfolio.EventBusSystem;

namespace Portfolio {
    public class ToggleAimSubscriberComponent : EventBusSubscriberComponent<ToggleAimEvent> {

        [SerializeField] private GameObject _mainCam;
        [SerializeField] private GameObject _aimCam;
        [SerializeField] private GameObject _targetingReticle;

        private bool lastBool;

        protected override void OnEvent(object sender, EventArgs eventArgs) {
            _mainCam.SetActive(lastBool);
            lastBool = !lastBool;
            _aimCam.SetActive(lastBool);
            _targetingReticle.SetActive(lastBool);
        }
    }
}
