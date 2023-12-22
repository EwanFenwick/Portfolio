using System;
using Portfolio.EventBusSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {

	#region Properties

	public Vector2 MouseDelta { get; set; }
	public Vector2 MoveComposite { get; set; }

	#endregion

	#region Variables

	[Inject] private readonly EventBus _eventBus;

	private Controls _controls;

	#endregion

	#region Lifecycle

	private void OnEnable() {
		if(_controls != null) {
			return;
		}

		Cursor.lockState = CursorLockMode.Locked;

		_controls = new Controls();
		_controls.Player.SetCallbacks(this);
		_controls.Player.Enable();
	}

	private void OnDisable() {
		_controls.Player.Disable();
	}

	#endregion

	#region Public Mathods

	public void OnLook(InputAction.CallbackContext context) {
		MouseDelta = context.ReadValue<Vector2>();
	}

	public void OnMove(InputAction.CallbackContext context) {
		MoveComposite = context.ReadValue<Vector2>();
	}

	public void OnJump(InputAction.CallbackContext context) {
		if(!context.performed) {
			return;
		}

		_eventBus.Publish(this, new JumpPerformedEvent());
	}

    public void OnInteract(InputAction.CallbackContext context) {
        if(!context.performed) {
			return;
		}

		_eventBus.Publish(this, new InteractionPerformedEvent());
    }

    public void OnAim(InputAction.CallbackContext context) {
		if (!context.performed) {
			return;
		}

		_eventBus.Publish(this, new ToggleAimEvent());
	}

    #endregion
}
