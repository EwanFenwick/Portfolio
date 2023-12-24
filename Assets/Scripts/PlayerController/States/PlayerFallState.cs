using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerFallState : PlayerBaseState {

        public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter() {
            _stateMachine.Velocity.y = 0f;

            _stateMachine.Animator.CrossFadeInFixedTime(_settings.FallHash, _settings.CrossFadeDuration);
        }

        public override void Tick() {
            ApplyGravity();
            Move();

            if(_stateMachine.Controller.isGrounded) {
                _stateMachine.SwitchState(new PlayerMoveState(_stateMachine));
            }
        }

        public override void Exit() { }
    }
}
