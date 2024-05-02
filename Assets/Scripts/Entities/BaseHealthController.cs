using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthController : MonoBehaviour
{

    public delegate void OnDeath();
    public OnDeath OnDeathCallback;

    public int OverallHealth = 20;
    public int MaxHealth = 20;

    [SerializeField] private BodyPartHealth headPart;
    [SerializeField] private BodyPartHealth bodyPart;

	private void Start() {
        if (headPart != null)
        headPart.OnHitCallback += TakeDamage;

        if (bodyPart != null)
        bodyPart.OnHitCallback += TakeDamage;
	}

	public void TakeDamage(int val) {
        OverallHealth -= val;
        if (OverallHealth <= 0) {
            OverallHealth = 0;
            OnDeathCallback?.Invoke();
		}
	} // End of TakeDamage().

    public void HealImmediate(int val) {
        OverallHealth += val;
        if (OverallHealth > MaxHealth) OverallHealth = MaxHealth;
	} // End of HealImmediate().
}
