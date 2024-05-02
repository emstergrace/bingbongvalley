using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartHealth : MonoBehaviour
{
    public delegate void OnHit(int dmg);
    public OnHit OnHitCallback;

	// If armor has 20% damage reduction, this would be 0.2f
	[SerializeField] private float damageReduction = 0f; 

	public void TakeDamage(int val) {
		int damage = Mathf.CeilToInt(val - damageReduction * val);
		OnHitCallback?.Invoke(damage);
	} // End of TakeDamage().

	public void SetDamageReduction(float val) {
		damageReduction = val;
	} // End of SetDamageReduction().

}
