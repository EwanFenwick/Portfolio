using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerPausedState : PlayerBaseState {

        #region Variables

        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        #endregion

        public PlayerPausedState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

            stateMachine.EventBus.Subscribe<PausePlayerEvent>(SwitchToMoveState);
        }

        public override void Exit() {
            stateMachine.EventBus.Unsubscribe<PausePlayerEvent>(SwitchToMoveState);
        }

        public override void Tick() {
            stateMachine.Animator.SetFloat(MoveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
        }

        #endregion

        #region Private Methods

        private void SwitchToMoveState(object sender, EventArgs eventArgs) {
            if(((PausePlayerEvent)eventArgs).IsPaused) {
                return;
            }

            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        #endregion
    }
}
