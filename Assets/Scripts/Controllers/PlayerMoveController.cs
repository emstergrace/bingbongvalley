using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : BaseMoveController
{

	public static PlayerMoveController Inst { get; private set; }

	private void Awake() {
		Inst = this;
	}

	private void Start() {
		GameInputManager.GameplayMap.FindAction("Move").performed += HandleInput;
		GameInputManager.GameplayMap.FindAction("Move").canceled += (x) => HandleInput(Vector2.zero);
	}

	public void HandleInput(InputAction.CallbackContext action) {
		if (GameManager.AllowPlayerInput())
			HandleInput(action.ReadValue<Vector2>());
	} // End of HandleInput().

	public override void HandleInput(Vector2 input) {
		base.HandleInput(input);
	} // End of HandleInput().
}
