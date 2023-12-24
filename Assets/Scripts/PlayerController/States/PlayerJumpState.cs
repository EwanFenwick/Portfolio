using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerJumpState : PlayerBaseState {

        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
        
        #region Public Methods

        public override void Enter() {
            _stateMachine.Velocity = new Vector3(_stateMachine.Velocity.x, _stateMachine.JumpForce, _stateMachine.Velocity.z);

            _stateMachine.Animator.CrossFadeInFixedTime(_settings.JumpHash, _settings.CrossFadeDuration);
        }

        public override void Tick() {
            ApplyGravity();

            if(_stateMachine.Velocity.y <= 0f) {
                _stateMachine.SwitchState(new PlayerFallState(_stateMachine));
            }

            FaceMoveDirection();
            Move();
        }

        public override void Exit() { }

        #endregion
    }
}
