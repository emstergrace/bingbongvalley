using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSceneOnTouch : MonoBehaviour
{
	public string sceneName;
	public Vector3 teleportPosition;
	public FaceDirection directionAfterTeleport;

	private void OnTriggerEnter2D(Collider2D collision) {

		GameManager.LoadScene(sceneName, teleportPosition);
	}
}
