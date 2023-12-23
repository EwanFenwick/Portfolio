using UnityEngine;

namespace Portfolio.PlayerController {
    public abstract class PlayerBaseState : State {

        protected readonly PlayerStateMachine stateMachine;

        protected PlayerBaseState(PlayerStateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        protected void CalculateMoveDirection(bool halfSpeed = false) {
            var cameraForward = new Vector3(stateMachine.MainCamera.forward.x, 0, stateMachine.MainCamera.forward.z);
            var cameraRight = new Vector3(stateMachine.MainCamera.right.x, 0, stateMachine.MainCamera.right.z);

            var moveDirection = cameraForward.normalized * stateMachine.InputReader.MoveComposite.y + cameraRight.normalized * stateMachine.InputReader.MoveComposite.x;

            stateMachine.Velocity.x = CalcVelocity(moveDirection.x);
            stateMachine.Velocity.z = CalcVelocity(moveDirection.z);

            float CalcVelocity(float vectorComponent) => halfSpeed ?
                (float)(vectorComponent * stateMachine.MovementSpeed) / 2
                : (float)(vectorComponent * stateMachine.MovementSpeed);
        }

        protected void FaceMoveDirection() => RotateToDirection(
            new(stateMachine.Velocity.x, 0f, stateMachine.Velocity.z));

        protected void FaceCameraDirection() => RotateToDirection(
            new(stateMachine.MainCamera.forward.x, 0f, stateMachine.MainCamera.forward.z));

        protected void ApplyGravity() {
            if(stateMachine.Velocity.y > Physics.gravity.y) {
                stateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
            }
        }

        protected void Move() {
            stateMachine.Controller.Move(stateMachine.Velocity * Time.deltaTime);
        }

        private void RotateToDirection(Vector3 faceDirection) {
            if(faceDirection == Vector3.zero) {
                return;
            }

            stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation,
                Quaternion.LookRotation(faceDirection),
                stateMachine.LookRotationDampFactor * Time.deltaTime);
        }
    }
}
