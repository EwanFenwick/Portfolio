using System;
using UnityEngine;
using Portfolio.EventBusSystem;

namespace Portfolio.PlayerController {
    public class PlayerAimState : PlayerBaseState {
        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine){ }

        public override void Enter() {
            stateMachine.EventBus.Publish(this, new AimStateChangedEvent(PlayerStatePhase.Entered));

            stateMachine.EventBus.Subscribe<AimPerformedEvent>(SwitchToMoveState);
            stateMachine.EventBus.Subscribe<PausePlayerEvent>(SwitchToPausedState);

            stateMachine.Velocity.y = Physics.gravity.y;

            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick() {
            if(!stateMachine.Controller.isGrounded) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }

            CalculateMoveDirection(true);
            FaceCameraDirection();
            Move();

            stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 0.5f : 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            stateMachine.EventBus.Unsubscribe<AimPerformedEvent>(SwitchToMoveState);
            stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(SwitchToPausedState);

            stateMachine.EventBus.Publish(this, new AimStateChangedEvent(PlayerStatePhase.Exited));
        }

        private void SwitchToMoveState(object sender, EventArgs eventArgs) {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        private void SwitchToPausedState(object sender, EventArgs eventArgs) {
            switch(((PausePlayerEvent)eventArgs).PauseType) {
                case PauseEventType.Dialogue:
                    stateMachine.SwitchState(new PlayerDialogueState(stateMachine));
                    break;

                default:
                    Debug.LogError("Unexpected PauseEventType encountered");
                    break;
            }
        }
    }
}
