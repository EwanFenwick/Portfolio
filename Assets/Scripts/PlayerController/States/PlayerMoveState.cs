using System;
using Portfolio.EventBusSystem;
using UnityEngine;
using static Portfolio.EventBusSystem.EventBusEnums;

namespace Portfolio.PlayerController {
    public class PlayerMoveState : PlayerBaseState {

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            _stateMachine.EventBus.Input.Subscribe<AimPerformedEvent>(SwitchToAimState);
            _stateMachine.EventBus.Input.Subscribe<JumpPerformedEvent>(SwitchToJumpState);
            _stateMachine.EventBus.PlayerState.Subscribe<TogglePlayerPauseStateEvent>(SwitchToPausedState);

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
            _stateMachine.EventBus.Input.Unsubscribe<AimPerformedEvent>(SwitchToAimState);
            _stateMachine.EventBus.Input.Unsubscribe<JumpPerformedEvent>(SwitchToJumpState);
            _stateMachine.EventBus.PlayerState.Unsubscribe<TogglePlayerPauseStateEvent>(SwitchToPausedState);
        }

        #endregion

        #region Private Methods

        private void SwitchToJumpState(object sender, EventArgs eventArgs) {
            _stateMachine.SwitchState(new PlayerJumpState(_stateMachine));
        }

        private void SwitchToPausedState(object sender, EventArgs eventArgs) {
            switch(((TogglePlayerPauseStateEvent)eventArgs).PauseType) {
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
