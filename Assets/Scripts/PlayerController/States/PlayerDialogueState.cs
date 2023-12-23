using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerDialogueState : PlayerPauseState {

        public PlayerDialogueState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            stateMachine.EventBus.Publish(this, new DialogueStateChangedEvent(PlayerStatePhase.Entered));

            base.Enter();
        }

        public override void Exit() {
            base.Exit();

            stateMachine.EventBus.Publish(this, new DialogueStateChangedEvent(PlayerStatePhase.Exited));
        }

        #endregion


    }
}
