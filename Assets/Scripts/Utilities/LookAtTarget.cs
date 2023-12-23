using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Portfolio.Utilities {
    public class LookAtTarget : MonoBehaviour {

        #region Editor Variables
#pragma warning disable 0649

        [SerializeField, Range(0.1f, 10f)] private float _rotationSpeed;
        [SerializeField] protected Transform _target;

#pragma warning restore 0649
        #endregion

        #region Variables

        private CancellationTokenSource _cancelToken;
        protected bool _nullOnDisable = true;

        #endregion

        #region Public Methods

        public void SetAndFollowTarget(Transform target) {
            if(SetTarget(target)) {
                FollowTarget();
            }

            bool SetTarget(Transform target) {
                if(target == null) {
                    EndFollowing();
                    return false;
                }

                _target = target;
                return true;
            }
        }

        public void EndFollowing() {
            _cancelToken?.Cancel();
            _target = _nullOnDisable ? null : _target;
        }

        #endregion

        #region Protected Methods

        protected void FollowTarget() {
            if(_target == null) {
                return;
            }

            _cancelToken = new CancellationTokenSource();

            LookAtTargetImmediate();

            MainThreadDispatcher.StartUpdateMicroCoroutine(FollowTargetSmoothly(_cancelToken.Token));
        }

        #endregion

        #region Private Methods

        private void LookAtTargetImmediate() {
            GetLookRotation(out var currentRot, out var newRot);

            //set initial position
            transform.rotation = Quaternion.Lerp(currentRot, newRot, 1f);
        }

        private IEnumerator FollowTargetSmoothly(CancellationToken cancellationToken) {
            GetLookRotation(out var currentRot, out var newRot);

            float counter = 0;
            while(counter <= _rotationSpeed) {
                if(cancellationToken.IsCancellationRequested) {
                    break;
                }

                counter += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / _rotationSpeed);

                //loop
                if(counter >= _rotationSpeed) {
                    counter = 0;
                    GetLookRotation(out currentRot, out newRot);
                }

                yield return null;
            }
        }

        private void GetLookRotation(out Quaternion currentRot, out Quaternion newRot) {
            currentRot = transform.rotation;
            var targetposition = new Vector3(_target.position.x, transform.position.y, _target.position.z); //remove y-axis from rotation
            newRot = Quaternion.LookRotation(targetposition - transform.position, transform.TransformDirection(Vector3.up));
        }

        #endregion
    }
}
