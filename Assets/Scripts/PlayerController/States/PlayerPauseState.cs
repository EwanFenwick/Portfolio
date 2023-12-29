using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public abstract class PlayerPauseState : PlayerBaseState {

        protected PlayerPauseState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            _stateMachine.EventBus.PlayerState.Subscribe<TogglePlayerPauseStateEvent>(CheckForUnpause);

            _stateMachine.Animator.CrossFadeInFixedTime(_settings.MoveBlendTreeHash, _settings.CrossFadeDuration);
            Cursor.lockState = CursorLockMode.Confined;
        }

        public override void Tick() {
            _stateMachine.Animator.SetFloat(_settings.MoveSpeedHash, 0f, _settings.AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            _stateMachine.EventBus.PlayerState.Unsubscribe<TogglePlayerPauseStateEvent>(CheckForUnpause);
        }

        #endregion

        #region Private Methods

        private void CheckForUnpause(object sender, EventArgs eventArgs) {
            //TODO: expand this to allow transition to other pause states later
            if(((TogglePlayerPauseStateEvent)eventArgs).PauseType != PauseEventType.Unpause) {
                return;
            }

            _stateMachine.SwitchState(new PlayerMoveState(_stateMachine));
        }

        #endregion
    }
}
