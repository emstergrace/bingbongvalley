using System.Collections;
using UnityEngine;

public class PlayerController : BaseEntity {

    [HideInInspector] public Vector2 lastMove;

    public static PlayerController Inst { get; private set; }

	private void Awake() {
		if (Inst == null) {
			Inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(this.gameObject);
		}
	} // End of Awake().

	public override void OnDeath() {

	} // End of OnDeath().

	public void Teleport(Vector3 position) {
		transform.position = position;
	} // End of Teleport().

} // End of PlayerController.