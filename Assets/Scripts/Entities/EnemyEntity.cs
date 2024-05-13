using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyEntity : BaseEntity
{

    public Action OnTakenDamage;

    protected override void Start() {
        base.Start();
        EnemyManager.inst?.RegisterEnemy(this);
        healthController.OnDeathCallback += OnDeath;
    }

    protected virtual void FixedUpdate() {
        if (anim != null && canMove) {
            anim.SetFloat("MoveX", moveDirection.x);
            anim.SetFloat("MoveY", moveDirection.y);
            anim.SetBool("Moving", isMoving);
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        OnTakenDamage?.Invoke();
    }

    public override void OnDeath() {
        EventManager.TriggerObjective(Objective.BountyNotifier + entityName, 1);
    } // End of OnDeath().

    private void OnDestroy() {
        // EnemyManager.inst?.removeEnemy(this);
    }
}
