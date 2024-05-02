using UnityEngine;
using System;

public class BaseEntity : MonoBehaviour {

    [SerializeField] protected string entityName;

    [HideInInspector] public Vector2 moveDirection; //Need this in here to conjunct with PlayerMoveState
    [HideInInspector] public bool isMoving;

    [SerializeField] protected Animator anim; public Animator Anim { get { return anim; } }
    [SerializeField] protected Rigidbody2D rigidBody; public Rigidbody2D RigidBody { get { return rigidBody; } }
    [SerializeField] protected SpriteRenderer spriteRend;

    [SerializeField]protected BaseHealthController healthController; public BaseHealthController HealthController { get { return healthController; } }
    [SerializeField] protected BaseMoveController moveController; public BaseMoveController MoveController { get { return moveController; } }

    protected bool canMove = true;

    public virtual void OnDeath() { }

    protected virtual void Start()
    {
        if (healthController != null)
            healthController.OnDeathCallback += OnDeath;
    }

    public virtual void TakeDamage(int damage) {

	}
}
