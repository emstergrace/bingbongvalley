using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector2 direction;
    [SerializeField] private float moveSpeed = 10f;

    public void SetDirection(Vector2 direction, float variance = 0f) {
        direction = Quaternion.AngleAxis(Random.Range(-1 * variance, 1 * variance), Vector3.forward) * direction;
        this.direction = direction;
    } // End of SetDirection().

	private void FixedUpdate() {
        transform.Translate(direction * Time.fixedDeltaTime * moveSpeed, Space.World);
	} // End of FixedUpdate().
}
