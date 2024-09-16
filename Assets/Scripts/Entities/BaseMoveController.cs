using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed = 20f;
    protected Quaternion rotation;
    protected Vector3 moveInput;

    protected Vector2 lastMove;
    protected bool isMoving = false;

    [SerializeField] protected Animator anim; protected bool hasAnim = false;
    [SerializeField] protected Rigidbody rigidBody;

	private void Start() {
        hasAnim = anim != null;
	}

	// Update is called once per frame
	void FixedUpdate()
    {

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, moveInput, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
        if (hasAnim) {
            rigidBody.MovePosition(rigidBody.position + moveInput * moveSpeed * anim.deltaPosition.magnitude);
        }
        else {
            rigidBody.MovePosition(transform.position + moveInput * moveSpeed * Time.deltaTime);
        }
        
        rigidBody.MoveRotation(rotation);

    } // End of Update().

    public void StopMovement() {
        moveInput = Vector3.zero;
	} // End of StopMovement().

    public virtual void HandleInput(Vector2 input) {
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

}
