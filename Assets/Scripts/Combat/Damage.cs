using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damageVal = 0;

    [SerializeField] protected bool isProjectile = true;

    private Collider2D[] ownerColliders = new Collider2D[0];
    private bool isUsed = false;

	public void SetDamage(int dmg, bool isProj = true) {
        damageVal = dmg;
        isProjectile = isProj;
	} // End of SetDamage().


    public void SetOwner(Collider2D[] s) {
        ownerColliders = s;
	} // End of SetShooter().

	private void OnTriggerEnter2D(Collider2D collision) {
        if (isUsed) return;
		foreach(Collider2D col in ownerColliders) {
            if (col == collision) return;
		}

        BodyPartHealth bph = collision.GetComponent<BodyPartHealth>();
        if (bph != null) {
            bph.TakeDamage(damageVal);
            isUsed = true;
		}
        else {
            BaseHealthController bhc = collision.GetComponent<BaseHealthController>();
            if (bhc != null) {
                bhc.TakeDamage(damageVal);
			}
            isUsed = true;
		}

        if (isProjectile) {
            Destroy(gameObject);
		}
	} // End of OnTriggerEnter2D().

} // End of Damage().
