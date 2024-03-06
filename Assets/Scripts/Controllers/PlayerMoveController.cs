using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : BaseMoveController
{

	private void Awake() {

	}

	private void Start() {
		InputManager.GameplayMap.FindAction("Move").performed += HandleInput;
		InputManager.GameplayMap.FindAction("Move").canceled += (x) => HandleInput(Vector2.zero);
	}

	public void HandleInput(InputAction.CallbackContext action) {
		HandleInput(action.ReadValue<Vector2>());
	} // End of HandleInput().

	public override void HandleInput(Vector2 input) {
		base.HandleInput(input);
	} // End of HandleInput().
}
