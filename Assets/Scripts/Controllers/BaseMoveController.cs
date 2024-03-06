using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    protected Vector2 moveInput;
    protected Vector2 lastMove;
    protected bool isMoving = false;

    [SerializeField] protected Animator anim; protected bool hasAnim = false;
    [SerializeField] protected Rigidbody2D rigidBody;

	private void Start() {
        hasAnim = anim != null;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + new Vector3(moveInput.x * Time.fixedDeltaTime * moveSpeed, moveInput.y * Time.fixedDeltaTime * moveSpeed, 0f));
    } // End of Update().

    public virtual void HandleInput(Vector2 input) {
        if (GameManager.IsGamePaused) return;

        moveInput = Vector2.zero;
        isMoving = false;

        if (input.sqrMagnitude > Mathf.Epsilon) {
            moveInput = input.normalized;
            lastMove = input;
            isMoving = true;

            if (hasAnim) {
                anim.SetFloat("MoveX", moveInput.x);
                anim.SetFloat("MoveY", moveInput.y);
			}
		}
		else {
            if (hasAnim) {
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
            }
		}
        if (hasAnim)
            anim.SetBool("Moving", isMoving);
    } // End of HandleInput().

}
