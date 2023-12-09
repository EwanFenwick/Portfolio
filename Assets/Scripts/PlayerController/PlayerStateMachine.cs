using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InteractionController))]
public class PlayerStateMachine : StateMachine {

    public Vector3 Velocity;
    public float MovementSpeed { get; private set; } = 5f;
    public float JumpForce { get; private set; } = 5f;
    public float LookRotationDampFactor { get; private set; } = 10f;
    public Transform MainCamera { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public InteractionController InteractionController { get; set; }

    private void Start() {
        MainCamera = Camera.main.transform;

        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        InteractionController = GetComponent<InteractionController>();

        SwitchState(new PlayerMoveState(this));
    }
}
