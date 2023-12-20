using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerDialogueState : PlayerBaseState {

        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerDialogueState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter() {
            stateMachine.InteractionController.OnInteraction();

            stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

            stateMachine.InputReader.OnInterationPerformed += ProcessInteraction;
            stateMachine.InteractionController.InteractionStateChanged += OnInteractionStateChanged;

            stateMachine.CinemachineFreeLook.enabled = false;
        }

        public override void Exit() {
            stateMachine.InputReader.OnInterationPerformed -= ProcessInteraction;
            stateMachine.InteractionController.InteractionStateChanged -= OnInteractionStateChanged;

            stateMachine.CinemachineFreeLook.enabled = true;
        }

        public override void Tick() {
            stateMachine.Animator.SetFloat(MoveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
        }

        private void OnInteractionStateChanged(bool isInteracting) {
            if(!isInteracting) {
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            }
        }

        private void ProcessInteraction() {
            stateMachine.InteractionController.OnInteraction();
        }
    }
}
