using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {

	#region Public Variables

	public Vector2 MouseDelta;
	public Vector2 MoveComposite;

	public Action OnJumpPerformed;

	#endregion

	#region Private Variables

	private Controls controls;

	#endregion

	#region Lifecycle

	private void OnEnable() {
		if(controls != null) {
			return;
		}

		controls = new Controls();
		controls.Player.SetCallbacks(this);
		controls.Player.Enable();
	}

	public void OnDisable() {
		controls.Player.Disable();
	}

	#endregion

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

		OnJumpPerformed?.Invoke();
	}
}
