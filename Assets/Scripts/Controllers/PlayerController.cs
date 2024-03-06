using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public Vector2 lastMove;

    public float moveSpeed;

    [HideInInspector] public Vector2 moveDirection; //Need this in here to conjunct with PlayerMoveState
    [HideInInspector] public bool isMoving;

    [SerializeField] protected Animator anim; public Animator Anim { get { return anim; } }
    [SerializeField] protected Rigidbody2D rigidBody; public Rigidbody2D RigidBody { get { return rigidBody; } }
    [SerializeField] protected SpriteRenderer spriteRend;

    public static PlayerController Inst { get; private set; }

	private void Awake() {
        Inst = this;
        //DontDestroyOnLoad(gameObject);
	}
     void Start() {

	} // End of Start().

	private void FixedUpdate() {

	} // End of FixedUpdate().

	private void Update() {

	} // End of Update(). 

}