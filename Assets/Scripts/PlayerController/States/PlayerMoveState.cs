using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerMoveState : PlayerBaseState {

        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter() {
            stateMachine.EventBus.Subscribe<AimPerformedEvent>(SwitchToAimState);
            stateMachine.EventBus.Subscribe<JumpPerformedEvent>(SwitchToJumpState);
            stateMachine.EventBus.Subscribe<PausePlayerEvent>(SwitchToPausedState);

            stateMachine.Velocity.y = Physics.gravity.y;

            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick() {
            if(!stateMachine.Controller.isGrounded) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }

            CalculateMoveDirection();
            FaceMoveDirection();
            Move();

            stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            stateMachine.EventBus.Unsubscribe<AimPerformedEvent>(SwitchToAimState);
            stateMachine.EventBus.Unsubscribe<JumpPerformedEvent>(SwitchToJumpState);
            stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(SwitchToPausedState);
        }

        private void SwitchToJumpState(object sender, EventArgs eventArgs) {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }

        private void SwitchToPausedState(object sender, EventArgs eventArgs) {
            switch(((PausePlayerEvent)eventArgs).PauseType) {
                case PauseEventType.Dialogue:
                    stateMachine.SwitchState(new PlayerDialogueState(stateMachine));
                    break;
                
                default:
                    break;
            }
        }

        private void SwitchToAimState(object sender, EventArgs eventArgs) {
            stateMachine.SwitchState(new PlayerAimState(stateMachine));
        }
    }
}