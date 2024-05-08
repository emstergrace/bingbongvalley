using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float DespawnTime = 2f;
    public bool UsingLeanPool = false;
    float time = 0f;

	private void Update() {
		time += Time.deltaTime;
        if (time > DespawnTime) {
            if (UsingLeanPool) {
                time = 0f;
                Lean.Pool.LeanPool.Despawn(this.gameObject);
			}
            else {
                Destroy(this.gameObject);
			}
		}

	}
}
