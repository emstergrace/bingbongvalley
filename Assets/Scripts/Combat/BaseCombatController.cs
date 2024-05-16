using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombatController : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    protected float timeCounter = 0f;
    [SerializeField]protected float attackTimer = 1f;
    protected bool triedAttack = false;
    protected Vector2 aimDirection = Vector2.zero;
    protected int consecutiveShots = 0;

    public virtual void Start() {
	} // End of Start().

    public void HandleInput(bool attack) {

	} // End of HandleInput().

    public virtual void Update() {
        if (GameManager.IsGamePaused) return;

        if (timeCounter >= 0f) {
            if (anim) anim.SetBool("Attack", false);
            timeCounter -= Time.deltaTime;
		}
	} // End of Update().

    public void UpdateAttackTimer() {

    } // End of UpdateAttackTimer().

    public virtual bool CanAttack(Vector3 pos) {
        return true;
	} // End of CanAttack().
    
    public virtual bool TryAttack() {
        if (timeCounter <= 0f) {
            Attack();
            return true;
		}
        return false;
	} // End of TryAttack().

    public virtual void Attack() {
        Melee();
	} // End of Attack().

    protected virtual void Melee() {
        if (anim) {
            anim.Play("melee attack");
		}
	} // End of Melee().

    protected virtual void Shoot(Vector2 aimDir) { // Every shot after initial shot should have some sort of inaccuracy if automatic or if not waiting prior to directional resest

    } // End of Shoot().

} // End of BaseAttackController.
