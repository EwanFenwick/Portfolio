using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.PopupController {
    public class PopupControllerView : MonoBehaviour {
        
        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private Popup[] _popupPrefabs;

#pragma warning restore 0649
        #endregion

        #region Variables

        private readonly Dictionary<Type, Popup> _popupDictionary = new Dictionary<Type, Popup>();
        private readonly Dictionary<Type, Popup> _popupInstances = new Dictionary<Type, Popup>();

        #endregion

        #region Lifecycle

        private void Start() {
            for(int i = 0, iLength = _popupPrefabs.Length; i < iLength; i++) {
                var popup = _popupPrefabs[i];
                _popupDictionary.Add(popup.GetType(), popup);
            }
        }

        #endregion

        #region Public Methods

        public void ShowPopup(Type popupType) {
            if(GetPopup(popupType) is var popup) {
                popup.Open().Forget();
            }
        }

        public void ClosePopup(Type popupType) {
            if(GetPopup(popupType) is var popup) {
                popup.Close().Forget();
            }
        }

        public void CloseAllPopups(bool instant = false) {
            foreach (var popup in _popupInstances.Values.Where(p => p.isActiveAndEnabled)) {
                popup.Close().Forget();
            }
        }

        #endregion

        #region Private Methods

        private Popup GetPopup(Type popupType) {
            if(_popupInstances.TryGetValue(popupType, out Popup popupInstance)) {
                return popupInstance;
            }

            if(_popupDictionary.TryGetValue(popupType, out Popup popup)) {
                Popup instance = Instantiate(popup, transform);
                _popupInstances.Add(popupType, instance);
                return instance;
            }

            Debug.LogError($"Cannot find popup: {popupType.Name}");

            return null;
        }

        #endregion
    }
}