using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSceneInteractable : MonoBehaviour, IInteractable
{
	public string sceneName;
	public Vector3 teleportPosition;
	public FaceDirection directionAfterTeleport;

	public void Interact() {
		GameManager.LoadScene(sceneName, teleportPosition);
	}
}

public enum FaceDirection
{
	Up,
	Down,
	Left,
	Right
}