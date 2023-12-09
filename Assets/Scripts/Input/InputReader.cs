using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {

	#region Properties

	public Vector2 MouseDelta { get; set; }
	public Vector2 MoveComposite { get; set; }

	public Action OnJumpPerformed { get; set; }
	public Action OnInterationPerformed { get; set; }

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

	private void OnDisable() {
		controls.Player.Disable();
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

		OnJumpPerformed?.Invoke();
	}

    public void OnInteract(InputAction.CallbackContext context) {
        if(!context.performed) {
			return;
		}

		OnInterationPerformed?.Invoke();
    }

    #endregion
}
