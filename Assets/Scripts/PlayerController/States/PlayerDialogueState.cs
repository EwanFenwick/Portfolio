using System;
using Portfolio.EventBusSystem;
using UnityEngine;

namespace Portfolio.PlayerController {
    public class PlayerDialogueState : PlayerPauseState {

        public PlayerDialogueState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        #region Public Methods

        public override void Enter() {
            _stateMachine.EventBus.PlayerState.Publish(this, new DialogueStateChangedEvent(PlayerStatePhase.Entered));

            base.Enter();
        }

        public override void Exit() {
            base.Exit();

            _stateMachine.EventBus.PlayerState.Publish(this, new DialogueStateChangedEvent(PlayerStatePhase.Exited));
        }

        #endregion


    }
}
