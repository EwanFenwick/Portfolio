using UnityEngine;

namespace Portfolio.Utilities {
    public class LookAtCamera : LookAtTarget {
    
        private void Awake() {
            _target = Camera.main.transform;
            _nullOnDisable = false;
        }

        private void OnEnable() {
            FollowTarget();
        }

        private void OnDisable() {
            EndFollowing();
        }
    }
}
