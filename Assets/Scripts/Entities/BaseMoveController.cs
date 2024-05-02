using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    protected Vector2 moveInput;
    protected Vector2 lastMove;
    protected bool isMoving = false;
    private Vector3 tempPos = Vector3.zero;

    protected bool isStuck = false; public bool IsStuck { get { return isStuck; } }
    private int framesStuck = 0;
    private int maxFramesStuck = 30;

    [SerializeField] protected Animator anim; protected bool hasAnim = false;
    [SerializeField] protected Rigidbody2D rigidBody;

	private void Start() {
        hasAnim = anim != null;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        tempPos = transform.position;
        rigidBody.MovePosition(transform.position + new Vector3(moveInput.x * Time.fixedDeltaTime * moveSpeed, moveInput.y * Time.fixedDeltaTime * moveSpeed, 0f));
        if (moveInput != Vector2.zero && Vector3.SqrMagnitude(transform.position - tempPos) < Mathf.Epsilon) {
            framesStuck++;
            if (framesStuck > maxFramesStuck) {
                isStuck = true;
			}
		}
        else {
            isStuck = false;
            framesStuck = 0;
		}
    } // End of Update().

    public void StopMovement() {
        moveInput = Vector2.zero;
	} // End of StopMovement().

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
