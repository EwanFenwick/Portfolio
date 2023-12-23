using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public abstract class PlayerPauseState : PlayerBaseState {
        
        #region Variables

        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        #endregion
        
        protected PlayerPauseState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            stateMachine.EventBus.Subscribe<PausePlayerEvent>(CheckForUnpause);

            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick() {
            stateMachine.Animator.SetFloat(MoveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit() {
            stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(CheckForUnpause);
        }

        #endregion

        #region Private Methods

        private void CheckForUnpause(object sender, EventArgs eventArgs) {
            //TODO: expand this to allow transition to other pause states later
            if(((PausePlayerEvent)eventArgs).PauseType != PauseEventType.Unpause) {
                return;
            }

            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        #endregion
    }
}
