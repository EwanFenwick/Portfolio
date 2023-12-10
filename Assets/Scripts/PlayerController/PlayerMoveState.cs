using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerMoveState : PlayerBaseState {

        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter() {
            stateMachine.Velocity.y = Physics.gravity.y;

            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

            stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
            stateMachine.InputReader.OnInterationPerformed += ProcessInteraction;
        }

        public override void Tick() {
            if (!stateMachine.Controller.isGrounded) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }

            CalculateMoveDirection();
            FaceMoveDirection();
            Move();

            stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            stateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
            stateMachine.InputReader.OnInterationPerformed -= ProcessInteraction;
        }

        private void SwitchToJumpState() {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }

        private void ProcessInteraction() {
            stateMachine.InteractionController.OnInteraction();
        }
    }
}
