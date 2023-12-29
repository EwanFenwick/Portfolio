using Portfolio.EventBusSystem;
using UnityEngine;
using Zenject;

namespace Portfolio.PlayerController {
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerStateMachine : StateMachine {

        #region Variables

        [Inject] private readonly GlobalEventBus _eventBus;

        #endregion

        #region Properties

        public Vector3 Velocity;
        public float MovementSpeed { get; private set; } = 4f;
        public float JumpForce { get; private set; } = 4f;
        public float LookRotationDampFactor { get; private set; } = 10f;
        public Transform MainCamera { get; private set; }

        public InputReader InputReader { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController Controller { get; private set; }
        
        public GlobalEventBus EventBus => _eventBus;

        #endregion

        #region Lifecycle

        private void Start() {
            MainCamera = Camera.main.transform;

            InputReader = GetComponent<InputReader>();
            Animator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();

            SwitchState(new PlayerMoveState(this));
        }

        #endregion
    }
}
