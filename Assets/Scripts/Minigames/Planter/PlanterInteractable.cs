using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterInteractable : MonoBehaviour, IInteractable
{
	public GameObject plantCanvas = null;
	public PlanterMenu planterMenu = null;

	public void Interact() {
		if (!plantCanvas.activeSelf) {
			//plantCanvas.SetActive(true);
			GameManager.PushMenuUI(planterMenu);
		}
	}
}
