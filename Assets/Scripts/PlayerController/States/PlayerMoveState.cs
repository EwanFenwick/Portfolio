using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerMoveState : PlayerBaseState {

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            _stateMachine.EventBus.Subscribe<AimPerformedEvent>(SwitchToAimState);
            _stateMachine.EventBus.Subscribe<JumpPerformedEvent>(SwitchToJumpState);
            _stateMachine.EventBus.Subscribe<PausePlayerEvent>(SwitchToPausedState);

            _stateMachine.Velocity.y = Physics.gravity.y;

            _stateMachine.Animator.CrossFadeInFixedTime(_settings.MoveBlendTreeHash, _settings.CrossFadeDuration);

            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Tick() {
            if(!_stateMachine.Controller.isGrounded) {
                _stateMachine.SwitchState(new PlayerFallState(_stateMachine));
            }

            CalculateMoveDirection();
            FaceMoveDirection();
            Move();

            _stateMachine.Animator.SetFloat(_settings.MoveSpeedHash, _stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, _settings.AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            _stateMachine.EventBus.Unsubscribe<AimPerformedEvent>(SwitchToAimState);
            _stateMachine.EventBus.Unsubscribe<JumpPerformedEvent>(SwitchToJumpState);
            _stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(SwitchToPausedState);
        }

        #endregion

        #region Private Methods

        private void SwitchToJumpState(object sender, EventArgs eventArgs) {
            _stateMachine.SwitchState(new PlayerJumpState(_stateMachine));
        }

        private void SwitchToPausedState(object sender, EventArgs eventArgs) {
            switch(((PausePlayerEvent)eventArgs).PauseType) {
                case PauseEventType.Dialogue:
                    _stateMachine.SwitchState(new PlayerDialogueState(_stateMachine));
                    break;
                
                default:
                    break;
            }
        }

        private void SwitchToAimState(object sender, EventArgs eventArgs) {
            _stateMachine.SwitchState(new PlayerAimState(_stateMachine));
        }

        #endregion
    }
}
