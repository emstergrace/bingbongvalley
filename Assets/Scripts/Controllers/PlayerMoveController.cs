using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : BaseMoveController
{

	public static PlayerMoveController Inst { get; private set; }
	private Vector3 postMovement = Vector3.zero;
	private Transform cameraTF = null;

	private int stepsSinceGrounded = 0;
	private bool isGrounded = false;
	private int groundContactCount = 0;
	private Vector3 contactNormal = Vector3.up;
	private RaycastHit groundHit;
	private float minGroundDotProduct = 0;

	private void Awake() {
		Inst = this;
	}

	private void Start() {
		GameInputManager.GameplayMap.FindAction("Move").performed += HandleInput;
		GameInputManager.GameplayMap.FindAction("Move").canceled += (x) => CancelInput();
		cameraTF = Camera.main.transform;
	}

	public void HandleInput(InputAction.CallbackContext action) {
		if (GameManager.AllowPlayerInput())
			HandleInput(action.ReadValue<Vector2>());
	} // End of HandleInput().

	public void CancelInput() {
		moveInput = Vector3.zero;
	} // End of CancelInput().

	public override void HandleInput(Vector2 input) {
		if (GameManager.IsGamePaused) return;

		moveInput = Vector3.zero;
		isMoving = false;

		if (input.sqrMagnitude > Mathf.Epsilon) {
			moveInput.Set(input.x, 0f, input.y);
			lastMove = input;
			isMoving = true;
		}

		if (hasAnim)
			anim.SetBool("Moving", isMoving);
	} // End of HandleInput().

	void FixedUpdate() {

		if (moveInput.sqrMagnitude > Mathf.Epsilon) {
			postMovement = cameraTF.forward * moveInput.z + cameraTF.right * moveInput.x;
			postMovement.Set(postMovement.x, 0f, postMovement.z);

			Vector3 desiredForward = Vector3.RotateTowards(transform.forward, postMovement, turnSpeed * Time.deltaTime, 0f);
			rotation = Quaternion.LookRotation(desiredForward);
			if (hasAnim) {
				rigidBody.MovePosition(rigidBody.position + postMovement * moveSpeed * anim.deltaPosition.magnitude);
			}
			else {
				rigidBody.MovePosition(rigidBody.position + postMovement * moveSpeed * Time.deltaTime);
			}
			rigidBody.MoveRotation(rotation);
		}

	} // End of Update().

	private void UpdateGroundedState() {
		stepsSinceGrounded += 1;

		if (isGrounded || SnapToGround()) {
			stepsSinceGrounded = 0;
			if (groundContactCount > 1) {
				contactNormal.Normalize();
			}
		}
		else {
			contactNormal = Vector3.up;
		}
	} // End of UpdateGroundedState().

	private bool SnapToGround() {
		if (stepsSinceGrounded > 1) {
			return false;
		}
		if (!Physics.Raycast(rigidBody.position, Vector3.down, out groundHit)) {
			return false;
		}
		if (groundHit.normal.y < minGroundDotProduct) {
			return false;
		}
		groundContactCount = 1;
		contactNormal = groundHit.normal;
		float speed = rigidBody.velocity.magnitude;
		float dot = Vector3.Dot(rigidBody.velocity, groundHit.normal);
		rigidBody.velocity = (rigidBody.velocity - groundHit.normal * dot).normalized * moveSpeed;
		return true;
			
	} // End of SnapToGround().

}
