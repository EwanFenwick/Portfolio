using UnityEngine;

namespace Portfolio.PlayerController {
    public abstract class PlayerBaseState : State {
        
        #region Variables

        protected readonly AnimationSettings _settings = new AnimationSettings();
        protected readonly PlayerStateMachine _stateMachine;

        #endregion

        protected PlayerBaseState(PlayerStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }

        #region Protected Methods

        protected void CalculateMoveDirection(bool halfSpeed = false) {
            var cameraForward = new Vector3(_stateMachine.MainCamera.forward.x, 0, _stateMachine.MainCamera.forward.z);
            var cameraRight = new Vector3(_stateMachine.MainCamera.right.x, 0, _stateMachine.MainCamera.right.z);

            var moveDirection = cameraForward.normalized * _stateMachine.InputReader.MoveComposite.y + cameraRight.normalized * _stateMachine.InputReader.MoveComposite.x;

            _stateMachine.Velocity.x = CalcVelocity(moveDirection.x);
            _stateMachine.Velocity.z = CalcVelocity(moveDirection.z);

            float CalcVelocity(float vectorComponent) => halfSpeed ?
                (float)(vectorComponent * _stateMachine.MovementSpeed) / 2
                : (float)(vectorComponent * _stateMachine.MovementSpeed);
        }

        protected void FaceMoveDirection() => RotateToDirection(
            new(_stateMachine.Velocity.x, 0f, _stateMachine.Velocity.z));

        protected void FaceCameraDirection() => RotateToDirection(
            new(_stateMachine.MainCamera.forward.x, 0f, _stateMachine.MainCamera.forward.z));

        protected void ApplyGravity() {
            if(_stateMachine.Velocity.y > Physics.gravity.y) {
                _stateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
            }
        }

        protected void Move() {
            _stateMachine.Controller.Move(_stateMachine.Velocity * Time.deltaTime);
        }

        #endregion

        #region Private Variables

        private void RotateToDirection(Vector3 faceDirection) {
            if(faceDirection == Vector3.zero) {
                return;
            }

            _stateMachine.transform.rotation = Quaternion.Slerp(_stateMachine.transform.rotation,
                Quaternion.LookRotation(faceDirection),
                _stateMachine.LookRotationDampFactor * Time.deltaTime);
        }

        #endregion
    }

    #region Nested Classes

    public class AnimationSettings {
        public readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        public readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        public readonly int FallHash = Animator.StringToHash("Fall");
        public readonly int JumpHash = Animator.StringToHash("Jump");

        public readonly float AnimationDampTime = 0.1f;
        public readonly float CrossFadeDuration = 0.1f;
    }

    #endregion
}
