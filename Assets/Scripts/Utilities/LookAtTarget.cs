using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio.Utilities {
    public class LookAtTarget : MonoBehaviour {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _target;

        private CancellationTokenSource cancelToken;

        private void OnEnable() {
            if(_target != null) {
                LookAtTargetImmediate();
                FollowTarget();
            }
        }

        private void LookAtTargetImmediate() {
            GetLookRotation(out var currentRot, out var newRot);

            //set initial position
            transform.rotation = Quaternion.Lerp(currentRot, newRot, 1f);
        }

        private void OnDisable() {
            cancelToken?.Cancel();
            _target = null;
        }

        public bool SetTarget(Transform target) {
            if(target == null) {
                cancelToken?.Cancel();
                return false;
            }

            _target = target;
            return true;
        }

        public void SetAndFollowTarget(Transform target) {
            if(SetTarget(target)) {
                FollowTarget();
            }
        }

        private void FollowTarget() {
            if(_target == null) {
                return;
            }

            cancelToken = new CancellationTokenSource();
            
            LookAtTargetImmediate();
            FollowTargetSmoothly().Forget();
        }

        private async UniTask FollowTargetSmoothly() {
            GetLookRotation(out var currentRot, out var newRot);

            float counter = 0;
            while(counter < _rotationSpeed) {
                cancelToken.Token.ThrowIfCancellationRequested();

                counter += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / _rotationSpeed);

                await UniTask.DelayFrame(1, cancellationToken: cancelToken.Token);
            }

            FollowTargetSmoothly().Forget();
        }

        private void GetLookRotation(out Quaternion currentRot, out Quaternion newRot) {
            currentRot = transform.rotation;
            var targetposition = new Vector3(_target.position.x, transform.position.y, _target.position.z); //remove y-axis rotation
            newRot = Quaternion.LookRotation(targetposition - transform.position, transform.TransformDirection(Vector3.up));
        }
    }
}
