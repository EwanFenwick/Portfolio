using System;
using UnityEngine;
using Portfolio.EventBusSystem;

namespace Portfolio.PlayerController {
    public class PlayerAimState : PlayerBaseState {

        public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine){ }

        #region Public Methods

        public override void Enter() {
            _stateMachine.EventBus.Publish(this, new AimStateChangedEvent(PlayerStatePhase.Entered));

            _stateMachine.EventBus.Subscribe<AimPerformedEvent>(SwitchToMoveState);
            _stateMachine.EventBus.Subscribe<PausePlayerEvent>(SwitchToPausedState);

            _stateMachine.Velocity.y = Physics.gravity.y;

            _stateMachine.Animator.CrossFadeInFixedTime(_settings.MoveBlendTreeHash, _settings.CrossFadeDuration);
        }

        public override void Tick() {
            if(!_stateMachine.Controller.isGrounded) {
                _stateMachine.SwitchState(new PlayerFallState(_stateMachine));
            }

            CalculateMoveDirection(true);
            FaceCameraDirection();
            Move();

            _stateMachine.Animator.SetFloat(_settings.MoveSpeedHash, _stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 0.5f : 0f, _settings.AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            _stateMachine.EventBus.Unsubscribe<AimPerformedEvent>(SwitchToMoveState);
            _stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(SwitchToPausedState);

            _stateMachine.EventBus.Publish(this, new AimStateChangedEvent(PlayerStatePhase.Exited));
        }

        #endregion

        #region Private Methods

        private void SwitchToMoveState(object sender, EventArgs eventArgs) {
            _stateMachine.SwitchState(new PlayerMoveState(_stateMachine));
        }

        private void SwitchToPausedState(object sender, EventArgs eventArgs) {
            switch(((PausePlayerEvent)eventArgs).PauseType) {
                case PauseEventType.Dialogue:
                    _stateMachine.SwitchState(new PlayerDialogueState(_stateMachine));
                    break;

                default:
                    Debug.LogError("Unexpected PauseEventType encountered");
                    break;
            }
        }

        #endregion
    }
}
