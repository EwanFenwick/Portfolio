using System;
using NaughtyAttributes;
using UnityEngine;
using static Portfolio.SceneManagement.SceneManagementEnums;

namespace Portfolio.Utilities {
    [Serializable]
    public class SerialisableSceneDictionary : ISerialisableDictionary<SceneType, int> {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] private SceneType _key;

        [SerializeField, Scene] private int _value;

#pragma warning restore 0649
        #endregion

        #region Properties

        public SceneType Key => _key;

        public int Value => _value;

        #endregion
    }
}