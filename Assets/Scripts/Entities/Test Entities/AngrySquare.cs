using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrySquare : EnemyEntity
{
	public override void OnDeath() {
		base.OnDeath();

		Destroy(this.gameObject);
	}
}
